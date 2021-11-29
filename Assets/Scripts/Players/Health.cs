using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

[RequireComponent(typeof(AnimationSwitcher))]
public class Health : MonoBehaviourPunCallbacks, ITakeDamage
{
    private const int _maxHealth = 100;

    [SerializeField] private Gradient _gradient;
    private Slider _healthValue;
    private Image _fill;

    private AnimationSwitcher _animationSwitcher;

    private void Awake()
    {
        _healthValue = FindObjectOfType<Slider>();
        _healthValue.value = _maxHealth;
    }
    private void Start()
    {
        _animationSwitcher = GetComponent<AnimationSwitcher>();
        _fill = _healthValue.fillRect.GetComponent<Image>();
        _fill.color = _gradient.Evaluate(1);
    }
    public void ToHeal(int health)
    {
        _healthValue.value += health;
    }
    public void TakeDamage(int damage)
    {
        if(photonView.IsMine)
        {
            int damageReduce = _animationSwitcher.IsBlockNow ? damage / 5 : damage;
            _healthValue.value -= damageReduce;
            _fill.color = _gradient.Evaluate(_healthValue.normalizedValue);

            if (_healthValue.value <= 0)
            {
                _animationSwitcher.SetDeadAnimation();
            }
        }
    }
    public void DestroyPlayer() //Called from Animator
    {
        PhotonNetwork.Destroy(gameObject);
        PhotonNetwork.LoadLevel("Lobby");
    }
}

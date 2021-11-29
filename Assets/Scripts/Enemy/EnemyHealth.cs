using UnityEngine;
using Photon.Pun;

public class EnemyHealth : MonoBehaviourPun
{
    [SerializeField] private ParticleSystem _bloodDrops;
    private int _health = 100;
    private EnemyAnimationSwitcher _animationSwitcher;

    private void Start()
    {
        _animationSwitcher = GetComponent<EnemyAnimationSwitcher>();
    }
    public void TakeDamage(int damage)
    {
        _health -= damage;
        _bloodDrops.Play();

        if(_health <= 0)
        {
            _animationSwitcher.SetDeadAnimation();
        }
    }
    public void DestroyEnemy()
    {
        PhotonNetwork.Destroy(gameObject);
    }
}

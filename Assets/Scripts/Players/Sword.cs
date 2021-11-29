using UnityEngine;
using Photon.Pun;

public class Sword : MonoBehaviourPun
{
    [SerializeField] private AnimationSwitcher _animationSwitcher;
    private const int _damage = 35;
    private AudioPlayer _audioPlayer;

    private void Start()
    {
        _audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    private void OnTriggerEnter(Collider collider)
    {
        if(_animationSwitcher.IsHitNow)
        {
            var enemy = collider.GetComponent<EnemyHealth>();
            var drop = collider.GetComponent<Drop>();

            enemy?.TakeDamage(_damage);
            drop?.TakeDamage(_damage);

            if(enemy != null || drop != null)
                _audioPlayer.PlayHitClip();
        }
    }
    
}

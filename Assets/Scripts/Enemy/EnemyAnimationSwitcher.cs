using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimationSwitcher : MonoBehaviour
{
    private Animator _animator;
    public bool IsDead { get; private set; }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public void SetMoveAnimation(bool isRun)
    {
        _animator.SetBool("IsRun", isRun);
    }
    public void SetHitAnimation()
    {
        if(!_animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            _animator.SetTrigger("Hit");
    }
    public void SetDeadAnimation()
    {
        if(!_animator.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
        {
            _animator.SetTrigger("Dead");
            IsDead = true;
        }
            
    }
}

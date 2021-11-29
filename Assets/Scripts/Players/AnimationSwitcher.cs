using UnityEngine;
using Photon.Pun;

public class AnimationSwitcher : MonoBehaviourPun
{

    private Animator _animator;
    private ClickHandler _buttonAttack;
    private ClickHandler _buttonBlock;
    public bool IsHitNow { get; private set; }
    public bool IsBlockNow { get; private set; }
    

    public void Init(ClickHandler buttonAttack, ClickHandler buttonBlock)
    {
        this._buttonAttack = buttonAttack;
        this._buttonBlock = buttonBlock;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetMoveAnimation(bool isRun)
    {
        _animator.SetBool("Run", isRun);
    }

    public void SetDeadAnimation()
    {
        if(!CheckIsDeadNow())
            _animator.SetTrigger("Dead");
    }

    private void Update()
    {
        if(photonView.IsMine)
        {
            if (CheckIsDeadNow()) return;

            UpdateAnimationState();
            
            if(_buttonAttack.IsPressed && !IsHitNow)
            {
                _animator.SetTrigger("Hit");
            }

            if (_buttonBlock.IsPressed)
                _animator.SetTrigger("Block");
        }
    }
    private void UpdateAnimationState()
    {
        IsHitNow = _animator.GetCurrentAnimatorStateInfo(0).IsName("Hit");
        IsBlockNow = _animator.GetCurrentAnimatorStateInfo(0).IsName("Block");
    }
    private bool CheckIsDeadNow()
    {
        return _animator.GetCurrentAnimatorStateInfo(0).IsName("Dead");
    }

}

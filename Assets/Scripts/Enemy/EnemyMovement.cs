using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

[RequireComponent(typeof(NavMeshAgent), typeof(EnemyAnimationSwitcher))]
public class EnemyMovement : MonoBehaviourPun
{
    private NavMeshAgent _navMeshAgent;
    private EnemyAnimationSwitcher __animationSwitcher;
    private Transform _target;
    private float _attackDistance = 1.6f;


    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        __animationSwitcher = GetComponent<EnemyAnimationSwitcher>();
        ChooseTarget();
    }

    private void Update()
    {
        if(photonView.IsMine)
        {
            if (_target == null) ChooseTarget();

            if (!__animationSwitcher.IsDead)
            {
                if (Vector3.Distance(transform.position, _target.position) <= _attackDistance)
                {
                    _navMeshAgent.destination = transform.position;
                    __animationSwitcher.SetHitAnimation();
                    transform.LookAt(_target);
                }
                else
                {
                    _navMeshAgent.destination = _target.position;
                    __animationSwitcher.SetMoveAnimation(true);
                }
            }
        }
    }
    private void ChooseTarget()
    {
        var targets = FindObjectsOfType<Movement>();
        _target = targets[Random.Range(0, targets.Length - 1)].transform;
    }
}

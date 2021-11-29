using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform _target;
    private Vector3 _offset;

    public void SetTarget(Transform target)
    {
        _target = target;
        _offset = transform.position - _target.position;
    }
    public void Update()
    {
        if(_target != null)
        {
            Vector3 newPosition = new Vector3(_target.position.x, transform.position.y, _target.position.z - 4);
            transform.position = newPosition;
        }
    }

}

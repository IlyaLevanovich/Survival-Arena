using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private float _angularSpeed;
    [SerializeField] private Vector3 _directionRotate;

    private void Update()
    {
        transform.rotation *= Quaternion.AngleAxis(_angularSpeed, _directionRotate);
    }
}

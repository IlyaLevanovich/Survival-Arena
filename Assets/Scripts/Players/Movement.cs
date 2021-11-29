using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody), typeof(AnimationSwitcher))]
public class Movement : MonoBehaviourPun, IPunObservable
{
    [SerializeField] private int _moveSpeed;
    private Rigidbody _rigidbody;
    private AnimationSwitcher _animationSwitcher;

    private FixedJoystick _joystick;
    public FixedJoystick Joystick { get { return _joystick; } set { _joystick = value; } }

    private Vector3 _correctPosition;
    private Quaternion _coorectRotation;


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_rigidbody.position);
            stream.SendNext(_rigidbody.rotation);
        }
        else
        {
            _correctPosition = (Vector3)stream.ReceiveNext();
            _coorectRotation = (Quaternion)stream.ReceiveNext();
        }
    }
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animationSwitcher = GetComponent<AnimationSwitcher>();
    }
    private void FixedUpdate()
    {
        if(photonView.IsMine)
        {
            float rotationAngle = Mathf.Atan2(_joystick.Horizontal, _joystick.Vertical) * Mathf.Rad2Deg;
            Vector3 joystickOffset = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);

            if (rotationAngle != 0)
            {
                _rigidbody.rotation = Quaternion.Euler(0, rotationAngle, 0);
                _rigidbody.MovePosition(transform.position + joystickOffset * _moveSpeed * Time.fixedDeltaTime);
                _animationSwitcher.SetMoveAnimation(true);
            }
            else
            {
                _animationSwitcher.SetMoveAnimation(false);
            }
        }
        else
        {
            _rigidbody.position = Vector3.Lerp(_rigidbody.position, _correctPosition, Time.fixedDeltaTime);
            _rigidbody.rotation = Quaternion.Lerp(_rigidbody.rotation, _coorectRotation, Time.fixedDeltaTime * 2);
        }
    }


}

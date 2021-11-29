using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviourPun
{
    [SerializeField] private GameObject _firstPlayer, _secondPlayer;
    [SerializeField] private Vector3 _spawnPosition;

    //For initialization spawned player
    [SerializeField] private FixedJoystick _movementJoystick;
    [SerializeField] private CameraFollow _cameraFolow;
    [SerializeField] private ClickHandler _buttonAttack, _buttonBlock;

    private void Start()
    {
        Spawn();
    }
    private void Spawn()
    {
        var player = PhotonNetwork.Instantiate(ChoosePlayer(), _spawnPosition, Quaternion.identity);

        player.GetComponent<Movement>().Joystick = _movementJoystick;
        player.GetComponent<AnimationSwitcher>().Init(_buttonAttack, _buttonBlock);

        _cameraFolow.SetTarget(player.transform);
    }
    private string ChoosePlayer()
    {
        GameObject player = PhotonNetwork.CurrentRoom.PlayerCount == 1 ? _firstPlayer : _secondPlayer;
        return player.name;
    }
}

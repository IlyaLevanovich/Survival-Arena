using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class RoomConnecting : MonoBehaviourPunCallbacks
{
    [SerializeField] private InputField _roomName;
    [SerializeField] private Text _log;

    private void Start()
    {
        PhotonNetwork.SendRate = 20;
        PhotonNetwork.SerializationRate = 10;

    }

    public void JoinRoom()
    {
        if (_roomName.text == "")
        {
            try { PhotonNetwork.JoinRandomRoom(); }
            finally { _log.text = "There ara currently no rooms available"; }
        }
        else
        {
            try { PhotonNetwork.JoinRoom(_roomName.text); }
            finally { _log.text = "Couldn't find a room with that name"; }
        }   
    }

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(_roomName.text) || _roomName.text.Length < 3)
        {
            _log.text = "Room name cannot be shorted than 3 characters";
        }
        else
        {
            RoomOptions options = new RoomOptions()
            {
                MaxPlayers = 2
            };
            PhotonNetwork.CreateRoom(_roomName.text, options);
        }
        
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}

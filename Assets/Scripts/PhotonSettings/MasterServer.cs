using UnityEngine;
using Photon.Pun;

public class MasterServer : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _loadingPanel;

    private void Start()
    {
        if(PhotonNetwork.IsConnected)
            PhotonNetwork.Disconnect();

        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnected()
    {
        _loadingPanel.SetActive(false);
    }
}

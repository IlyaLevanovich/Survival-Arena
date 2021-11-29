using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(MeshRenderer))]
public class Drop : MonoBehaviourPun
{
    public DropSpawner DropSpawner { get; set; }

    [SerializeField] private List<GameObject> _dropsObjects = new List<GameObject>();
    [SerializeField] private GameObject _bonusHealth;
    private int _strength = 100;

    public void TakeDamage(int damage)
    {
        if (photonView.IsMine)
        {
            _strength -= damage;
            if (_strength <= 0)
            {
                var shards = new GameObject[_dropsObjects.Count];
                for (int i = 0; i < shards.Length; i++)
                {
                    var shard = PhotonInstantiate(_dropsObjects[i]);
                    shards[i] = shard;
                }
                TryCreateBonus();
                DropSpawner.DestroyDropShards(shards, transform.position);
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
    private GameObject PhotonInstantiate(GameObject subject)
    {
        var drop = PhotonNetwork.Instantiate(subject.name, transform.position, Quaternion.identity);
        return drop;
    }
    private void TryCreateBonus()
    {
        if (Random.value > 0.7)
            PhotonInstantiate(_bonusHealth);
    }

}

using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;
using System.Threading.Tasks;
using System.Collections;

public class DropSpawner : MonoBehaviourPun
{
    [SerializeField] private GameObject _drop;
    [SerializeField] private List<Vector3> _spawnPositions = new List<Vector3>();
    

    private void Start()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
            StartCoroutine(CreateDrop(GetRandomPosition()));
    }
    public async void DestroyDropShards(GameObject[] shards, Vector3 position)
    {
        await Task.Delay(1500);
        foreach (var item in shards)
        {
            PhotonNetwork.Destroy(item);
        }
        _spawnPositions.Add(position);
        StartCoroutine(CreateDrop(GetRandomPosition()));
            
    }
    private IEnumerator CreateDrop(Vector3 position)
    {
        if (_spawnPositions.Count != 0)
        {
            yield return new WaitForSeconds(Random.Range(25, 60));

            var drop = PhotonNetwork.Instantiate(_drop.name, position, Quaternion.identity);
            drop.GetComponent<Drop>().DropSpawner = this;
            FixListPositions(position);
        }
    }
    private void FixListPositions(Vector3 value)
    {
        _spawnPositions.Remove(value);
    }
    private Vector3 GetRandomPosition()
    {
        return _spawnPositions[Random.Range(0, _spawnPositions.Count)];
    }
}

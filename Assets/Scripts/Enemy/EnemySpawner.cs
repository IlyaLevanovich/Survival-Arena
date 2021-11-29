using System.Collections;
using UnityEngine;
using Photon.Pun;

public class EnemySpawner : MonoBehaviourPun
{
    private const int _countEnemiesPerWave = 5;

    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _waitingPlayerText;
    
    private int _currentWave = 1;


    private void Start()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            _waitingPlayerText.SetActive(false);
            StartCoroutine(EnemySpawn(_currentWave));
        }    
    }

    private IEnumerator EnemySpawn(int wave)
    {
        for (int i = 0; i < _countEnemiesPerWave * wave; i++)
        {
            PhotonNetwork.Instantiate(_enemy.name, new Vector3(0, 1, 13), Quaternion.identity);
            yield return new WaitForSeconds(5);
        }
        _currentWave++;
        yield return new WaitForSeconds(30);
        StartCoroutine(EnemySpawn(_currentWave));
    }
}

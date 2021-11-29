using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BonusHealth : MonoBehaviourPun
{
    private const int _healthAmount = 20;
    private const int _lifeTime = 30;

    private void Start()
    {
        StartCoroutine(DestroyThisObject());
    }

    private void OnCollisionEnter(Collision collision)
    {
        var playerHealth = collision.gameObject.GetComponent<Health>();
        if(playerHealth != null)
        {
            playerHealth.ToHeal(_healthAmount);
            PhotonNetwork.Destroy(gameObject);
        }
    }

    private IEnumerator DestroyThisObject()
    {
        yield return new WaitForSeconds(_lifeTime);
        PhotonNetwork.Destroy(gameObject);
    }
}

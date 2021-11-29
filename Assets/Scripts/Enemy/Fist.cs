using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Fist : MonoBehaviour
{
    private const int _damage = 5;

    private void OnTriggerEnter(Collider collider)
    {
        var takeDamage = collider.GetComponent<ITakeDamage>();
        if(takeDamage != null)
        {
            takeDamage.TakeDamage(_damage);
        }
    }
}

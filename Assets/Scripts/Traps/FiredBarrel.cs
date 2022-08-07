using UnityEngine;

public class FiredBarrel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageReciever>(out var damagedObject))
        {
            damagedObject.ReciveBigDamage(gameObject.transform.position);
        }
    }
}

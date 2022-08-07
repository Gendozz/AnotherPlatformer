using UnityEngine;

[RequireComponent(typeof(Collider2D))] 
public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("OnTriggerEnter in " + (typeof(DamageDealer)));
        if (collision.TryGetComponent<IDamageReciever>(out IDamageReciever damageReciever))
        {
            damageReciever.RecieveSmallDamage(damage);
        }

        gameObject.SetActive(false);
    }
}


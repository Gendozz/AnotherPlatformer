using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HealthCollectible : MonoBehaviour
{
    [SerializeField]
    private int healthAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IHealable>() != null)
        {
            collision.GetComponent<IHealable>().RestoreHealth(healthAmount);
        }

        gameObject.SetActive(false);
    }
}

using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EndLevel : MonoBehaviour
{
    public static Action onPlayerGotToLevelEnd; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            onPlayerGotToLevelEnd?.Invoke();
        }
    }
}

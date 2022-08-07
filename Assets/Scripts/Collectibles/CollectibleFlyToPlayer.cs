using System.Collections;
using UnityEngine;

public class CollectibleFlyToPlayer : MonoBehaviour
{
    private Player player;

    private Vector3 startPosition;

    [SerializeField] private float timeToGetToPlayer;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnEnable()
    {
        startPosition = transform.position;
        StartCoroutine(MoveToPlayer());
    }

    private IEnumerator FlyToPlayer()
    {
        float timeElapsed = 0;

        Vector3 topPoint = new Vector3(Mathf.Abs(player.transform.position.x - transform.position.x), player.transform.position.y, transform.position.y);
        while(timeElapsed < timeToGetToPlayer)
        {
            timeElapsed += Time.deltaTime;

            Vector3 m1 = Vector3.Lerp(startPosition, topPoint, timeElapsed / timeToGetToPlayer);
            Vector3 m2 = Vector3.Lerp(topPoint, player.transform.position, timeElapsed / timeToGetToPlayer);
            transform.position = Vector3.Lerp(m1, m2, timeElapsed / timeToGetToPlayer);
        }
        yield return null;
    }

    private IEnumerator MoveToPlayer()
    {
        float timeElapsed = 0;


        while(timeElapsed < timeToGetToPlayer)
        {
            timeElapsed += Time.deltaTime;

            transform.position = Vector3.Lerp(startPosition, player.bonusCollectionPosition, timeElapsed / timeToGetToPlayer);

            yield return null;
        }   
    }
}

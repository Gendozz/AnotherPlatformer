using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ScoreCollectible : MonoBehaviour
{
    [SerializeField]
    private int scoreAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IScoreGetable>() != null)
        {
            collision.GetComponent<IScoreGetable>().AddScore(scoreAmount);

        }

        gameObject.SetActive(false);
    }
}

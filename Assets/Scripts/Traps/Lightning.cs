using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Animator))]
public class Lightning : MonoBehaviour
{
    [SerializeField] private float betweenHitsDelaySeconds;
    
    private Collider2D collider;

    private Animator lightningAnimator;

    //private float animationTime;

    private WaitForSeconds betweenHitsDelay;

    private WaitForSeconds animationDuration;

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        lightningAnimator = GetComponent<Animator>();
        betweenHitsDelay = new WaitForSeconds(betweenHitsDelaySeconds);

        RuntimeAnimatorController runtimeAnimatorController = lightningAnimator.runtimeAnimatorController;

        foreach (var item in runtimeAnimatorController.animationClips)
        {
            if (item.name.Equals("LightningSelf"))
            {
                animationDuration = new WaitForSeconds(item.length);
            }
        }

        StartCoroutine(TrapSequense());
    }

    private IEnumerator TrapSequense()
    {
        while (true)
        {
            yield return animationDuration;
            collider.enabled = false;
            yield return betweenHitsDelay;
            collider.enabled = true;
            lightningAnimator.SetTrigger("Hit");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.gameObject.TryGetComponent<IDamageReciever>(out var damageHandler))
        {
            damageHandler.ReciveBigDamage(gameObject.transform.position);
        }
    }


    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}

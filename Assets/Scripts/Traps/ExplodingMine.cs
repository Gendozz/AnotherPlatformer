using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ExplodingMine : MonoBehaviour
{
    [SerializeField] private float jumpDelay;

    [SerializeField] private float jumpForce;

    [SerializeField] private float angulaVelocityOnJumpRange;

    [SerializeField] private float explosionDelay;

    [SerializeField] private float detectionRadius;

    [SerializeField] private LayerMask detectionLayerMask;

    [SerializeField] private Animator mineAnimator;

    [SerializeField] private GameObject mineSprite;

    [SerializeField] private GameObject explosionSprite;

    [SerializeField]
    private float impactRadius;

    private Rigidbody2D rigidbody;

    private bool isCharged = true;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.simulated = false;
    }

    void Update()
    {
        if (isCharged)
        {
            Detect();  
        }    
    }

    private void Detect()
    {
        Collider2D detectedCollider = Physics2D.OverlapCircle(transform.position, detectionRadius, detectionLayerMask);
        if (detectedCollider != null)
        {
            isCharged = false;
            StartCoroutine(Explode(detectedCollider));
        }
    }

    private IEnumerator Explode(Collider2D impactedCollider)
    {
        float elapsedTime = 0;
        while(elapsedTime < jumpDelay)
        {
            elapsedTime += Time.deltaTime;
            mineAnimator.speed += (1 - elapsedTime / jumpDelay) * 0.05f;
            // TODO: sounds of beeping
            yield return null;
        }

        elapsedTime = 0;
        rigidbody.simulated = true;
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
        rigidbody.angularVelocity = Random.Range(-angulaVelocityOnJumpRange, angulaVelocityOnJumpRange);

        while(elapsedTime < explosionDelay)
        {
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        rigidbody.simulated = false;
        mineSprite.SetActive(false);
        explosionSprite.SetActive(true);
        mineAnimator.speed = 1;
        mineAnimator.SetBool("toExplode", true);

        //IApplyExternalImpact impactedObject= impactedCollider.GetComponent<IApplyExternalImpact>();
        //if (impactedObject != null && Physics2D.OverlapCircle(transform.position, impactRadius, detectionLayerMask))
        //{
        //    impactedObject.ApplyExternalImpact(new Vector2(explosionSprite.transform.position.x, explosionSprite.transform.position.y));            
        //}

        if(impactedCollider.TryGetComponent<IDamageReciever>(out var damageHandler) && Physics2D.OverlapCircle(transform.position, impactRadius, detectionLayerMask))
        {
            damageHandler.ReciveBigDamage(new Vector2(explosionSprite.transform.position.x, explosionSprite.transform.position.y));
        }
    }

    private void SetGameObjectActiveToFalse()
    {
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Color color = Color.red;

        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        color = Color.green;

        Gizmos.DrawWireSphere(explosionSprite.transform.position, impactRadius);

    }
}

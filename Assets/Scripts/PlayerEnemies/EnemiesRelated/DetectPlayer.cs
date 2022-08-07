using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Works only with one animation in animator
// TODO: Should work with several animations and detect player at specific

[RequireComponent(typeof(Animator), typeof(IShooter))]
public class DetectPlayer : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private Transform detectionOrigin;

    [SerializeField] private float detectionRadius;

    [SerializeField] private LayerMask playerMask;

    private IShooter shooter;

    void Start()
    {
        animator = GetComponent<Animator>();
        shooter = GetComponent<IShooter>();
    }

    private void Detect(int xDirection)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(detectionOrigin.position, detectionRadius, new Vector2(xDirection, 0), 5f, playerMask);

        if (hits.Length > 0)
        {
            Debug.Log($"{gameObject.name} detected player on {xDirection} xDirection");
            animator.speed = 0;
            shooter.Shoot(xDirection > 0 ? true : false);
            
            StartCoroutine(detectAnotherTime(xDirection));
        }
        else
        {
            animator.speed = 1;
        }      
    }

    private IEnumerator detectAnotherTime(int xDirection)
    {
        yield return new WaitForSeconds(0.5f);
        Detect(xDirection);
    }

}

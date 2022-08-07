using System.Collections;
using UnityEngine;

[RequireComponent(typeof(IDamagable))]
public class Enemy : MonoBehaviour, IDamageReciever, ICanDie
{
    [SerializeField] private int coinsCostOnKill;

    [SerializeField] private float spawnNextCoinDelay;

    private WaitForSeconds waitForNextCoinSpawn;

    IDamagable health;

    private SpriteRenderer spriteRenderer;

    private Coroutine dieRoutine;

    private void Start()
    {
        health = GetComponent<IDamagable>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        waitForNextCoinSpawn = new WaitForSeconds(spawnNextCoinDelay);
    }

    public void RecieveSmallDamage(int damageAmount = 1)
    {
        health.TakeDamage(damageAmount);

    }

    public void ReciveBigDamage(Vector2 fromDirection, int damageAmount = 1)
    {
        throw new System.NotImplementedException();
    }


    public void Die()
    {
        dieRoutine = StartCoroutine(DieSequence());
        spriteRenderer.enabled = false;
        GetComponent<Collider2D>().enabled = false;

    }

    private IEnumerator DieSequence()
    {
        yield return StartCoroutine(SpawnCoins(coinsCostOnKill));
        gameObject.SetActive(false);
    }

    private IEnumerator SpawnCoins(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            ObjectPooler.SharedInstance.SpawnFromPool(StringConsts.COIN, transform.position, false);
            yield return waitForNextCoinSpawn;
        }
    }


}

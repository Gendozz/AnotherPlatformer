using UnityEngine;

public class Health : MonoBehaviour, IDamagable, IHealable
{
    [SerializeField] private int maxLives;

    private int currentLives;

    private IHealthDisplayer healthDisplayer;

    void Start()
    {
        currentLives = maxLives;
        if (!TryGetComponent(out healthDisplayer))
        {
            healthDisplayer = GetComponentInChildren<IHealthDisplayer>();
        }
        if (healthDisplayer != null) healthDisplayer.ShowActualHealth(currentLives, maxLives);
    }

    public void TakeDamage(int damage)
    {
        currentLives -= damage;
        if (healthDisplayer != null) healthDisplayer.ShowActualHealth(currentLives, maxLives);
        if (currentLives <= 0) Die();
    }

    private void Die()
    {
        if (TryGetComponent<ICanDie>(out ICanDie canDieObject))
        {
            canDieObject.Die();
            return;
        }
        Debug.Log($"GameObject {gameObject.name} doesn't have specified Die behaviour and just deactivated");
        gameObject.SetActive(false);
    }

    public void RestoreHealth(int healthAmountToRestore)
    {
        currentLives += healthAmountToRestore;
        if (healthAmountToRestore + currentLives > maxLives)
        {
            currentLives = maxLives;
        }
        if (healthDisplayer != null) healthDisplayer.ShowActualHealth(currentLives, maxLives);
    }
}

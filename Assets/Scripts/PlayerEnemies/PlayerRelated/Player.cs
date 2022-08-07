using UnityEngine;
using System;
[RequireComponent(typeof(IDamagable), typeof(IShooter))]
[RequireComponent(typeof(PlayerInput), typeof(PlayerMovement), typeof(PlayerAnimation))]

public class Player : MonoBehaviour, IScoreGetable, IHealthDisplayer, IDamageReciever, ICanDie
{
    [SerializeField] private int maxRecieveDamage = 1;

    [SerializeField] private Transform bonusCollectionPoint;

    public Vector3 bonusCollectionPosition => bonusCollectionPoint.position;

    private int scores;
    public int CurrentScoreAmount { get { return scores; } }

    private PlayerMovement playerMovement;
    private PlayerAnimation playerAnimation;
    private PlayerInput playerInput;

    private IDamagable health;
    private IShooter shooter;

    public static Action<int> onPlayerLiveAmountChanged;
    public static Action onPlayerKilled;
    public static Action<int> onPlayerScoresAmountChanged;

    private void Start()
    {
        scores = 0;
        onPlayerScoresAmountChanged?.Invoke(scores);

        playerMovement = GetComponent<PlayerMovement>();
        playerInput = GetComponent<PlayerInput>();
        playerAnimation = GetComponent<PlayerAnimation>();
        health = GetComponent<IDamagable>();
        shooter = GetComponent<IShooter>();
    }

    private void Update()
    {
        if (playerInput.isAttackButtonPressed)
        {
            if (shooter.CanShoot)
            {
                shooter.Shoot(playerAnimation.IsLookingRight);
                playerAnimation.PlayShootAnimation();
            }
        }
    }

    public void AddScore(int scoresAmountToAdd)
    {
        scores += scoresAmountToAdd;
        onPlayerScoresAmountChanged?.Invoke(scores);
    }

    public void ShowActualHealth(int currentHealth, int maxHealth)
    {
        onPlayerLiveAmountChanged?.Invoke(currentHealth);
    }

    public void RecieveSmallDamage(int damageAmount)
    {
        health.TakeDamage(damageAmount);
    }

    public void ReciveBigDamage(Vector2 fromDirection, int damageAmount)
    {
        health.TakeDamage(damageAmount > maxRecieveDamage ? maxRecieveDamage : damageAmount);
        playerMovement.ApplyExternalImpact(fromDirection);
    }

    public void Die()
    {
        onPlayerKilled?.Invoke();
    }
}

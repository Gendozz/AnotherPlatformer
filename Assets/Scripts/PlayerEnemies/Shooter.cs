using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour, IShooter
{
    [SerializeField] private Transform rightFirePoint;

    [SerializeField] private Transform leftFirePoint;

    [SerializeField] private string projectilePoolerTag;

    [SerializeField] private float speedOfProjectileAlongX;

    [SerializeField] private float cooldownDuration;

    private WaitForSeconds waitForCooldown;

    public bool CanShoot { get; private set; } = true;

    private void Start()
    {
        waitForCooldown = new WaitForSeconds(cooldownDuration);
    }

    public void Shoot(bool toRight)
    {
        Transform currentFirePoint;

        if (toRight)
        {
            currentFirePoint = rightFirePoint;
        }
        else
        {
            currentFirePoint = leftFirePoint;
        }

        if (CanShoot)
        {
            GameObject spawnedObject = ObjectPooler.SharedInstance.SpawnFromPool(projectilePoolerTag, currentFirePoint.position, false);

            if (spawnedObject.TryGetComponent<IProjectile>(out IProjectile projectile))
            {
                projectile.SetFireParams(toRight, speedOfProjectileAlongX);
                CanShoot = false;
                StartCoroutine(CoolDown());
            }
            else
            {
                Debug.LogWarning($"Spawned object {gameObject.name} doesn't have IProjectile component");
            } 
        }
    }

    private IEnumerator CoolDown()
    {
        yield return waitForCooldown;
        CanShoot = true;
    }
}

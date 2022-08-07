using UnityEngine;

public interface IDamageReciever
{
    public void RecieveSmallDamage(int damageAmount = 1);

    public void ReciveBigDamage(Vector2 fromDirection, int damageAmount = 1);
}
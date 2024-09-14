using System;

public class EnemyRange
{
    private int health;
    private int damage;
    private int ammo;

    public EnemyRange(int health, int damage, int ammo)
    {
        this.health = health;
        this.damage = damage;
        this.ammo = ammo;
    }

    public void TakeDamage(int damageReceived)
    {
        health -= damageReceived;
        if (health < 0) health = 0;
    }

    public int GetDamage()
    {
        if (ammo > 0)
        {
            ammo--;
            return damage;
        }
        return 0;
    }

    public bool IsAlive()
    {
        return health > 0;
    }
}
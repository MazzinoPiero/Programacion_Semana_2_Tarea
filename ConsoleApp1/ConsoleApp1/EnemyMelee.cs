using System;

public class EnemyMelee
{
    private int health;
    private int damage;

    public EnemyMelee(int health, int damage)
    {
        this.health = health;
        this.damage = damage;
    }

    public void TakeDamage(int damageReceived)
    {
        health -= damageReceived;
        if (health < 0) health = 0;
    }

    public int GetDamage()
    {
        return damage;
    }

    public bool IsAlive()
    {
        return health > 0;
    }
}
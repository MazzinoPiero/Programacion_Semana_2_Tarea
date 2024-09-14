using System;

public class Player
{
    private int health;
    private int damage;

    public Player(int health, int damage)
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

    public int GetHealth()
    {
        return health;
    }
}
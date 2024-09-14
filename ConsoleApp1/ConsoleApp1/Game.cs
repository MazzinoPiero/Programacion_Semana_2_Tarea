using System;
using System.Collections.Generic;
using System.Linq;

public class Game
{
    private Player? player;
    private List<object>? enemiesList;

    public void Execute()
    {
            Console.WriteLine("Que empiece el Duelo XiaoLing");

            player = CreatePlayer();
            enemiesList = new List<object>
            {
                new EnemyMelee(150, 10),
                new EnemyRange(200, 15, 3),
                new EnemyMelee(60, 20),
                new EnemyRange(80, 8, 2)
            };

            Console.WriteLine($"{enemiesList.Count} enemigos estan delante de tuyo");

            while (player.GetHealth() > 0)
            {
                ShowGameStats();
                PlayerTurn();

                if (enemiesList.All(enemy => !IsAlive(enemy)))
                {
                    break;
                }

                EnemiesTurn();
            }

            if (player.GetHealth() > 0)
            {
                Console.WriteLine("Ganaste! TUs enemigos han sido vencidos");
            }
            else
            {
                Console.WriteLine("GAME OVER pipipi");
            }
        }

        private Player CreatePlayer()
        {
            Console.WriteLine("Dale vida a tu personaje:");
            Console.Write("Introduce la salud de tu personaje (máximo 100): ");
            int health = CheckValidInput(1, 100);

            Console.Write("Introduce el daño de tu personaje (máximo 100): ");
            int damage = CheckValidInput(1, 100);
            return new Player(health, damage);
        }

        private void ShowGameStats()
        {
            Console.WriteLine("Fight-fight");
            Console.WriteLine($"Vida restante: {player?.GetHealth()}. Poder de ataque: {player?.GetDamage()}");
            for (int i = 0; enemiesList != null && i < enemiesList.Count; i++)
            {
                Console.WriteLine($"Enemigo {i}: {(IsAlive(enemiesList[i]) ? "Vivo" : "Muerto")}");
            }
        }

        private void PlayerTurn()
        {
            Console.WriteLine("Tu turno:");
            int enemyChosen = CheckValidEnemy();
            int damageDealt = player?.GetDamage() ?? 0;
            if (enemiesList != null && enemyChosen >= 0 && enemyChosen < enemiesList.Count)
            {
                TakeDamage(enemiesList[enemyChosen], damageDealt);
                Console.WriteLine($"Causaste {damageDealt} de daño al enemigo {enemyChosen}");
            }
        }

        private void EnemiesTurn()
        {
            Console.WriteLine("Turno del enemigo:");
            foreach (var enemy in enemiesList?.Where(enemy => IsAlive(enemy)) ?? Enumerable.Empty<object>())
            {
                int damageCaused = GetDamage(enemy);
                player?.TakeDamage(damageCaused);
                Console.WriteLine($"Un enemigo te causó {damageCaused} de daño");
            }
        }

        private int CheckValidEnemy()
        {
            while (true)
            {
                Console.WriteLine("Elige al enemigo para atacar:");
                for (int i = 0; enemiesList != null && i < enemiesList.Count; i++)
                {
                    if (IsAlive(enemiesList[i]))
                    {
                        Console.WriteLine($"{i}: Enemigo vivo");
                    }
                }

                if (enemiesList != null && 
                    int.TryParse(Console.ReadLine(), out int enemyIndexChosen) && 
                    enemyIndexChosen >= 0 && 
                    enemyIndexChosen < enemiesList.Count && 
                    IsAlive(enemiesList[enemyIndexChosen]))
                {
                    return enemyIndexChosen;
                }
                Console.WriteLine("Número de enemigo inválido, elige un enemigo vivo.");
            }
        }

        private int CheckValidInput(int min, int max)
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int value) && value >= min && value <= max)
                {
                    return value;
                }
                Console.WriteLine($"Cantidad inválida, introduce un número entre {min} y {max}.");
            }
        }

        private bool IsAlive(object enemy)
        {
            return enemy switch
            {
                EnemyMelee melee => melee.IsAlive(),
                EnemyRange range => range.IsAlive(),
                _ => false
            };
        }

        private void TakeDamage(object enemy, int damage)
        {
            switch (enemy)
            {
                case EnemyMelee melee:
                    melee.TakeDamage(damage);
                    break;
                case EnemyRange range:
                    range.TakeDamage(damage);
                    break;
            }
        }

        private int GetDamage(object enemy)
        {
            return enemy switch
            {
                EnemyMelee melee => melee.GetDamage(),
                EnemyRange range => range.GetDamage(),
                _ => 0
        };
    }
}
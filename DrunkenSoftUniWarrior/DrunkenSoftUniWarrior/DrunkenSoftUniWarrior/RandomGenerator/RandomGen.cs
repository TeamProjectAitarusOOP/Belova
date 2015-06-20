using System;
using Microsoft.Xna.Framework;
using DrunkenSoftUniWarrior.Characters;
using DrunkenSoftUniWarrior.Items;
using DrunkenSoftUniWarrior.Items.Armors;
using DrunkenSoftUniWarrior.Items.Weapons;

namespace DrunkenSoftUniWarrior.RandomGenerator
{
    internal static class RandomGen
    {
        private static Random rand = new Random();
        private static Vector2 randomPosition;

        internal static Vector2 GetRandomPosition()
        {
            float randomX = rand.Next(DrunkenSoftUniWarrior.EnemySpawnAreaBeginX, DrunkenSoftUniWarrior.EnemySpawnAreaEndX);
            float randomY = rand.Next(DrunkenSoftUniWarrior.EnemySpawnAreaBeginY, DrunkenSoftUniWarrior.EnemySpawnAreaEndY);
            randomPosition = new Vector2(randomX, randomY);
            foreach (var unit in DrunkenSoftUniWarrior.Units)
            {
                float distance = Vector2.Distance(unit.Position, randomPosition);
                if (distance <= Enemy.EnemyChaseDistance)
                {
                    GetRandomPosition();
                }
            }
            return randomPosition;
        }

        internal static int GetRandomItemLevel()
        {
            switch (DrunkenSoftUniWarrior.Hero.Level)
            {
                case 1:
                    return rand.Next(1, 4);
                case 2:
                    return rand.Next(1, 5);
                case 3:
                    return rand.Next(1, 6);
                default:
                    return rand.Next(DrunkenSoftUniWarrior.Hero.Level - 2, DrunkenSoftUniWarrior.Hero.Level + 3);
            }
        }

        internal static void Drop(Vector2 position)
        {
            int numberOfItems = rand.Next(0, 3);

            switch (numberOfItems)
            {
                case 0:
                    DropRandomItem(position);
                    break;
                case 1:
                    DropRandomItem(position);
                    DropRandomItem(position);
                    break;
                case 2:
                    DropRandomItem(position);
                    DropRandomItem(position);
                    DropRandomItem(position);
                    break;
                default:
                    break;
            }
        }

        internal static void DropRandomItem(Vector2 position)
        {
            int randomX = rand.Next(0, 50) + (int)position.X;
            int randomY = rand.Next(0, 50) + (int)position.Y;
            int randomItem = rand.Next(0, 22);
            int randomLevel = GetRandomItemLevel();

            switch (randomItem)
            {
                case 0:
                    DrunkenSoftUniWarrior.Items.Add(new Sword(new System.Drawing.Point(randomX, randomY), randomLevel));
                    break;
                case 1:
                    DrunkenSoftUniWarrior.Items.Add(new Pants(new System.Drawing.Point(randomX, randomY), randomLevel));
                    break;
                case 2:
                case 3:
                case 4:
                    DrunkenSoftUniWarrior.Items.Add(new Potion(new System.Drawing.Point(randomX, randomY), randomLevel));
                    break;
                case 5:
                    DrunkenSoftUniWarrior.Items.Add(new Vest(new System.Drawing.Point(randomX, randomY), randomLevel));
                    break;
                case 6:
                    DrunkenSoftUniWarrior.Items.Add(new Shield(new System.Drawing.Point(randomX, randomY), randomLevel));
                    break;
                case 7:
                    DrunkenSoftUniWarrior.Items.Add(new Protectors(new System.Drawing.Point(randomX, randomY), randomLevel));
                    break;
                case 8:
                    DrunkenSoftUniWarrior.Items.Add(new Helmet(new System.Drawing.Point(randomX, randomY), randomLevel));
                    break;
                case 9:
                    DrunkenSoftUniWarrior.Items.Add(new Gloves(new System.Drawing.Point(randomX, randomY), randomLevel));
                    break;
                case 10:
                    DrunkenSoftUniWarrior.Items.Add(new Boots(new System.Drawing.Point(randomX, randomY), randomLevel));
                    break;
                case 11:
                    DrunkenSoftUniWarrior.Items.Add(new Axe(new System.Drawing.Point(randomX, randomY), randomLevel));
                    break;
                case 12:
                    DrunkenSoftUniWarrior.Items.Add(new Bow(new System.Drawing.Point(randomX, randomY), randomLevel));
                    break;
                case 13:
                    DrunkenSoftUniWarrior.Items.Add(new KillerAxe(new System.Drawing.Point(randomX, randomY), randomLevel));
                    break;
                case 14:
                    DrunkenSoftUniWarrior.Items.Add(new Mace(new System.Drawing.Point(randomX, randomY), randomLevel));
                    break;
                case 15:
                    DrunkenSoftUniWarrior.Items.Add(new Machete(new System.Drawing.Point(randomX, randomY), randomLevel));
                    break;
                case 16:
                    DrunkenSoftUniWarrior.Items.Add(new MagicStick(new System.Drawing.Point(randomX, randomY), randomLevel));
                    break;
                case 17:
                    DrunkenSoftUniWarrior.Items.Add(new Rifle(new System.Drawing.Point(randomX, randomY), randomLevel));
                    break;
                case 18:
                    DrunkenSoftUniWarrior.Items.Add(new ShoulderPad(new System.Drawing.Point(randomX, randomY), randomLevel));
                    break;
                case 19:
                case 20:
                case 21:
                    DrunkenSoftUniWarrior.Items.Add(new Potion(new System.Drawing.Point(randomX, randomY), randomLevel));
                    break;
                default:
                    break;
            }
        }
    }
}

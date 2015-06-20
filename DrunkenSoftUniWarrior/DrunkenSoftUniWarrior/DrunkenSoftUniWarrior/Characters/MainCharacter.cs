using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using DrunkenSoftUniWarrior.Items;
using DrunkenSoftUniWarrior.Interfaces;
using DrunkenSoftUniWarrior.RandomGenerator;
using DrunkenSoftUniWarrior.BackgroundObjects;
using System.Windows.Forms;
using System.Drawing;

namespace DrunkenSoftUniWarrior.Characters
{
    internal class MainCharacter : Character, IMovabble
    {
        private int expirienceRequiredForTheNextLevel;
        private SoundEffect swordHit;
        private bool isPlayed = true;
        private readonly string AssetMoveUp;
        private readonly string AssetMoveDown;
        private const float HeroSpeed = 2f;

        public MainCharacter(ContentManager content, string assetMoveLeft, string assetMoveRight, string assetMoveDown, string assetMoveUp, string assetHitLeft, string assetHitRight, Vector2 position, int level, float frameTime, int numberOfFrames, bool looping)
            : base(content, assetMoveLeft, assetMoveRight, assetHitLeft, assetHitRight, position, level, frameTime, numberOfFrames, looping)
        {
            this.IsAlive = true;
            this.Position = position;
            this.Damage = BaseDamage * 10;
            this.Expirience = 0;
            this.expirienceRequiredForTheNextLevel = 100;
            this.AssetMoveUp = assetMoveUp;
            this.AssetMoveDown = assetMoveDown;
            this.Inventory = new Item[2];
            this.swordHit = content.Load<SoundEffect>("Swing");
        }
        
        internal Item[] Inventory { get; set; }

        public void MoveUp()
        {
            if (IsAlive)
            {
                float x = this.Position.X;
                float y = this.Position.Y;
                y -= HeroSpeed;
                if (y < DrunkenSoftUniWarrior.MenuHeight)
                {
                    y = DrunkenSoftUniWarrior.MenuHeight;
                }
                this.Position = new Vector2(x, y);
            }
        }

        public void MoveDown()
        {
            if (IsAlive)
            {
                float x = this.Position.X;
                float y = this.Position.Y;
                y += HeroSpeed;
                if (y > DrunkenSoftUniWarrior.WindowHeight - DrunkenSoftUniWarrior.MenuHeight)
                {
                    y = DrunkenSoftUniWarrior.WindowHeight - DrunkenSoftUniWarrior.MenuHeight;
                }
                this.Position = new Vector2(x, y);
            }
        }

        public void MoveRight()
        {
            if (IsAlive)
            {
                float x = this.Position.X;
                float y = this.Position.Y;
                x += HeroSpeed;
                if (x > DrunkenSoftUniWarrior.WindowWidth - DrunkenSoftUniWarrior.MenuHeight)
                {
                    x = DrunkenSoftUniWarrior.WindowWidth - DrunkenSoftUniWarrior.MenuHeight;
                }
                this.Position = new Vector2(x, y);
            }
        }

        public void MoveLeft()
        {
            if (IsAlive)
            {
                float x = this.Position.X;
                float y = this.Position.Y;
                x -= HeroSpeed;
                if (x < 0)
                {
                    x = 0;
                }
                this.Position = new Vector2(x, y);  
            }
        }

        internal override void Awareness()
        {
            for (int index = 1; index < DrunkenSoftUniWarrior.Units.Count; index++)
            {
                float distanceFromEnemy = Vector2.Distance(this.Position, DrunkenSoftUniWarrior.Units[index].Position);
                if (distanceFromEnemy < HitDistance && DrunkenSoftUniWarrior.KeyBoard.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space))
                {
                    if (this.Position.X > DrunkenSoftUniWarrior.Units[index].Position.X)
                    {
                        this.ChangeAsset(this.Content, this.AssetHitLeft, 3);
                    }
                    else
                    {
                        this.ChangeAsset(this.Content, this.AssetHitRight, 3);
                    }
                    if (isPlayed)
                    {
                        swordHit.Play();
                        isPlayed = false;
                    }
                    Attack(index);
                }
            }
        }

        private void Attack(int index)
        {
            Enemy enemy = (Enemy)DrunkenSoftUniWarrior.Units[index];
            enemy.Health -= this.Damage - this.Damage * enemy.Armor / 175;
            if (enemy.Health <= 0)
            {
                this.isPlayed = true;
                this.GetExpirience(enemy.Expirience);
                this.ChangeAsset(this.Content, this.AssetMoveDown, 1);
                if (enemy is Nakov)
                {
                    Nakov.DropDomashyarka(enemy.Position);
                    DrunkenSoftUniWarrior.Units.Remove(enemy);
                    return;
                }
                RandomGen.Drop(enemy.Position);
                DrunkenSoftUniWarrior.Units.Remove(enemy);

            }
        }

        private void GetExpirience(int xp)
        {
            if (this.Expirience + xp >= this.expirienceRequiredForTheNextLevel)
            {
                this.LevelUp();
            }
            this.Expirience += xp;
        }

        private void LevelUp()
        {
            MenuBar.DamageButton.Visible = true;
            MenuBar.ArmorButton.Visible = true;
            this.Level++;
            this.Health = this.Level * BaseHealth;
            this.expirienceRequiredForTheNextLevel += this.expirienceRequiredForTheNextLevel * 2;
        }

        
    }
}

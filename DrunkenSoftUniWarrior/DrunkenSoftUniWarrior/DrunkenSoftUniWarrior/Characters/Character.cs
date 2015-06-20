using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using DrunkenSoftUniWarrior.Interfaces;

namespace DrunkenSoftUniWarrior.Characters
{
    internal abstract class Character : NPC, ISkills
    {
        protected readonly string AssetMoveLeft;
        protected readonly string AssetMoveRight;
        protected readonly string AssetHitLeft;
        protected readonly string AssetHitRight;
        protected const float HitDistance = 32.0f;
        protected const int BaseDamage = 2;
        protected const int BaseArmor = 10;
        protected const int BaseHealth = 1000;
        private const int BaseXP = 20;

        protected Character(ContentManager content, string assetMoveLeft, string assetMoveRight, string assetHitLeft, string assetHitRight, Vector2 position, int level, float frameTime, int numberOfFrames, bool looping)
            : base(content, assetMoveRight, position, frameTime, numberOfFrames, looping)
        {
            this.IsAlive = true;
            this.AssetMoveRight = assetMoveRight;
            this.AssetMoveLeft = assetMoveLeft;
            this.AssetHitLeft = assetHitLeft;
            this.AssetHitRight = assetHitRight;
            this.InitializeStats(level);
        }

        private void InitializeStats(int level)
        {
            this.Level = level;
            this.Damage = BaseDamage * this.Level;
            this.Health = BaseHealth * this.Level;
            this.Armor = BaseArmor * this.Level;
            this.Expirience = BaseXP * this.Level;
        }

        public int Level { get; set; }

        public int Expirience { get; set; }

        public double Health { get; set; }

        public double Armor { get; set; }

        public double Damage { get; set; }

        public bool IsAlive { get; set; }

        internal abstract void Awareness();
        
    }
}
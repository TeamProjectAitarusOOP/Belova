using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using DrunkenSoftUniWarrior.Enums;
using DrunkenSoftUniWarrior;

namespace DrunkenSoftUniWarrior.Characters
{
    internal class Enemy : Character
    {
        private EnemyState enemyState;
        private float enemyOrientation;
        private readonly float startPositionX;
        private readonly float startPositionY;
        public const float EnemyChaseDistance = 75.0f;
        public const float EnemyTurnSpeed = 2f;
        public const float EnemySpeed = 0.7f;

        public Enemy(ContentManager content, string assetMoveLeft, string assetMoveRight, string assetHitLeft, string assetHitRight, Vector2 position, int level, float frameTime, int numberOfFrames, bool looping)
            : base(content, assetMoveLeft, assetMoveRight, assetHitLeft, assetHitRight, position, level, frameTime, numberOfFrames, looping)
        {
            this.enemyState = EnemyState.Chill;
            this.Position = position;
            this.startPositionX = position.X;
            this.startPositionY = position.Y;
        }

        internal override void Awareness()
        {
            float distanceFromMainCharacter = Vector2.Distance(this.Position, DrunkenSoftUniWarrior.Hero.Position);
            ChangeState(distanceFromMainCharacter);
            float currentEnemySpeed;

            if (this.enemyState == EnemyState.Chasing)
            {
                if (DrunkenSoftUniWarrior.Hero.Position.X > this.Position.X)
                {
                    this.ChangeAsset(this.Content, this.AssetMoveRight, 4);
                }
                else
                {
                    this.ChangeAsset(this.Content, this.AssetMoveLeft, 4);
                }
                currentEnemySpeed = EnemySpeed;
                this.enemyOrientation = TurnToFace(this.Position, DrunkenSoftUniWarrior.Hero.Position, enemyOrientation, EnemyTurnSpeed);
            }
            else if (this.enemyState == EnemyState.Chill)
            {
                currentEnemySpeed = EnemySpeed;
                this.enemyOrientation = TurnToFace(this.Position, new Vector2(this.startPositionX, this.startPositionY), enemyOrientation, EnemyTurnSpeed);
                if (DrunkenSoftUniWarrior.Hero.Position.X > this.startPositionX)
                {
                    this.ChangeAsset(this.Content, this.AssetMoveLeft, 4);
                }
                else
                {
                    this.ChangeAsset(this.Content, this.AssetMoveRight, 4);
                }
                if (((this.startPositionX - this.Position.X) * (this.startPositionX - this.Position.X) +
                    (this.startPositionY - this.Position.Y) * (this.startPositionY - this.Position.Y)) <=
                    0.2 * 0.2)
                {
                    this.ChangeAsset(this.Content, this.AssetMoveRight, 1);
                    currentEnemySpeed = 0;
                }
            }
            else
            {
                if (DrunkenSoftUniWarrior.Hero.Position.X > this.Position.X)
                {
                    this.ChangeAsset(this.Content, this.AssetHitRight, 2);
                }
                else
                {
                    this.ChangeAsset(this.Content, this.AssetHitLeft, 2);
                }
                currentEnemySpeed = 0;
                this.Attack();                
                
            }
            Vector2 heading = new Vector2((float)Math.Cos(enemyOrientation), (float)Math.Sin(enemyOrientation));
            this.Position += heading * currentEnemySpeed;
        }

        private void ChangeState(float distanceFromMainCharacter)
        {
            if (distanceFromMainCharacter < EnemyChaseDistance && distanceFromMainCharacter > HitDistance)
            {
                this.enemyState = EnemyState.Chasing;
            }
            else if (distanceFromMainCharacter < HitDistance)
            {
                this.enemyState = EnemyState.Caught;
            }
            else
            {
                this.enemyState = EnemyState.Chill;
            }
        }

        private static float TurnToFace(Vector2 position, Vector2 faceThis, float currentAngle, float turnSpeed)
        {
            float x = faceThis.X - position.X;
            float y = faceThis.Y - position.Y;
            float desiredAngle = (float)Math.Atan2(y, x);
            float difference = WrapAngle(desiredAngle - currentAngle);
            difference = MathHelper.Clamp(difference, -turnSpeed, turnSpeed);
            return WrapAngle(currentAngle + difference);
        }

        private static float WrapAngle(float radians)
        {
            while (radians < -MathHelper.Pi)
            {
                radians += MathHelper.TwoPi;
            }
            while (radians > MathHelper.Pi)
            {
                radians -= MathHelper.TwoPi;
            }
            return radians;
        }

        private void Attack()
        {
            DrunkenSoftUniWarrior.Hero.Health -= this.Damage - this.Damage * DrunkenSoftUniWarrior.Hero.Armor / 175;
            if (DrunkenSoftUniWarrior.Hero.Health <= 0)
            {
                DrunkenSoftUniWarrior.Hero.IsAlive = false;
            }
        }
    }
}

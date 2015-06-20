using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using DrunkenSoftUniWarrior.Items.QuestItem;

namespace DrunkenSoftUniWarrior.Characters
{
    internal class Nakov : Enemy
    {

        public Nakov(ContentManager content, string assetMoveLeft, string assetMoveRight, string assetHitLeft, string assetHitRight, Vector2 position, int level, float frameTime, int numberOfFrames, bool looping)
            : base(content, assetMoveLeft, assetMoveRight, assetHitLeft, assetHitRight, position, level, frameTime, numberOfFrames, looping)
        {
            this.Health = this.Level * 2000;
        }

        internal static void DropDomashyarka(Vector2 position)
        {
            DrunkenSoftUniWarrior.Items.Add(new Domashnyarka(new System.Drawing.Point((int)position.X, (int)position.Y)));
        }
    }
}

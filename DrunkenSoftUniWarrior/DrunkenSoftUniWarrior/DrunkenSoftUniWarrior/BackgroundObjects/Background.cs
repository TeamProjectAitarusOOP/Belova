using DrunkenSoftUniWarrior.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DrunkenSoftUniWarrior.BackgroundObjects
{
    internal class Background : IDraw
    {
        public Background(ContentManager content, string asset, Rectangle baseRectangle)
        {
            this.Object = content.Load<Texture2D>(asset);
            this.BaseRectangle = baseRectangle;
        }

        public Texture2D Object { get; set; }

        public Rectangle BaseRectangle { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Object, this.BaseRectangle, Color.White);
        }
    }
}

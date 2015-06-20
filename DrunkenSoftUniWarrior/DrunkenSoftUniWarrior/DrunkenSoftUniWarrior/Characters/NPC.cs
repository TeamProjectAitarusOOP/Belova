using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using DrunkenSoftUniWarrior.Interfaces;

namespace DrunkenSoftUniWarrior.Characters
{
    internal class NPC : IAnimation, IDraw
    {
        private float frameTime;
        private float elapsedTime;
        private int currentFrame;
        private int numberOfFrames;
        private bool looping;
        private Rectangle sourceRectangle;
        private Texture2D characterTexture;

        public NPC(ContentManager content, string asset, Vector2 position, float frameTime, int numberOfFrames, bool looping)
        {
            this.Content = content;
            this.frameTime = frameTime;
            this.numberOfFrames = numberOfFrames;
            this.looping = looping;
            this.characterTexture = content.Load<Texture2D>(asset);
            this.FrameWidth = (characterTexture.Width / this.numberOfFrames); // calculate frame in asset
            this.FrameHeight = (characterTexture.Height); // frame hight base on NPC hight 
            this.Position = position;
        }

        internal Vector2 Position { get; set; }

        internal int FrameWidth { get; set; }

        internal int FrameHeight { get; set; }

        protected ContentManager Content { get; set; }

        public void playCharacterAnimation(GameTime gameTime)
        {
            this.elapsedTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            this.sourceRectangle = new Rectangle(this.currentFrame * this.FrameWidth, 0, this.FrameWidth, this.FrameHeight);
            if (this.elapsedTime >= this.frameTime)
            {
                if (this.currentFrame >= this.numberOfFrames - 1)
                {
                    if (this.looping)
                    {
                        this.currentFrame = 0;
                    }
                }
                else
                {
                    this.currentFrame++;
                }
                this.elapsedTime = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.characterTexture, this.Position, this.sourceRectangle, Color.White);
        }

        public void ChangeAsset(ContentManager content, string asset, int numberOfFrames)
        {
            this.characterTexture = content.Load<Texture2D>(asset);
            this.numberOfFrames = numberOfFrames;
        }
    }
}

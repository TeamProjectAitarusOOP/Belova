using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using DrunkenSoftUniWarrior.Items;
using DrunkenSoftUniWarrior.Characters;
using DrunkenSoftUniWarrior.RandomGenerator;
using DrunkenSoftUniWarrior.Enums;
using DrunkenSoftUniWarrior.BackgroundObjects;
using DrunkenSoftUniWarrior.Items.QuestItem;


namespace DrunkenSoftUniWarrior
{
    public class DrunkenSoftUniWarrior : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Background background;
        private Background gameOver;
        private Background win;
        private Random rand;
        private bool isHeroMoving;
        private Direction direction;
        private NPC drRadeva;
        private NPC sleepingGuy;
        private NPC selfieGuy;
        private MenuBar menu;
        private PictureBox story;
        private System.Drawing.Image storyImg;
        private Button startButton;
        private bool isKeyboardBlocked;
        private Song gameMusic;

        #region COLLISION

        private Rectangle ForestLeftOne;
        private Rectangle ForestLeftTwo;
        private Rectangle ForestLeftTre;
        private Rectangle ForestLeftFour;
        private Rectangle ForestLeftDown;
        private Rectangle SmallHouse;
        private Rectangle BigHouse;
        private Rectangle Well;
        private Rectangle ForestRight;

        private bool ForestLeftOneBool;
        private bool ForestLeftTwoBool;
        private bool ForestLeftTreBool;
        private bool ForestLeftFourBool;
        private bool ForestLeftDownBool;
        private bool SmallHouseBool;
        private bool BigHouseBool;
        private bool WellBool;
        private bool ForestRightBool;

        #endregion COLLISION

        private readonly Vector2 heroStartingPosition;
        private readonly Vector2 drRadevaStartingPosition;
        private readonly Vector2 selfieGuyStartingPosition;
        private readonly Vector2 sleepingGuyStartingPosition;
        public const int WindowHeight = 676;
        public const int WindowWidth = 1024;
        public const int MenuHeight = 50;
        public const int EnemySpawnAreaBeginX = 600;
        public const int EnemySpawnAreaBeginY = 250;
        public const int EnemySpawnAreaEndX = 774;
        public const int EnemySpawnAreaEndY = 590;
        private const int HeroLevelRequiredForNakov = 2;
        private const int StoryMargin = 69;
        private const int StartButtonWidth = 100;
        private const int StartButtonHeight = 20;
        private const string StoryPath = "Story.jpg";


        public DrunkenSoftUniWarrior()
        {
            Content.RootDirectory = "Content";
            this.graphics = new GraphicsDeviceManager(this);
            this.graphics.PreferredBackBufferHeight = WindowHeight;
            this.graphics.PreferredBackBufferWidth = WindowWidth;
            this.InitializeCollisionBorders();
            this.isHeroMoving = false;
            Units = new List<Character>();
            MaxUnitsOnTheMap = 5;
            Items = new List<Item>();
            this.rand = new Random();
            this.heroStartingPosition = new Vector2(DrunkenSoftUniWarrior.WindowWidth / 2, DrunkenSoftUniWarrior.MenuHeight);
            this.drRadevaStartingPosition = new Vector2(435, 450);
            this.selfieGuyStartingPosition = new Vector2(10, 480);
            this.sleepingGuyStartingPosition = new Vector2(730, 200);
            this.menu = new MenuBar();
            this.InitializeStory();
        }

        internal static List<Item> Items { get; set; }

        internal static List<Character> Units { get; set; }

        internal static MainCharacter Hero { get; set; }

        internal static KeyboardState KeyBoard { get; set; }

        internal static int MaxUnitsOnTheMap { get; set; }

        protected override void Initialize()
        {
            base.Initialize();
            this.IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            Hero = new MainCharacter(Content, "HeroMoveLeft", "HeroMoveRight", "HeroMoveDown", "HeroMoveUp", "HeroHitLeft", "HeroHitRight", this.heroStartingPosition, 1, 150f, 4, true);
            Units.Add(Hero);
            this.background = new Background(Content, "Background2", new Rectangle(0, MenuHeight, this.graphics.PreferredBackBufferWidth, this.graphics.PreferredBackBufferHeight));
            this.gameOver = new Background(Content, "GameOver", new Rectangle(0, 0, this.graphics.PreferredBackBufferWidth, this.graphics.PreferredBackBufferHeight));
            this.win = new Background(Content, "YouWin", new Rectangle(0, 0, this.graphics.PreferredBackBufferWidth, this.graphics.PreferredBackBufferHeight));
            this.AddMenu();
            this.AddNPCs();
            AddStory();
            gameMusic = Content.Load<Song>("GameMusic");
            MediaPlayer.Play(gameMusic);
        }

        protected override void Update(GameTime gameTime)
        {
            KeyBoard = Keyboard.GetState();
            this.MovePlayer(gameTime);
            if (!this.isHeroMoving)
            {
                Hero.ChangeAsset(Content, "HeroMoveDown", 1);
                Hero.playCharacterAnimation(gameTime);
            }

            this.CheckForCollision();

            if (Hero.Level < HeroLevelRequiredForNakov)
            {
                this.SpawnEnemy();
            }
            else if (Hero.Level >= HeroLevelRequiredForNakov && !Domashnyarka.isDropped)
            {
                this.SpawnNakov();
            }

            this.AddItemsOnTheGround();
            this.RemoveItemsOnTheGround();
            this.AnimateCharacters(gameTime);

            for (int index = 0; index < Units.Count; index++)
            {
                Units[index].Awareness();
            }

            UpdateMenu();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.GreenYellow);
            this.spriteBatch.Begin();

            if (!Hero.IsAlive)
            {
                this.GameOver();
            }
            else if (Domashnyarka.isUsed)
            {
                this.Win();
            }
            else
            {
                this.background.Draw(spriteBatch);
                this.drRadeva.Draw(spriteBatch);
                this.sleepingGuy.Draw(spriteBatch);
                this.selfieGuy.Draw(spriteBatch);
                Units.ForEach(unit => unit.Draw(this.spriteBatch));
            }
            this.spriteBatch.End();
            base.Draw(gameTime);
        }

        private void GameOver()
        {
            MediaPlayer.Stop();
            this.menu.DisposeMenu();
            foreach (var item in Hero.Inventory)
            {
                if (item != null)
                {
                    item.Dispose();
                }
            }
            Units.Clear();
            Items.ForEach(item => item.ItemStats.Dispose());
            Items.ForEach(item => item.Dispose());
            gameOver.Draw(spriteBatch);
        }

        private void Win()
        {
            MediaPlayer.Stop();
            this.menu.DisposeMenu();
            foreach (var item in Hero.Inventory)
            {
                if (item != null)
                {
                    item.Dispose();
                }
            }
            Items.ForEach(item => item.ItemStats.Dispose());
            Items.ForEach(item => item.Dispose());
            Units.Clear();
            win.Draw(spriteBatch);
        }

        private void AddNPCs()
        {
            this.drRadeva = new NPC(Content, "DrRadeva", this.drRadevaStartingPosition, 1500f, 2, true);
            this.sleepingGuy = new NPC(Content, "SleepingGuy", this.sleepingGuyStartingPosition, 500f, 3, true);
            this.selfieGuy = new NPC(Content, "SelfieGuy", this.selfieGuyStartingPosition, 600f, 3, true);
        }

        private void AddMenu()
        {
            Control.FromHandle(Window.Handle).Controls.Add(MenuBar.HealthBar);
            Control.FromHandle(Window.Handle).Controls.Add(MenuBar.Health);
            Control.FromHandle(Window.Handle).Controls.Add(MenuBar.Stats);
            Control.FromHandle(Window.Handle).Controls.Add(MenuBar.Inventory);
            Control.FromHandle(Window.Handle).Controls.Add(MenuBar.DamageButton);
            Control.FromHandle(Window.Handle).Controls.Add(MenuBar.ArmorButton);
            Control.FromHandle(Window.Handle).Controls.Add(MenuBar.HeroStats);
            Control.FromHandle(Window.Handle).Controls.Add(MenuBar.Weapon);
            Control.FromHandle(Window.Handle).Controls.Add(MenuBar.Armor);
        }

        private void InitializeCollisionBorders()
        {
            ForestLeftOne = new Rectangle(0, 0, 350, 150);
            ForestLeftTwo = new Rectangle(0, 130, 270, 70);
            ForestLeftTre = new Rectangle(0, 200, 240, 220);
            ForestLeftFour = new Rectangle(0, 400, 200, 50);
            ForestLeftDown = new Rectangle(0, 550, 150, 200);
            ForestRight = new Rectangle(900, 470, 400, 250);
            SmallHouse = new Rectangle(338, 340, 105, 130);
            BigHouse = new Rectangle(750, 62, 175, 140);
            Well = new Rectangle(713, 355, 41, 10);
        }

        private static void UpdateMenu()
        {
            MenuBar.HealthBar.Maximum = Hero.Level * 1000;
            MenuBar.HealthBar.ChangeSize(Math.Max((int)Hero.Health, 0));
            MenuBar.HeroStats.SetText(String.Format("Damage:{0}             Armor:{1}             Level:{2}",
                                        Hero.Damage, Hero.Armor, Hero.Level));
        }

        private void CheckForCollision()
        {
            ForestLeftOneBool =
                ForestLeftOne.Intersects(new Rectangle((int)Hero.Position.X, (int)Hero.Position.Y, Hero.FrameWidth, Hero.FrameHeight));
            ForestLeftTwoBool =
                ForestLeftTwo.Intersects(new Rectangle((int)Hero.Position.X, (int)Hero.Position.Y, Hero.FrameWidth, Hero.FrameHeight));
            ForestLeftTreBool =
                ForestLeftTre.Intersects(new Rectangle((int)Hero.Position.X, (int)Hero.Position.Y, Hero.FrameWidth, Hero.FrameHeight));
            ForestLeftFourBool =
                ForestLeftFour.Intersects(new Rectangle((int)Hero.Position.X, (int)Hero.Position.Y, Hero.FrameWidth, Hero.FrameHeight));
            ForestLeftDownBool =
                ForestLeftDown.Intersects(new Rectangle((int)Hero.Position.X, (int)Hero.Position.Y, Hero.FrameWidth, Hero.FrameHeight));
            SmallHouseBool =
                SmallHouse.Intersects(new Rectangle((int)Hero.Position.X, (int)Hero.Position.Y, Hero.FrameWidth, Hero.FrameHeight));
            BigHouseBool =
                BigHouse.Intersects(new Rectangle((int)Hero.Position.X, (int)Hero.Position.Y, Hero.FrameWidth, Hero.FrameHeight));
            WellBool =
                Well.Intersects(new Rectangle((int)Hero.Position.X, (int)Hero.Position.Y, Hero.FrameWidth, Hero.FrameHeight));
            ForestRightBool =
                ForestRight.Intersects(new Rectangle((int)Hero.Position.X, (int)Hero.Position.Y, Hero.FrameWidth, Hero.FrameHeight));

            if (ForestLeftTwoBool || ForestLeftOneBool || ForestLeftTreBool || ForestLeftFourBool
                || ForestLeftDownBool || SmallHouseBool || BigHouseBool || WellBool || ForestRightBool)
            {
                this.HitBorder();
            }
        }

        private void MovePlayer(GameTime gameTime)
        {
            if (KeyBoard.IsKeyDown(Keys.W) && Hero.IsAlive && !Domashnyarka.isUsed && !this.isKeyboardBlocked)
            {
                direction = Direction.Up;
                Hero.MoveUp();
                Hero.ChangeAsset(Content, "HeroMoveUp", 4);
                Units[0].playCharacterAnimation(gameTime);
                this.isHeroMoving = true;
            }
            else if (KeyBoard.IsKeyDown(Keys.D) && Hero.IsAlive && !Domashnyarka.isUsed && !this.isKeyboardBlocked)
            {
                direction = Direction.Right;
                Hero.MoveRight();
                Hero.ChangeAsset(Content, "HeroMoveRight", 4);
                Units[0].playCharacterAnimation(gameTime);
                this.isHeroMoving = true;
            }
            else if (KeyBoard.IsKeyDown(Keys.S) && Hero.IsAlive && !Domashnyarka.isUsed && !this.isKeyboardBlocked)
            {
                direction = Direction.Down;
                Hero.MoveDown();
                Hero.ChangeAsset(Content, "HeroMoveDown", 4);
                Units[0].playCharacterAnimation(gameTime);
                this.isHeroMoving = true;
            }
            else if (KeyBoard.IsKeyDown(Keys.A) && Hero.IsAlive && !Domashnyarka.isUsed && !this.isKeyboardBlocked)
            {
                direction = Direction.Left;
                Hero.MoveLeft();
                Hero.ChangeAsset(Content, "HeroMoveLeft", 4);
                Units[0].playCharacterAnimation(gameTime);
                this.isHeroMoving = true;
            }
            else if (KeyBoard.IsKeyDown(Keys.Space) && Hero.IsAlive && !Domashnyarka.isUsed && !this.isKeyboardBlocked)
            {
                Units[0].playCharacterAnimation(gameTime);
            }
        }

        private void AnimateCharacters(GameTime gameTime)
        {
            for (int index = 1; index < Units.Count; index++)
            {
                Units[index].playCharacterAnimation(gameTime);
            }

            this.sleepingGuy.playCharacterAnimation(gameTime);
            this.drRadeva.playCharacterAnimation(gameTime);
            this.selfieGuy.playCharacterAnimation(gameTime);
        }

        private void SpawnNakov()
        {
            MaxUnitsOnTheMap = 2;
            if (Units.Count < MaxUnitsOnTheMap)
            {
                Units.Add(new Nakov(Content, "NakovLeft", "NakovRight", "NakovHitLeft", "NakovHitRight", RandomGen.GetRandomPosition(), Hero.Level, 150f, 4, true));
            }
        }

        private void SpawnEnemy()
        {
            if (Units.Count < MaxUnitsOnTheMap)
            {
                switch (rand.Next(0, 3))
                {
                    case 0:
                        Units.Add(new Enemy(Content, "EnemyOneLeft", "EnemyOneRight", "EnemyOneHitLeft", "EnemyOneHitRight", RandomGen.GetRandomPosition(), Hero.Level, 150f, 4, true));
                        break;
                    case 1:
                        Units.Add(new Enemy(Content, "EnemyTwoLeft", "EnemyTwoRight", "EnemyTwoHitLeft", "EnemyTwoHitRight", RandomGen.GetRandomPosition(), Hero.Level, 150f, 4, true));
                        break;
                    case 2:
                        Units.Add(new Enemy(Content, "EnemyThreeLeft", "EnemyThreeRight", "EnemyThreeHitLeft", "EnemyThreeHitRight", RandomGen.GetRandomPosition(), Hero.Level, 150f, 4, true));
                        break;
                }
            }
        }

        private void AddItemsOnTheGround()
        {
            foreach (var item in Items)
            {
                Control.FromHandle(Window.Handle).Controls.Add(item);
                Control.FromHandle(Window.Handle).Controls.Add(item.ItemStats);
            }
        }

        private void RemoveItemsOnTheGround()
        {
            if (Items.Count > 6)
            {
                Items[0].ItemStats.Dispose();
                Items[0].Dispose();
                Items.RemoveAt(0);
            }
        }

        private void HitBorder()
        {
            if (direction == Direction.Left)
            {
                Hero.MoveRight();
            }
            else if (direction == Direction.Up)
            {
                Hero.MoveDown();
            }
            else if (direction == Direction.Right)
            {
                Hero.MoveLeft();
            }
            else if (direction == Direction.Down)
            {
                Hero.MoveUp();
            }
        }

        private void InitializeStory()
        {
            this.isKeyboardBlocked = true;
            int storyWidth = WindowWidth - StoryMargin * 2;
            int storyHeight = WindowHeight - StoryMargin * 2 - MenuHeight;
            this.story = new PictureBox();
            this.storyImg = new System.Drawing.Bitmap(StoryPath);
            this.story.Image = this.storyImg;
            this.story.SizeMode = PictureBoxSizeMode.Zoom;
            this.story.Size = new System.Drawing.Size(storyWidth, storyHeight);
            this.story.Location = new System.Drawing.Point(StoryMargin, MenuHeight + StoryMargin);
            this.startButton = new Button();
            this.startButton.Size = new System.Drawing.Size(StartButtonWidth, StartButtonHeight);
            this.startButton.Location = new System.Drawing.Point(storyWidth / 2, WindowHeight - StoryMargin / 2 - StartButtonHeight / 2);
            this.startButton.Text = "START GAME";
            this.startButton.MouseClick += startButton_MouseClick;
        }

        private void startButton_MouseClick(object sender, MouseEventArgs e)
        {
            this.isKeyboardBlocked = false;
            this.storyImg.Dispose();
            this.story.Dispose();
            this.startButton.Dispose();
        }

        private void AddStory()
        {
            Control.FromHandle(Window.Handle).Controls.Add(this.story);
            Control.FromHandle(Window.Handle).Controls.Add(this.startButton);
        }
    }
}

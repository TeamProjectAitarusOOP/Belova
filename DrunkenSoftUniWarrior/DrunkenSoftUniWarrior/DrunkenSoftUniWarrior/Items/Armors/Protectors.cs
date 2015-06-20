using System.Drawing;

namespace DrunkenSoftUniWarrior.Items.Armors
{
    internal class Protectors : Armor
    {
        private const string Path = "Protectors.jpg";

        public Protectors(Point position, int level)
            : base(position, level)
        {
            this.Picture = new Bitmap(Path);
            this.Defence = 1.8 * this.Level;
            this.ItemStats.Image = this.Picture;
            this.Image = resizeImage(this.Picture, new Size(ItemButtonSize, ItemButtonSize));
            this.ItemStats.Text = string.Format("{0}\n\nDefence: {1}\n\nLevel: {2}", this.GetType().Name, this.Defence.ToString(), this.Level);
        }
    }
}



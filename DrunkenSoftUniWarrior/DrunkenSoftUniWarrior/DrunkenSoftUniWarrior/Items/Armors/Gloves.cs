using System.Drawing;

namespace DrunkenSoftUniWarrior.Items.Armors
{
    internal class Gloves : Armor
    {
        private const string Path = "Gloves.jpg";

        public Gloves(Point position, int level)
            : base(position, level)
        {
            this.Picture = new Bitmap(Path);
            this.Defence = 0.8 * this.Level;
            this.ItemStats.Image = this.Picture;
            this.Image = resizeImage(this.Picture, new Size(ItemButtonSize, ItemButtonSize));
            this.ItemStats.Text = string.Format("{0}\n\nDefence: {1}\n\nLevel: {2}", this.GetType().Name, this.Defence.ToString(), this.Level);
        }
    }
}


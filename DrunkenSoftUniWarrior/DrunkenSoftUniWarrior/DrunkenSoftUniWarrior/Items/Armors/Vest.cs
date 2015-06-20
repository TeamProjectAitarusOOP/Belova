using System.Drawing;

namespace DrunkenSoftUniWarrior.Items.Armors
{
    internal class Vest : Armor
    {
        private const string Path = "ArmorVest.jpg";

        public Vest(Point position, int level)
            : base(position, level)
        {
            this.Picture = new Bitmap(Path);
            this.Defence = 3.5 * this.Level;
            this.ItemStats.Image = this.Picture;
            this.Image = resizeImage(this.Picture, new Size(ItemButtonSize, ItemButtonSize));
            this.ItemStats.Text = string.Format("{0}\n\nDefence: {1}\n\nLevel: {2}", this.GetType().Name, this.Defence.ToString(), this.Level);
        }
    }
}



using System.Drawing;

namespace DrunkenSoftUniWarrior.Items.Armors
{
    internal class Knife : Weapon
    {
        private const string Path = "Knife.jpg";

        public Knife(Point position, int level)
            : base(position, level)
        {
            this.Picture = new Bitmap(Path);
            this.Damage = 0.7 * this.Level;
            this.ItemStats.Image = this.Picture;
            this.Image = resizeImage(this.Picture, new Size(ItemButtonSize, ItemButtonSize));
            this.ItemStats.Text = string.Format("{0}\n\nDamage: {1}\n\nLevel: {2}", this.GetType().Name, this.Damage.ToString(), this.Level);
        }
    }
}



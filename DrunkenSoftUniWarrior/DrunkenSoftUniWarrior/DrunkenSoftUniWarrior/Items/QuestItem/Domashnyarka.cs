using System;
using System.Drawing;
using System.Windows.Forms;

namespace DrunkenSoftUniWarrior.Items.QuestItem
{
    internal class Domashnyarka : Potion
    {
        private const string Path = "Domashnyarka.jpg";

        public Domashnyarka(Point position, int level = 100) 
            : base(position, level)
        {
            isUsed = false;
            isDropped = true;
            this.Picture = new Bitmap(Path);
            this.ItemStats.Image = this.Picture;
            this.Image = resizeImage(this.Picture, new Size(ItemButtonSize, ItemButtonSize));
            this.ItemStats.Text = string.Format("Mysterious\npotion");
        }

        internal static bool isUsed { get; set; }

        internal static bool isDropped { get; set; }

        public override void itemButton_Click(object sender, EventArgs e)
        {
            PictureBox poition = (PictureBox)sender;
            this.ItemStats.Visible = false;
            poition.Dispose();
            isUsed = true;
        }
    }
}

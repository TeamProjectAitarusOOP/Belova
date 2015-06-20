using System;
using System.Drawing;
using DrunkenSoftUniWarrior.BackgroundObjects;

namespace DrunkenSoftUniWarrior.Items
{
    internal abstract class Weapon : Item
    {
        protected Weapon(Point position, int level)
            : base (position, level)
        {
        }

        public double Damage { get; set; }

        public override void itemButton_Click(object sender, EventArgs e)
        {
            this.Location = new Point(MenuBar.EndMargin + MenuBar.HeroStatsWidth + MenuBar.HealthBarWidth + MenuBar.SpaceBetweenSubmenus * 2 + MenuBar.WeaponWidth + 10, 
                                      MenuBar.SecondRowPositionY - (InventorySize - MenuBar.SecondRowHeight) / 2);
            this.Size = new Size(InventorySize, InventorySize);
            this.Image = resizeImage(this.Picture, new Size(InventorySize, InventorySize));
            this.ItemStats.Location = new Point(this.Location.X + InventorySize, this.Location.Y + InventorySize);
            if (DrunkenSoftUniWarrior.Hero.Inventory[0] == null)
            {
                DrunkenSoftUniWarrior.Hero.Inventory[0] = this;
            }
            else
            {
                Weapon weapon = (Weapon)DrunkenSoftUniWarrior.Hero.Inventory[0];
                DrunkenSoftUniWarrior.Hero.Damage -= weapon.Damage;
                DrunkenSoftUniWarrior.Hero.Inventory[0].Dispose();
                DrunkenSoftUniWarrior.Hero.Inventory[0] = this;
            }
            DrunkenSoftUniWarrior.Hero.Damage += this.Damage;
            DrunkenSoftUniWarrior.Items.Remove(this);
        }
    }
}

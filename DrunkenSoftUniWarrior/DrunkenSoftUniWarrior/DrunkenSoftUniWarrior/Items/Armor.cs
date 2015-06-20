using System;
using System.Drawing;
using DrunkenSoftUniWarrior.BackgroundObjects;

namespace DrunkenSoftUniWarrior.Items
{
    internal abstract class Armor : Item
    {
        protected Armor(Point position, int level)
            : base (position, level)
        {
        }

        public double Defence { get; set; }

        public override void itemButton_Click(object sender, EventArgs e)
        {
            this.Location = new Point(MenuBar.EndMargin + MenuBar.HeroStatsWidth + MenuBar.HealthBarWidth + MenuBar.SpaceBetweenSubmenus * 2 + MenuBar.WeaponWidth + 30 + Item.InventorySize + MenuBar.ArmorWidth, 
                                      MenuBar.SecondRowPositionY - (InventorySize - MenuBar.SecondRowHeight) / 2);
            this.Size = new Size(InventorySize, InventorySize);
            this.Image = resizeImage(this.Picture, new Size(InventorySize, InventorySize));
            this.ItemStats.Location = new Point(this.Location.X + InventorySize, this.Location.Y + InventorySize);
            if (DrunkenSoftUniWarrior.Hero.Inventory[1] == null)
            {
                DrunkenSoftUniWarrior.Hero.Inventory[1] = this;
            }
            else
            {
                Armor armor = (Armor)DrunkenSoftUniWarrior.Hero.Inventory[1];
                DrunkenSoftUniWarrior.Hero.Armor -= armor.Defence;
                DrunkenSoftUniWarrior.Hero.Inventory[1].Dispose();
                DrunkenSoftUniWarrior.Hero.Inventory[1] = this;
            }
            DrunkenSoftUniWarrior.Hero.Armor += this.Defence;
            DrunkenSoftUniWarrior.Items.Remove(this);
        }
    }
}

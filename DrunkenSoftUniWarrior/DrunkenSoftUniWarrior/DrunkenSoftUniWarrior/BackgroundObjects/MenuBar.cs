using System.Drawing;
using System.Windows.Forms;
using DrunkenSoftUniWarrior;
using DrunkenSoftUniWarrior.Items;

namespace DrunkenSoftUniWarrior.BackgroundObjects
{
    internal class MenuBar
    {
        internal const int EndMargin = 10;
        internal const int SecondRowHeight = 18;
        internal const int SecondRowPositionY = 25;
        internal const int ArmorWidth = 46;
        internal const int WeaponWidth = 62;
        internal const int HealthBarWidth = 200;
        internal const int HeroStatsWidth = 345;
        internal const int SpaceBetweenSubmenus = 90;
        private const int FirstRowHeight = 15;
        private const int FirstRowTextSize = 10;
        private const int FirstPositionY = 3;
        private const int SecondRowTextSize = 11;

        public MenuBar()
        {
            Health = new MenuLabel(EndMargin,
                                   FirstPositionY, 60, FirstRowHeight, FirstRowTextSize, "HEALTH");

            Stats = new MenuLabel(EndMargin + HealthBarWidth + SpaceBetweenSubmenus,
                                  FirstPositionY, 50, FirstRowHeight, FirstRowTextSize, "STATS");

            Inventory = new MenuLabel(EndMargin + HeroStatsWidth + HealthBarWidth + SpaceBetweenSubmenus * 2,
                                      FirstPositionY, 85, FirstRowHeight, FirstRowTextSize, "INVENTORY");

            HealthBar = new HealthBar(EndMargin,
                                      SecondRowPositionY, HealthBarWidth, SecondRowHeight);

            HeroStats = new MenuLabel(EndMargin + HealthBarWidth + SpaceBetweenSubmenus, SecondRowPositionY,
                                      HeroStatsWidth, SecondRowHeight, SecondRowTextSize);

            Weapon = new MenuLabel(EndMargin + HeroStatsWidth + HealthBarWidth + SpaceBetweenSubmenus * 2,
                                  SecondRowPositionY, WeaponWidth, SecondRowHeight, SecondRowTextSize, "Weapon");

            Armor = new MenuLabel(EndMargin + HeroStatsWidth + HealthBarWidth + SpaceBetweenSubmenus * 2 + WeaponWidth + 20 + Item.InventorySize,
                                  SecondRowPositionY, ArmorWidth, SecondRowHeight, SecondRowTextSize, "Armor");

            this.InitializeStatsButtons();
        }

        internal static HealthBar HealthBar { get; set; }

        internal static MenuLabel Health { get; set; }

        internal static MenuLabel Stats { get; set; }

        internal static MenuLabel HeroStats { get; set; }

        internal static MenuLabel Inventory { get; set; }

        internal static MenuLabel Weapon { get; set; }

        internal static MenuLabel Armor { get; set; }

        internal static Button DamageButton { get; set; }

        internal static Button ArmorButton { get; set; }

        public void DisposeMenu()
        {
            HealthBar.Dispose();
            Health.Dispose();
            Stats.Dispose();
            HeroStats.Dispose();
            Inventory.Dispose();
            DamageButton.Dispose();
            ArmorButton.Dispose();
            Weapon.Dispose();
            Armor.Dispose();
        }

        private void InitializeStatsButtons()
        {
            DamageButton = new Button();
            ArmorButton = new Button();
            DamageButton.Location = new System.Drawing.Point(EndMargin + HealthBarWidth + SpaceBetweenSubmenus - SecondRowHeight, MenuBar.SecondRowPositionY);
            ArmorButton.Location = new System.Drawing.Point(EndMargin + HealthBarWidth + SpaceBetweenSubmenus - SecondRowHeight + 127, MenuBar.SecondRowPositionY);
            DamageButton.Size = new System.Drawing.Size(SecondRowHeight, SecondRowHeight);
            ArmorButton.Size = new System.Drawing.Size(SecondRowHeight, SecondRowHeight);
            ArmorButton.Text = "+";
            DamageButton.Text = "+";
            ArmorButton.Font = new Font(ArmorButton.Font.FontFamily, 16);
            DamageButton.Font = new Font(DamageButton.Font.FontFamily, 16);
            ArmorButton.BringToFront();
            DamageButton.BringToFront();
            DamageButton.Visible = false;
            ArmorButton.Visible = false;
            DamageButton.MouseClick += DamageButton_MouseClick;
            ArmorButton.MouseClick += ArmorButton_MouseClick;
        }

        private void DamageButton_MouseClick(object sender, MouseEventArgs e)
        {
            DamageButton.Visible = false;
            ArmorButton.Visible = false;
            DrunkenSoftUniWarrior.Hero.Damage += 15;
        }

        private void ArmorButton_MouseClick(object sender, MouseEventArgs e)
        {
            DamageButton.Visible = false;
            ArmorButton.Visible = false;
            DrunkenSoftUniWarrior.Hero.Armor += 15;
        }
    }
}

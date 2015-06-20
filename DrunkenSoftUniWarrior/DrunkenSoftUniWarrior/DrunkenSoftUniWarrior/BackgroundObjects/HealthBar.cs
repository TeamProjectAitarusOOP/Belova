using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DrunkenSoftUniWarrior.BackgroundObjects
{
    internal class HealthBar : ProgressBar
    {
        public HealthBar(int positionX, int positionY, int width, int height)
            : base()
        {
            this.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.Location = new System.Drawing.Point(positionX, positionY);
            this.MarqueeAnimationSpeed = 1;
            this.Size = new System.Drawing.Size(width, height);
            this.TabIndex = 4;
        }

        public void ChangeSize(int health)
        {
            this.Value = health;
        }

    }
}

using System.Windows.Forms;


namespace DrunkenSoftUniWarrior.BackgroundObjects
{
    internal class MenuLabel : Label
    {
        public MenuLabel(int positionX, int positionY, int width, int height, int sizeText, string text)
            : base()
        {
            this.Location = new System.Drawing.Point(positionX, positionY);
            this.Text = text;
            this.Size = new System.Drawing.Size(width, height);
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", sizeText, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.Black;
        }

        public MenuLabel(int positionX, int positionY, int width, int height, int sizeText)
            : base()
        {
            this.Location = new System.Drawing.Point(positionX, positionY);
            this.Size = new System.Drawing.Size(width, height);
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", sizeText, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.Black;
        }

        public void SetText(string text)
        {
            this.Text = text;
        }
    }
}

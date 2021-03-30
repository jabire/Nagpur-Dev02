using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestWindowsForms
{
    public partial class UserControl2 : UserControl
    {
        private int mouseX;
        private int mouseY;

        //public int x = 60;
        //public int y = 150;
        //public int width = 30;
        //public int height = 50;
        public UserControl2()
        {
            InitializeComponent();
        }

        //System.Drawing.Rectangle recta = new Rectangle();

        //Rectangle rect = new Rectangle(60,150,30,50);

        public void UserControl2_Paint(object sender, PaintEventArgs e)
        {
            //Graphics g = e.Graphics;
            //Pen selPen = new Pen(Color.Red);
            //e.Graphics.DrawRectangle(selPen, rect);

            Rectangle rect = new Rectangle(mouseX - 25, mouseY - 25, 30, 30);
            //e.Graphics.DrawRectangle(redPen, rect);

            Pen selPen = new Pen(Color.Red);
            e.Graphics.DrawRectangle(selPen, rect);
        }

        private void UserControl2_MouseUp(object sender, MouseEventArgs e)
        {
            mouseX = e.X;
            mouseY = e.Y;
            this.Refresh();
        }
    }
}

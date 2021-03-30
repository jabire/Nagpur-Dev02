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
    public partial class UserControl7 : UserControl
    {
        public UserControl7()
        {
            InitializeComponent();
        }
        Rectangle rect = new Rectangle(310, 220, 60, 60);
        private void UserControl7_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen selPen = new Pen(Color.Red);
            e.Graphics.DrawRectangle(selPen, rect);
        }
    }
}

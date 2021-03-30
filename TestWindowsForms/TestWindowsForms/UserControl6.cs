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
    public partial class UserControl6 : UserControl
    {
        public UserControl6()
        {
            InitializeComponent();
        }
        Rectangle rect = new Rectangle(315, 140, 50, 50);
        private void UserControl6_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen selPen = new Pen(Color.Red);
            e.Graphics.DrawRectangle(selPen, rect);
        }
    }
}

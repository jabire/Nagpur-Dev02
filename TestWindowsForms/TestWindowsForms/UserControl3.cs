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
    public partial class UserControl3 : UserControl
    {
        public UserControl3()
        {
            InitializeComponent();
        }
        Rectangle rect = new Rectangle(170, 140, 30, 50);
        private void UserControl3_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen selPen = new Pen(Color.Red);
            e.Graphics.DrawRectangle(selPen, rect);
        }
    }
}

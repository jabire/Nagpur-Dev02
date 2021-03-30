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
    public partial class UserControl4 : UserControl
    {
        public UserControl4()
        {
            InitializeComponent();
        }
        Rectangle rect = new Rectangle(220, 150, 30, 30);
        private void UserControl4_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen selPen = new Pen(Color.Red);
            e.Graphics.DrawRectangle(selPen, rect);
        }
    }
}

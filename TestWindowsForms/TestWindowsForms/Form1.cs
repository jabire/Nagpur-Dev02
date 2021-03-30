using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace TestWindowsForms
{
    public partial class Form1 : Form
    {
        

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
(
    int nLeft,
    int nTop,
    int nRight,
    int nBottom,
    int nWidthEllipse,
    int nHeightEllipse
);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Select_F_Location.Region = Region.FromHrgn(CreateRoundRectRgn(1, 1, Select_F_Location.Width, Select_F_Location.Height, 10, 10));
            Save_Json.Region = Region.FromHrgn(CreateRoundRectRgn(1, 1, Save_Json.Width, Save_Json.Height, 10, 10));
            Refresh.Region = Region.FromHrgn(CreateRoundRectRgn(1, 1, Refresh.Width, Refresh.Height, 10, 10));


            userControl11.Show();
            userControl11.BringToFront();

            userControl21.Hide();
            userControl31.Hide();
            userControl41.Hide();
            userControl51.Hide();
            userControl61.Hide();
            userControl71.Hide();


            ////Adding images in list box from image list.
            //int i = 0;            
            //for (i = 0; i <= 5; i++)
            //{
            //    listBox1.Items.Add(imageList1.Images[i]);
            //}
        }
        private void button8_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.Blue, ButtonBorderStyle.Solid);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.Blue, ButtonBorderStyle.Solid);
        }
        /// <summary>
        /// Mouse event on hover of error id
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn30100_MouseHover(object sender, EventArgs e)
        {
            userControl21.Show();
            userControl21.BringToFront();

            userControl11.Hide();
            userControl31.Hide();
            userControl41.Hide();
            userControl51.Hide();
            userControl61.Hide();
            userControl71.Hide();
        }

        private void btn_30103_MouseHover(object sender, EventArgs e)
        {
            userControl31.Show();
            userControl31.BringToFront();

            userControl21.Hide();
            userControl11.Hide();
            userControl41.Hide();
            userControl51.Hide();
            userControl61.Hide();
            userControl71.Hide();
        }

        private void btn_30097_MouseHover(object sender, EventArgs e)
        {
            userControl41.Show();
            userControl41.BringToFront();

            userControl21.Hide();
            userControl31.Hide();
            userControl11.Hide();
            userControl51.Hide();
            userControl61.Hide();
            userControl71.Hide();
        }

        private void btn_30212_MouseHover(object sender, EventArgs e)
        {
            userControl51.Show();
            userControl51.BringToFront();

            userControl21.Hide();
            userControl31.Hide();
            userControl11.Hide();
            userControl41.Hide();
            userControl61.Hide();
            userControl71.Hide();
        }

        private void btn_30333_MouseHover(object sender, EventArgs e)
        {
            userControl61.Show();
            userControl61.BringToFront();

            userControl21.Hide();
            userControl31.Hide();
            userControl41.Hide();
            userControl51.Hide();
            userControl11.Hide();
            userControl71.Hide();
        }

        private void btn_30334_MouseHover(object sender, EventArgs e)
        {
            userControl71.Show();
            userControl71.BringToFront();

            userControl21.Hide();
            userControl31.Hide();
            userControl41.Hide();
            userControl51.Hide();
            userControl61.Hide();
            userControl11.Hide();
        }
        /// <summary>
        ///Mouse Event for Event Description.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_30100_MouseEnter(object sender, EventArgs e)
        {
            label4.Text = "Safe door open alarm";
        }

        private void btn_30100_MouseLeave(object sender, EventArgs e)
        {
            label4.Text = "";

        }

        private void btn_30103_MouseEnter(object sender, EventArgs e)
        {
            label4.Text = "E_Stop alarm";
        }

        private void btn_30103_MouseLeave(object sender, EventArgs e)
        {
            label4.Text = "";
        }

        private void btn_30097_MouseEnter(object sender, EventArgs e)
        {
            label4.Text = "Data check alarm";
        }

        private void btn_30097_MouseLeave(object sender, EventArgs e)
        {
            label4.Text = "";
        }

        private void btn_30212_MouseEnter(object sender, EventArgs e)
        {
            label4.Text = "Pressing_ForwardCylinder";
        }

        private void btn_30212_MouseLeave(object sender, EventArgs e)
        {
            label4.Text = "";
        }

        private void btn_30333_MouseEnter(object sender, EventArgs e)
        {
            label4.Text = "Inline_UpdownCylinder";
        }

        private void btn_30333_MouseLeave(object sender, EventArgs e)
        {
            label4.Text = "";
        }

        private void btn_30334_MouseEnter(object sender, EventArgs e)
        {
            label4.Text = "Pressing BoardCylinder";
        }

        private void btn_30334_MouseLeave(object sender, EventArgs e)
        {
            label4.Text = "";
        }
        private void Refresh_Click(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void Select_F_Location_Click(object sender, EventArgs e)
        {
            Image File;
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "Image files (*.jpg, *.png) | *.jpg; *.png";

            if (f.ShowDialog() == DialogResult.OK)
            {
                File = Image.FromFile(f.FileName);
                listBox1.Items.Add(f.SafeFileName);
                userControl11.Hide();
                userControl21.Hide();
                userControl31.Hide();
                userControl41.Hide();
                userControl51.Hide();
                userControl61.Hide();
                userControl71.Hide();
                pictureBox1.Image = File;
                pictureBox1.Image.Save(@"C:\Users\akros\Desktop\images1" + "\\" + f.SafeFileName);
                textBox1.Text = Convert.ToString(f.SafeFileName);
                //pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            }
        }
        private void Save_Json_Click(object sender, EventArgs e)
        {
            EventMapData eventMapData = new EventMapData()
            {
                FixtureName = "30100",
                errorId = 30100,
                imageName = "30100.jpeg",
                errorMessage = "Safe door open alarm",
                width = 30,
                height = 50,
                left = 60,
                top = 150
            };
            {

            };
            EventMapData eventMapData1 = new EventMapData()
            {
                FixtureName = "30103",
                errorId = 30103,
                imageName = "30103.jpeg",
                errorMessage = "E_Stop alarm",
                width = 30,
                height = 50,
                left = 170,
                top = 140
            };

            EventMapData eventMapData2 = new EventMapData()
            {
                FixtureName = "30097",
                errorId = 30097,
                imageName = "30097.jpeg",
                errorMessage = "Data check alarm",
                width = 30,
                height = 30,
                left = 220,
                top = 150
            };

            EventMapData eventMapData3 = new EventMapData()
            {
                FixtureName = "30212",
                errorId = 30212,
                imageName = "30212.jpeg",
                errorMessage = "Pressing_ForwardCylinder",
                width = 90,
                height = 60,
                left = 190,
                top = 100
            };

            EventMapData eventMapData4 = new EventMapData()
            {
                FixtureName = "30333",
                errorId = 30333,
                imageName = "30333.jpeg",
                errorMessage = "Inline_UpdownCylinder",
                width = 50,
                height = 50,
                left = 315,
                top = 140
            };

            EventMapData eventMapData5 = new EventMapData()
            {
                FixtureName = "30334",
                errorId = 30334,
                imageName = "30334.jpeg",
                errorMessage = "Pressing_BoardCylinder",
                width = 60,
                height = 60,
                left = 310,
                top = 220
            };
   
            string ResJson = JsonConvert.SerializeObject(eventMapData);
            string ResJson1 = JsonConvert.SerializeObject(eventMapData1);
            string ResJson2 = JsonConvert.SerializeObject(eventMapData2);
            string ResJson3 = JsonConvert.SerializeObject(eventMapData3);
            string ResJson4 = JsonConvert.SerializeObject(eventMapData4);
            string ResJson5 = JsonConvert.SerializeObject(eventMapData5);

            File.WriteAllText(@"EventMapData.json", ResJson);
            File.WriteAllText(@"EventMapData1.json", ResJson1);
            File.WriteAllText(@"EventMapData2.json", ResJson2);
            File.WriteAllText(@"EventMapData3.json", ResJson3);
            File.WriteAllText(@"EventMapData4.json", ResJson4);
            File.WriteAllText(@"EventMapData5.json", ResJson5);
            MessageBox.Show("Data Stored!");
        }
    }
}

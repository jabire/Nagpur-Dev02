using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static FixtureEventMapper.Utility;

namespace FixtureEventMapper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region privateVariables

        private Utility utility { get; set; }

        HitType MouseHitType = HitType.None;

        private bool DragInProgress = false;

        private Point LastPoint;

        public ObservableCollection<ImageDetails> ImageList = new ObservableCollection<ImageDetails>();

        List<EventMapData> AllEventMapData = new List<EventMapData>();

        private EventMapData selectedEventMapData;

        private ImageDetails selectedImage { get; set; }


        #endregion

        #region PublicVariables

        public ObservableCollection<EventMapData> SelectedEventMapDataCollection = new ObservableCollection<EventMapData>();

        public string selectedPath { get; set; }

        public EventMapData SelectedEventMapData
        {
            get
            {
                return selectedEventMapData;
            }
            set
            {                
                selectedEventMapData = value;               
                LoadErrorDetails(selectedEventMapData?.errorId);
            }
        }

        public ImageDetails SelectedImage
        {
            get
            {
                return selectedImage;
            }
            set
            {
                selectedImage = value;
                if (value != null)
                {
                    rectangle1.Visibility = Visibility.Hidden;
                    SetImage(value.ImagePath);
                    LoadEventDataBySelectedImage();
                }
            }
        }

        #endregion

        public MainWindow()
        {
            InitializeComponent();
            utility = new Utility();
            this.DataContext = this;
        }

        private void SetMouseCursor()
        {
            Cursor desired_cursor = utility.SetMouseCursor(MouseHitType);
            if (Cursor != desired_cursor) Cursor = desired_cursor;
        }

        private void RefreshImage(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                ImageList.Clear();
                string[] files = Directory.GetFiles(path, "*.png");
                foreach (var item in files)
                {
                    ImageDetails imageDetails = new ImageDetails { FileName = System.IO.Path.GetFileName(item), ImagePath = item };
                    ImageList.Add(imageDetails);
                }
                imageListBox.ItemsSource = ImageList;

                var fullpath = System.IO.Path.Combine(path, "eventMapData.json");
                AllEventMapData = JSonSerializer.LoadJson(fullpath);
            }

        }

        private void SetImage(string imagePath)
        {
            ImageBrush brush = new ImageBrush();
            brush.Stretch = Stretch.None;
            BitmapImage bitmapImage = new BitmapImage(new Uri(imagePath, UriKind.Relative));
            brush.ImageSource = bitmapImage;
            canvas1.Background = brush;
            canvas1.Width = bitmapImage.Width;
            canvas1.Height = bitmapImage.Height;

        }

        private void LoadRectangele(EventMapData eventMapData)
        {
            rectangle1.Visibility = Visibility.Visible;
            rectangle1.Width = eventMapData.width;
            rectangle1.Height = eventMapData.height;
            Canvas.SetLeft(rectangle1, eventMapData.left);
            Canvas.SetTop(rectangle1, eventMapData.top);
        }

        private void LoadEventDataBySelectedImage()
        {
            SelectedEventMapDataCollection = new ObservableCollection<EventMapData>(

                AllEventMapData.FindAll(x => x.imageName == SelectedImage.FileName)
                );

            EventIdList.ItemsSource = SelectedEventMapDataCollection;
        }

        private void LoadErrorDetails(int? ErrortId)
        {
            if (ErrortId.HasValue)
            {
                selectedEventMapData = SelectedEventMapDataCollection?.First<EventMapData>(x => x.errorId == ErrortId);
                var fullpath = System.IO.Path.Combine(selectedPath, selectedEventMapData.imageName);
                SetImage(fullpath);
                LoadRectangele(selectedEventMapData);
            }
        }


        private void canvas1_MouseDown(object sender, MouseButtonEventArgs e)
        {

            Rect rect = new Rect(Canvas.GetLeft(rectangle1), Canvas.GetTop(rectangle1), rectangle1.Width, rectangle1.Height);
            MouseHitType = utility.SetHitType(rectangle1, Mouse.GetPosition(canvas1), rect);
            SetMouseCursor();
            if (MouseHitType == HitType.None) return;

            LastPoint = Mouse.GetPosition(canvas1);
            DragInProgress = true;
        }

        // If a drag is in progress, continue the drag.
        // Otherwise display the correct cursor.
        private void canvas1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!DragInProgress)
            {
                Rect rect = new Rect(Canvas.GetLeft(rectangle1), Canvas.GetTop(rectangle1), rectangle1.Width, rectangle1.Height);
                MouseHitType = utility.SetHitType(rectangle1, Mouse.GetPosition(canvas1), rect);
                SetMouseCursor();
            }
            else
            {
                // See how much the mouse has moved.
                Point point = Mouse.GetPosition(canvas1);
                double offset_x = point.X - LastPoint.X;
                double offset_y = point.Y - LastPoint.Y;

                // Get the rectangle's current position.
                double new_x = Canvas.GetLeft(rectangle1);
                double new_y = Canvas.GetTop(rectangle1);
                double new_width = rectangle1.Width;
                double new_height = rectangle1.Height;

                // Update the rectangle.
                switch (MouseHitType)
                {
                    case HitType.Body:
                        new_x += offset_x;
                        new_y += offset_y;
                        break;
                    case HitType.UL:
                        new_x += offset_x;
                        new_y += offset_y;
                        new_width -= offset_x;
                        new_height -= offset_y;
                        break;
                    case HitType.UR:
                        new_y += offset_y;
                        new_width += offset_x;
                        new_height -= offset_y;
                        break;
                    case HitType.LR:
                        new_width += offset_x;
                        new_height += offset_y;
                        break;
                    case HitType.LL:
                        new_x += offset_x;
                        new_width -= offset_x;
                        new_height += offset_y;
                        break;
                    case HitType.L:
                        new_x += offset_x;
                        new_width -= offset_x;
                        break;
                    case HitType.R:
                        new_width += offset_x;
                        break;
                    case HitType.B:
                        new_height += offset_y;
                        break;
                    case HitType.T:
                        new_y += offset_y;
                        new_height -= offset_y;
                        break;
                }

                // Don't use negative width or height.
                if ((new_width > 0) && (new_height > 0))
                {
                    // Update the rectangle.
                    Canvas.SetLeft(rectangle1, new_x);
                    Canvas.SetTop(rectangle1, new_y);
                    rectangle1.Width = new_width;
                    rectangle1.Height = new_height;


                    if (selectedEventMapData != null)
                    {
                        selectedEventMapData.left = new_x;
                        selectedEventMapData.top = new_y;
                        selectedEventMapData.height = new_height;
                        selectedEventMapData.width = new_width;

                    }


                    // Save the mouse's new location.
                    LastPoint = point;
                }
            }

            // txtLocation.Text = rectangle1. + "  " + brush.AlignmentY;           
        }

        // Stop dragging.
        private void canvas1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DragInProgress = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var fbd = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = fbd.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {

                    selectedPath = fbd.SelectedPath;
                    RefreshImage(selectedPath);
                }
            }
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshImage(selectedPath);
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            ///LoadErrorDetails((sender as TextBlock).Text);
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            var fullpath = System.IO.Path.Combine(selectedPath, "eventMapData.json");
            if (JSonSerializer.SaveJson(AllEventMapData, fullpath))
            {
                MessageBox.Show("Saved Successfully");
            }
        }
    }
}

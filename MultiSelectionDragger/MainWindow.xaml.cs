using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MultiSelectionDragger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            List.MouseDown += List_MouseDown;
            List.MouseMove += List_MouseMove;
        }

        public string[] Items { get { return new[] { "Foo", "Bar", "Baz", "Muh", "Blah" }; } }

        #region Drag Drop Support

        Point startPoint;

        void List_MouseDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        void List_MouseMove(object sender, MouseEventArgs e)
        {
            // Get the current mouse position
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed
                && (
                    Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance
                    || Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                // Get the dragged ListViewItem
                var items = string.Join(", ", List.SelectedItems.Cast<string>().ToArray());

                // Initialize the drag & drop operation
                DataObject dragData = new DataObject(DataFormats.UnicodeText, items);
                DragDrop.DoDragDrop(List, dragData, DragDropEffects.Copy);
            }
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
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

namespace Ellie
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> ImagePaths = new List<string>();
        int index = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ViewNextImage(object sender, RoutedEventArgs e)
        {
            index++;
            if (index >= ImagePaths.Count)
            {
                index = 0;
            }
            string imagePath = ImagePaths[index];
            picHolder.Source = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
            UpdateTitle(imagePath);
        }

        private void ViewPreviousImage(object sender, RoutedEventArgs e)
        {
            index--;
            if (index < 0)
            {
                index = ImagePaths.Count - 1;
            }
            string imagePath = ImagePaths[index];
            picHolder.Source = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
            UpdateTitle(imagePath);
        }


        private void OpenFolderDialog(object sender, RoutedEventArgs e)
        {
            // Configure open folder dialog box
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.ShowDialog();
            string folderPath = dialog.SelectedPath;
			
			// when user doesn't select any folder
			if(folderPath.Length == 0)
			{
				return;
			}
            ImageFolder folder = new ImageFolder(folderPath);
            // filter images from all files
			ImagePaths = folder.ImagePaths;

            InitializeImageView();
        }

        private void InitializeImageView()
        {
			// When we have Images in folder
            if (ImagePaths.Count != 0)
            {
                string imagePath = ImagePaths[index];	// current Image
				// set new Image source
                picHolder.Source = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
                imageViewGrid.Visibility = Visibility.Visible;
                UpdateTitle(imagePath);
            }
            else
            {
                imageViewGrid.Visibility = Visibility.Hidden;
                ResetTitle();
            }
        }

        private void UpdateTitle(string str)
        {
            this.Title = $"{str} - Ellie";
        }

        private void ResetTitle()
        {
            this.Title = "Ellie";
        }
    }
}

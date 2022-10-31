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
        int index = 0;
        string CurrentImagePath = new string("");
        List<string> ImagePaths = new List<string>();

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
            NextImage();
        }

        private void NextImage()
        {
            index++;
            if (index >= ImagePaths.Count)
            {
                index = 0;
            }
            CurrentImagePath = ImagePaths[index];
            picHolder.Source = new BitmapImage(new Uri(CurrentImagePath, UriKind.Absolute));
            UpdateTitle(CurrentImagePath);
        }

        private void ViewPreviousImage(object sender, RoutedEventArgs e)
        {
            PreviousImage();
        }

        private void PreviousImage()
        {
            index--;
            if (index < 0)
            {
                index = ImagePaths.Count - 1;
            }
            CurrentImagePath = ImagePaths[index];
            picHolder.Source = new BitmapImage(new Uri(CurrentImagePath, UriKind.Absolute));
            UpdateTitle(CurrentImagePath);
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

                welcomeGrid.Visibility = Visibility.Hidden;
                editMenuItem.Visibility = Visibility.Visible;
                viewMenuItem.Visibility = Visibility.Visible;
                imageViewGrid.Visibility = Visibility.Visible;
                UpdateTitle(imagePath);
            }
            else
            {
				editMenuItem.Visibility = Visibility.Hidden;
                viewMenuItem.Visibility = Visibility.Hidden;
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

        private void copy_uri_clipboard(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(CurrentImagePath);
        }

    }
}

using Microsoft.Win32;
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
using Microsoft.WindowsAPICodePack.Dialogs;


namespace CopyImagesQuick
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string path_1, path_2, fileName;


        public MainWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;//CENTER THE SCREEN
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)//-->drag the window from the header border
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void changeFont(Label xLabel,string message)
        {
            xLabel.FontSize = 10;
            xLabel.Padding = new Thickness();
            xLabel.Foreground = new BrushConverter().ConvertFromString("#191f55") as SolidColorBrush;
            xLabel.Content = message;
        }


        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) //choose image
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg; *.jpg)|*.png;*.jpeg; *.jpg|All files (*.*)|*.*";
            //openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (openFileDialog.ShowDialog() == true)
            {
                changeFont(cImage, openFileDialog.FileName);            
                path_1 = System.IO.Path.GetFullPath(openFileDialog.FileName).ToString();
                //path_12 = System.IO.Path.GetTempPath();// 
                fileName = System.IO.Path.GetFileName(openFileDialog.FileName).ToString();


            }
        }

        private void Image_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e) //choose folder
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            CommonFileDialogResult result = dialog.ShowDialog();
            path_2 = dialog.FileName;      
            changeFont(cFolder, dialog.FileName); 


        }

        ///--->RESIZE THE BUTTON/IMAGES
        private void sImage_MouseEnter(object sender, MouseEventArgs e)
        {      
          (sender as Image).Width = sImage.Width * 1.03;
          (sender as Image).Height = sImage.Height * 1.03;
        } 
        private void sImage_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Image).Width = sImage.Width /1.03;
            (sender as Image).Height = sImage.Height / 1.03;
        }


        private void Image_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e) //save the copys
        {
           

            //  string fileName = "test.txt";
            string sourcePath = path_1;
            string targetPath = path_2; ///---->בהמשך ליצור תיקייה ולתוכה יהיה. אם קיימת כבר אז עם מספר..

            // Use Path class to manipulate file and directory paths.
            string sourceFile = path_1;
            string destFile= path_2;
           

            // If the directory already exists, this method does not create a new directory.
            System.IO.Directory.CreateDirectory(targetPath);

            // To copy a file to another location and 
            // overwrite the destination file if it already exists.
            for (int i = 1; i < (int.Parse(tImage.Text))+1; i++)
            {
                destFile = System.IO.Path.Combine(targetPath, i.ToString() + '_' + fileName);
                System.IO.File.Copy(sourceFile, destFile, true);
            }

            if (System.IO.Directory.Exists(sourcePath))
            {
                string[] files = System.IO.Directory.GetFiles(sourcePath);

                {
                    foreach (string s in files)
                    {
                        // Use static Path methods to extract only the file name from the path.
                        fileName = System.IO.Path.GetFileName(s);
                        destFile = System.IO.Path.Combine(targetPath, fileName);
                        System.IO.File.Copy(s, destFile, true);
                    }
                }
            }
            //else
            //{
            //    MessageBox.Show("Source path does not exist!");
            //}
            MessageBox.Show("Done");

         
        }
    }
}

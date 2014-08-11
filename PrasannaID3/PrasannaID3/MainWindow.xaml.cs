using Id3;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace PrasannaID3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            string startFolder = @"C:\Users\ppras_000\Desktop\Creating Lasting Change";

            DirectoryInfo di = new DirectoryInfo(startFolder);
            foreach (var dir in di.GetDirectories())
            {
                foreach (var file in dir.GetFiles("*.mp3"))
                {
                    using (FileStream fs = new FileStream(file.FullName, FileMode.Open))
                    {
                        using (var mp3 = new Mp3Stream(fs, Mp3Permissions.ReadWrite))
                        {
                            Id3Tag tag = mp3.GetTag(Id3TagFamily.Version2x);
                            tag.Title.Value = file.Name.Substring(0, 2);
                            tag.Album.Value = dir.Name.Substring(0, 6);
                            mp3.WriteTag(tag);

                        }
                    }
                }
            }



            MessageBox.Show("Done");
        }
    }
}

using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace BookCatalog1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new ApplicationViewModel();
            this.booksList.SelectionChanged += BooksList_SelectionChanged;   
        }

        private void BooksList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dc = this.DataContext as ApplicationViewModel;
            if(dc.Book == null)
            {
                this.ImageControl.Source = null;
            }
            else
            {
                this.ImageControl.Source = LoadImage(dc.Book.Image);
            }
        }

        private static BitmapImage LoadImage(byte[] imageData)                
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
    }
}

using System.Windows;
using Microsoft.Win32;
using System.IO;
using System.Windows.Media;

namespace BookCatalog1
{
    /// <summary>
    /// Логика взаимодействия для BookWindow.xaml
    /// </summary>
    public partial class BookWindow : Window
    {
        public Book Book { get; private set; }

        public BookWindow(Book b)
        {
            InitializeComponent();
            Book = b;
            this.DataContext = Book;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            string title = textBoxTitleBook.Text.Trim();
            string author = textBoxAuthor.Text.Trim();
            int yearRelease = int.Parse(textBoxYearRelease.Text.Trim());
            string isbn = textBoxISBN.Text.Trim();
            string description = textBoxDescription.Text.Trim();

            if (title.Length == 0)
            {
                textBoxTitleBook.ToolTip = "Поле не может быть пустым";
                textBoxTitleBook.Background = Brushes.Aquamarine;
            }
            else if(author.Length == 0) 
            {
                textBoxAuthor.ToolTip = "Поле не может быть пустым";
                textBoxAuthor.Background = Brushes.Aquamarine;
            }
            else if(yearRelease<1584 || yearRelease>=2023 || yearRelease.ToString().Length >4)
            {
                textBoxYearRelease.ToolTip = "Год выпуска не ранее 1584 года. Не может быть не наступившей датой и состоять больше чем из 4 цифр";
                textBoxYearRelease.Background = Brushes.Aquamarine;
            }
            else if(isbn.Length!=13)
            {
                textBoxISBN.ToolTip = "Состоит только из цифр. Количество цифр 13";
                textBoxISBN.Background = Brushes.Aquamarine;
            }
            else if(description.Length==0)
            {
                textBoxDescription.ToolTip = "Добавьте описание";
                textBoxDescription.Background = Brushes.Aquamarine;
            }
            else
            {
                textBoxTitleBook.ToolTip = "";
                textBoxTitleBook.Background = Brushes.Transparent;
                textBoxAuthor.ToolTip = "";
                textBoxAuthor.Background = Brushes.Transparent;
                textBoxYearRelease.ToolTip = "";
                textBoxYearRelease.Background = Brushes.Transparent;
                textBoxISBN.ToolTip = "";
                textBoxISBN.Background = Brushes.Transparent;
                textBoxDescription.ToolTip = "";
                textBoxDescription.Background = Brushes.Transparent;
                
                this.DialogResult = true;
            }        
        }

        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            var pathToImage = OpenImageDialog();

            if (!string.IsNullOrEmpty(pathToImage))
            {
                var imageBytes = File.ReadAllBytes(pathToImage);

                Book.Image = imageBytes;
            }
        }

        private string OpenImageDialog()
        {
            var dialog = new OpenFileDialog();

            dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                var pathToImage = dialog.FileName;

                return pathToImage;
            }

            return null;
        }
    }
}

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BookCatalog1
{
    public class Book : INotifyPropertyChanged              // МОДЕЛЬ ПРИЛОЖЕНИЯ
    {
                                 
        private string titleBook;               // Название книги
        private string author;                  // Автор
        private int yearRelease;                // Год выпуска
        private string isbn;                    // Международный стандартный книжный номер, состоит из 13 цифр
        private byte[] image;                   // Изображение обложки
        private string description;             // Краткое описание

        public int Id { get; set; }
        public string TitleBook
        {
            get { return titleBook; }
            set
            {
                titleBook = value;
                OnPropertyChanged("TitleBook");
            }
        }

        public string Author
        {
            get { return author; }
            set
            {
                author = value;
                OnPropertyChanged("Author");
            }
        }

        public int YearRelease
        {
            get { return yearRelease; }
            set
            {
                yearRelease = value;
                OnPropertyChanged("YearRelease");
            }
        }

        public string ISBN
        {
            get { return isbn; }
            set
            {
                isbn = value;
                OnPropertyChanged("ISBN");
            }
        }

        public byte[] Image
        {
            get { return image; }
            set
            {
                    image = value;
                    OnPropertyChanged("Image");
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

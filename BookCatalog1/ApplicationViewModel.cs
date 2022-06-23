using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Runtime.CompilerServices;

namespace BookCatalog1
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        public Book Book { get; set; }

        ApplicationContext db;
        RelayCommand addCommand;
        RelayCommand editCommand;
        RelayCommand deleteCommand;
        IEnumerable<Book> books;
 
        public IEnumerable<Book> Books
        {
            get { return books; }
            set
            {
                books = value;
                OnPropertyChanged("Books");
            }
        }
 
        public ApplicationViewModel()
        {
            db = new ApplicationContext();
            db.Books.Load();
            Books = db.Books.Local.ToBindingList();
        }
        // команда добавления
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand((o) =>
                  {
                      BookWindow bookWindow = new BookWindow(new Book());
                      if (bookWindow.ShowDialog() == true)
                      {
                          Book book = bookWindow.Book;
                          db.Books.Add(book);
                          db.SaveChanges();
                      }
                  }));
            }
        }
        // команда редактирования
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                  (editCommand = new RelayCommand((selectedItem) =>
                  {
                      if (selectedItem == null) return;
                      // получаем выделенный объект
                      Book book = selectedItem as Book;
 
                      Book b = new Book()
                      {
                          Id = book.Id,
                          TitleBook = book.TitleBook,
                          Author = book.Author,
                          YearRelease = book.YearRelease,
                          ISBN = book.ISBN,
                          Image = book.Image,
                          Description = book.Description
                      };
                      BookWindow bookWindow = new BookWindow(b);
 
 
                      if (bookWindow.ShowDialog() == true)
                      {
                          // получаем измененный объект
                          book = db.Books.Find(bookWindow.Book.Id);
                          if (book != null)
                          {
                              book.TitleBook = bookWindow.Book.TitleBook;
                              book.Author = bookWindow.Book.Author;
                              book.YearRelease = bookWindow.Book.YearRelease;
                              book.ISBN = bookWindow.Book.ISBN;
                              book.Image = bookWindow.Book.Image;
                              book.Description = bookWindow.Book.Description;
                              db.Entry(book).State = EntityState.Modified;
                              db.SaveChanges();
                          }
                      }
                  }));
            }
        }
        // команда удаления
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new RelayCommand((selectedItem) =>
                  {
                      if (selectedItem == null) return;
                      // получаем выделенный объект
                      Book book = selectedItem as Book;
                      db.Books.Remove(book);
                      db.SaveChanges();
                  }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

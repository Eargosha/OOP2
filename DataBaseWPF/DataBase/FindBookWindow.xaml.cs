using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace DataBase
{
    /// <summary>
    /// Логика взаимодействия для FindBookWindow.xaml
    /// </summary>
    public partial class FindBookWindow : Window
    {
        // в books принимаем значение всех книг из основного окна
        private ObservableCollection<Book> books;
        // в foundedBooks записываем найденные значения
        ObservableCollection<Book> foundedBooks;
        public FindBookWindow(ObservableCollection<Book> listOf)
        {
            InitializeComponent();
            // тут передае внешний список в внутрь класса окна
            books = listOf;
        }

        private void btnFindBook_Click(object sender, RoutedEventArgs e)
        {
            // Создаем новый список для хранения найденных книг
            foundedBooks = new ObservableCollection<Book>();

            // Проходим по всем книгам
            foreach (Book book in books)
            {
                bool match = true; // Флаг, указывающий на совпадение по всем критериям

                // Если поле имени книги заполнено и не совпадает с текущей книгой, пропускаем эту книгу
                if (!string.IsNullOrEmpty(textNameBookToFind.Text) && !book.Name.ToLower().Contains(textNameBookToFind.Text.ToLower()))
                {
                    match = false;
                }

                // Если поле автора книги заполнено и не совпадает с текущей книгой, пропускаем эту книгу
                if (!string.IsNullOrEmpty(textAutorBookToFind.Text) && !book.Autor.ToLower().Contains(textAutorBookToFind.Text.ToLower()))
                {
                    match = false;
                }

                // Если поле жанра книги заполнено и не совпадает с текущей книгой, пропускаем эту книгу
                if (!string.IsNullOrEmpty(textGenreBookToFind.Text) && !book.Genre.ToLower().Contains(textGenreBookToFind.Text.ToLower()))
                {
                    match = false;
                }

                // Если все условия выполнены, добавляем книгу в список найденных книг
                if (match)
                {
                    foundedBooks.Add(book);
                }
            }

            // Если найдены совпадения, отображаем их в DataGrid, иначе выводим сообщение
            if (foundedBooks.Count > 0)
            {
                dataGrid.ItemsSource = foundedBooks;
            }
            else
            {
                dataGrid.ItemsSource = null;
                MessageBox.Show(this, "Найдено 0 совпадений!", "Поиск книги", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

    }
}

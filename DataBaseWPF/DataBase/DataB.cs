using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    /// <summary>
    /// относится к Entity Framework, который является фреймворком объектно-реляционного отображения (ORM). 
    /// DbContext является основным классом в Entity Framework для взаимодействия с базой данных с использованием 
    /// подхода Code First. Это один из подходов в Entity Framework, который позволяет разработчикам сначала определять
    /// модели данных и их отношения в виде классов и свойств, а затем автоматически создавать соответствующую схему 
    /// базы данных на основе этих моделей. Этот подход позволяет разработчикам сосредоточиться на проектировании 
    /// объектной модели и обеспечивает более естественный способ работы с данными в коде.
    /// </summary>
    class DataB : DbContext
    {
        /// Список, внутри которого будут определенные элементы, что будут вытянуты из таблицы
        public DbSet<Book> Books { get; set; }

        /// Подключаемся к БД SqlLite через конструктор
        public DataB() : base("DefaultConnection") { }

        /// data - есть список с которым будет производиться работа в коде программы, чтобы постоянно не обращаться к базе данных SqlLiteЯ
        public ObservableCollection<Book> data;
       
        /// <summary>
        /// Находит книгу по id
        /// </summary>
        /// <param name="id">id по которому ищем</param>
        /// <returns>обьект типа Book, если не нашелся - null</returns>
        public Book findWithId(int id)
        {
            foreach(Book currentBook in data)
            {
                if (currentBook.id == id) return currentBook;
            }
            return null;
        }

        /// <summary>
        /// Метод сохранения данных в базу данных SqlLite
        /// </summary>
        /// <param name="filePath">путь сохранения</param>
        public void saveToFile(String filePath)
        {
            // Используем блок using для гарантированного освобождения ресурсов после завершения работы с соединением
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + filePath + ";Version=3;"))
            {
                // Открываем соединения
                connection.Open();

                // Создаем таблицу книг, если она еще не существует
                String createTableQuery = @"CREATE TABLE IF NOT EXISTS Books (
                                                id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                Name TEXT NOT NULL,
                                                Author TEXT,
                                                Genre TEXT,
                                                DepositPrice TEXT NOT NULL,
                                                RentalPrice TEXT NOT NULL,
                                                Status TEXT NOT NULL
                                            );";
                // Создаем новую команду SqlLite
                using (SQLiteCommand command = new SQLiteCommand(createTableQuery, connection))
                {
                    // Выполняем команду создания таблицы, написанную выше
                    command.ExecuteNonQuery();
                }

                // Пишем комаду удаления всех записей из SqlLite
                String deleteQuery = "DELETE FROM Books;";
                using (SQLiteCommand deleteCommand = new SQLiteCommand(deleteQuery, connection))
                {
                    // Выполняем команду удаления записей
                    deleteCommand.ExecuteNonQuery();
                }

                // Вставляем новые данные
                String insertQuery = "INSERT INTO Books (Id, Name, Author, Genre, DepositPrice, RentalPrice, Status) VALUES (@Id, @Name, @Author, @Genre, @DepositPrice, @RentalPrice, @Status);";
                foreach (var item in data)
                {
                    // Создаем команду вставки с параметрами
                    using (SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, connection))
                    {
                        // Связываем "переменные" SQL и переменные C#
                        insertCommand.Parameters.AddWithValue("@Id", item.id);
                        insertCommand.Parameters.AddWithValue("@Name", item.Name);
                        insertCommand.Parameters.AddWithValue("@Author", item.Autor);
                        insertCommand.Parameters.AddWithValue("@Genre", item.Genre);
                        insertCommand.Parameters.AddWithValue("@DepositPrice", item.DepositPrice);
                        insertCommand.Parameters.AddWithValue("@RentalPrice", item.RentalPrice);
                        insertCommand.Parameters.AddWithValue("@Status", item.Status);

                        // Выполняем команду, написанную выше
                        insertCommand.ExecuteNonQuery();
                    }
                }
                // Закрываем соединение, а то че оно
                connection.Close();
            }
        }

        public void loadFromFile(String filePath)
        {
            // Очищаем существующие данные в ObservableCollection перед загрузкой новых
            data.Clear();
            // Используем блок using для гарантированного освобождения ресурсов после завершения работы с соединением
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + filePath + ";Version=3;"))
            {
                // Открываем соединение
                connection.Open();
                // Запрос для выборки всех записей из таблицы Books
                String selectQuery = "SELECT * FROM Books;";
                // Задаем новую команду
                using (SQLiteCommand command = new SQLiteCommand(selectQuery, connection))
                {
                    // Выполнение команды и получение результата с помощью объекта SQLiteDataReader
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        // Чтение результатов выборки
                        while (reader.Read())
                        {
                            // Извлечение данных о книге из результата выборки
                            int id = reader.GetInt32(0);
                            String name = reader.GetString(1);
                            // Проверка на наличие значения в столбце Autor и Genre, так как они могут быть NULL в базе данных
                            String autor = reader.IsDBNull(2) ? null : reader.GetString(2);
                            String genre = reader.IsDBNull(3) ? null : reader.GetString(3);
                            String depositPrice = reader.GetString(4);
                            String rentalPrice = reader.GetString(5);
                            String status = reader.GetString(6);
                            // Создание объекта книги и добавление его в коллекцию данных
                            data.Add(new Book(id, name, autor, genre, depositPrice, rentalPrice, status));
                        }
                    }
                }
                // Закрытие соединения
                connection.Close();
            }
        }


        /// <summary>
        /// Метод добавления новой книги
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="autor">Автор</param>
        /// <param name="genre">Жанр</param>
        /// <param name="depositPrice">Цена депозита</param>
        /// <param name="rentalPrice">Цена аренды</param>
        /// <param name="status">Статус книги, готовность к выдаче</param>
        /// <param name="id">id</param>
        public void add_book(string name, string autor, string genre, string depositPrice, string rentalPrice, string status, int id)
        {
            data.Add(new Book(id+1, name, autor, genre, depositPrice, rentalPrice, status));
        }

        /// <summary>
        /// Метод удаления книги
        /// </summary>
        /// <param name="id">id по которому удаляется книга</param>
        public void remove_book(int id) 
        {
            if ((data.Count()-1 > id-1 || data.Count()-1 == id - 1) && (id > 0))
            {
                data.RemoveAt(id - 1);
            }
            else
                throw new ArgumentException("Can't ascess this index in list");
        }
    }
}

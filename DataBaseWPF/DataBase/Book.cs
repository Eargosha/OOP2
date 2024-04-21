using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    /// <summary>
    /// Книга - модель, что представляет одну строку таблицы
    /// </summary>
    public class Book
    {
        /// идентивикатор
        public int id { get; set; }
        /// имя, автор, жанр, залог, прокат, статус книги - готова/не готова к выдаче
        private String name, autor, genre, depositPrice, rentalPrice, status;

        /// Два конструктора
        public Book () { }

        public Book(int id, string name, string autor, string genre, string depositPrice, string rentalPrice, string status)
        {
            Name = name;
            this.id= id;
            Autor = autor;
            Genre = genre;
            DepositPrice = depositPrice;
            RentalPrice = rentalPrice;
            Status = status;
        }

        /// Сеттеры и геттеры многА
        public string Name { get => name; set => name = value; }
        public string Autor { get => autor; set => autor = value; }
        public string Genre { get => genre; set => genre = value; }
        public string DepositPrice { get => depositPrice; set => depositPrice = value; }
        public string RentalPrice { get => rentalPrice; set => rentalPrice = value; }
        public string Status { get => status; set => status = value; }
    }
}

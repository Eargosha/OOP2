using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AddBookWindow.xaml
    /// </summary>
    public partial class AddBookWindow : Window
    {
        /// задаются параметры, что будут приниматься из данного окна
        public String name { get; private set; }
        public String autor { get; private set; }
        public String genre { get; private set; }
        public String depositPrice { get; private set; }
        public String rentalPrice { get; private set; }
        public String status { get; private set; }

        public AddBookWindow()
        {
            InitializeComponent();
        }

        private void btnAddBookToList_Click(object sender, RoutedEventArgs e)
        {
            //Связываем ввод пользователя и переменные класса окна
            autor = textBoxAutor.Text;  //могут быть NULL
            genre = textBoxGenre.Text;
            status = (bool)checkBoxReady.IsChecked ? "Да" : "Нет";
            //Тут выполняются некоторые уловия ввода, проверка данных и показ пользователю, если их ввели неверно
            if (textBoxName.Text != "")
            {
                textBoxName.BorderBrush = new SolidColorBrush(Colors.LightGray);
                name = textBoxName.Text;
            }
            else
            {
                textBoxName.BorderBrush = new SolidColorBrush(Colors.IndianRed);
                return; // Отменить выполнение метода, чтобы окно не закрывалось при некорректных данных
            }

            if (Regex.IsMatch(textBoxDepositPrise.Text, @"^[0-9]+(\.[0-9]+)?\$?$"))
            {
                textBoxDepositPrise.BorderBrush = new SolidColorBrush(Colors.LightGray);
                depositPrice = textBoxDepositPrise.Text;
            }
            else
            {
                textBoxDepositPrise.BorderBrush = new SolidColorBrush(Colors.IndianRed);
                return; // Отменить выполнение метода, чтобы окно не закрывалось при некорректных данных
            }

            if (Regex.IsMatch(textBoxRentalPrice.Text, @"^[0-9]+(\.[0-9]+)?\$?$"))
            {
                textBoxRentalPrice.BorderBrush = new SolidColorBrush(Colors.LightGray);
                rentalPrice = textBoxRentalPrice.Text;
                this.DialogResult = true; // Закрыть окно только при корректных данных в обоих полях
            }
            else
            {
                textBoxRentalPrice.BorderBrush = new SolidColorBrush(Colors.IndianRed);
                return; // Отменить выполнение метода, чтобы окно не закрывалось при некорректных данных
            }
        } 
    }

}

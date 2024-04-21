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
    /// Interaction logic for EditBookWindow.xaml
    /// </summary>
    public partial class EditBookWindow : Window
    {
        public int id { get; private set; }
        public String name { get; private set; }
        public String autor { get; private set; }
        public String genre { get; private set; }
        public String depositPrice { get; private set; }
        public String rentalPrice { get; private set; }
        public String status { get; private set; }

        public EditBookWindow()
        {
            InitializeComponent();
        }

        private void btnChangeBookData_Click(object sender, RoutedEventArgs e)
        {
            name = textBoxName.Text;
            autor = textBoxAutor.Text;  //могут быть NULL
            genre = textBoxGenre.Text;
            depositPrice = textBoxDepositPrise.Text;
            rentalPrice = textBoxRentalPrice.Text;
            status = (bool)checkBoxReady.IsChecked ? "Да" : "Нет";
            this.Close();   
        }
    }
}

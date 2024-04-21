using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DataBase
{
    /// <summary>
    /// Interaction logic for DeleteBookWindow.xaml
    /// </summary>
    public partial class DeleteBookWindow : Window
    {
        public int id { get; private set; }
        public DeleteBookWindow()
        {
            InitializeComponent();
        }

        private void btnConfirmDelete_Click(object sender, RoutedEventArgs e)
        {
            int id2;
            if (int.TryParse(textIdBookToRemove.Text, out id2))
            {
                // Действия при успешном вводе
                textIdBookToRemove.BorderBrush = new SolidColorBrush(Colors.LightGray);
                id = id2;
            }
            else
            {
                // Действия при некорректном вводе
                textIdBookToRemove.BorderBrush = new SolidColorBrush(Colors.IndianRed);
                return; // Отменить выполнение метода, чтобы окно не закрывалось при некорректных данных
            }

            this.Close();
        }
    }
}

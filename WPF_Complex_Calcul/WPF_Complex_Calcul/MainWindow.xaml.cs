using Complex_calculator;
using System.Windows;
using System.Windows.Input;


/// Автор: Eargosha
namespace WPF_Complex_Calcul
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Класс есть partial т.к. его можно описать в разных файлах и во время компиляции он соберется в один, ака трансформер
        //"Частичный класс"
        //Window - родитель всех окон
        //Overide в toString потому что у родителя всех классов в C# - object есть уже свой toString
        //Для перегрузки операторов используется static, потому что оператор работает на уровне класса, а не на уровне определенного обьекта
        //Аля этот обьект + вот этот

        public MainWindow()
        {
            InitializeComponent();
        }

        //комплексные числа для вычислений
        //ссылка на объект
        ComplexNumber num_1 = new ComplexNumber();
        ComplexNumber num_2 = new ComplexNumber();
        ComplexNumber res_num = new ComplexNumber();

        /// <summary>
        /// Решает все что есть в программе и тут же выводит
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SolveAllFunction1(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            /*            ComplexNumber num_1 = new ComplexNumber((double)z1Real_input.Value, (double)z1Imag_input.Value);
                        ComplexNumber num_2 = new ComplexNumber((double)z2Real_input.Value, (double)z2Imag_input.Value);*/

            double num_1_r;
            double num_2_r;
            double num_1_i;
            double num_2_i;

            double.TryParse(z1Real_input.Text, out num_1_r);
            double.TryParse(z2Real_input.Text, out num_2_r);
            double.TryParse(z1Imag_input.Text, out num_1_i);
            double.TryParse(z2Imag_input.Text, out num_2_i);

            num_1.Real = num_1_r;
            num_2.Real = num_2_r;
            num_1.Imaginary = num_1_i;
            num_2.Imaginary = num_2_i;

            /*            num_1.Real = (double)z1Real_input.Value;
                        num_2.Real = (double)z2Real_input.Value;
                        num_1.Imaginary = (double)z1Imag_input.Value;
                        num_2.Imaginary = (double)z2Imag_input.Value;*/

            res_num = num_1 + num_2;
            Sum_holder.Text = res_num.ToString();

            res_num = num_1 - num_2;
            Sub_holder.Text = res_num.ToString();

            res_num = num_1 * num_2;
            Mult_holder.Text = res_num.ToString();

            if ((num_2.Real == 0) || (num_2.Imaginary == 0))
                Div_holder.Text = "Делить на 0 нельзя";
            else
            {
                res_num = num_1 / num_2;
                Div_holder.Text = res_num.ToString();
            }

            Mod_holder.Text = $"{num_1.Module():F4}";
            Arg_holder.Text = $"{num_1.Argument():F4}";
        }
    }
}

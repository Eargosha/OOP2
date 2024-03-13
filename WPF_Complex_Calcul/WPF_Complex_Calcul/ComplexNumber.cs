using System;

/// Автор: Eargosha

/// Пространство имен, где хранится весь код калькулятора
namespace Complex_calculator
{
    /// <summary>
    /// Класс ComplexNumber (Комплексное число), назначение: произведение расчетов с комплексными числами,
    /// пример использования: создание калькулятора комплексных чисел
    /// поля: double Real, double Imaginary
    /// конструкторы: ComplexNumber(double real, double imaginary) | ComplexNumber()
    /// методы: object.Module() | object.Argument() | object.ToString()
    /// операторы: +, -, /, *
    /// </summary>
    public class ComplexNumber
    {
        /// <summary>
        /// Вещественная часть комплексного числа, доступ можно получить напрямую
        /// или через префиксы: .get, .set
        /// </summary>
        public double Real {get; set;}
        /// <summary>
        /// Мнимая часть комплексного числа, доступ можно получить напрямую
        /// или через префиксы: .get, .set
        /// </summary>
        public double Imaginary {get; set;}


        /// <summary>
        /// Конструктор обьекта для класса ComplexNumber
        /// </summary>
        /// <param name="real">Вещественная часть комплексного числа</param>
        /// <param name="imaginary">Мнимая часть комплексного числа</param>
        public ComplexNumber(double real, double imaginary) //так как поля класса в области паблик, то для этих конструкторов тест не делался
        {
            Real = real;
            Imaginary = imaginary;
        }
        
        /// <summary>
        /// Пустой конструктор для класса ComplexNumber
        /// </summary>
        public ComplexNumber() {}


        /// <summary>
        /// Перегрузка оператора "+" для класса ComplexNumber
        /// </summary>
        /// <param name="num1">Слагаемое #1 - комплексное число класса ComplexNumber</param>
        /// <param name="num2">Слагаемое #2 - комплексное число класса ComplexNumber</param>
        /// <returns>Сумма - комплексное число типа ComplexNumber</returns>
        // Static указывает на то, что перегруженный оператор работает на уровне 
        // класса, а не на уровне объекта
        public static ComplexNumber operator + (ComplexNumber num1, ComplexNumber num2)
        {
            return new ComplexNumber(num1.Real + num2.Real, num1.Imaginary + num2.Imaginary) ;
        }

        /// <summary>
        /// Перегрузка оператора "-" для класса ComplexNumber
        /// </summary>
        /// <param name="num1">Уменьшаемое - комплексное число класса ComplexNumber</param>
        /// <param name="num2">Вычитаемое - комплексное число класса ComplexNumber</param>
        /// <returns>Разность - комплексное число типа ComplexNumber</returns>
        // Static указывает на то, что перегруженный оператор работает на уровне 
        // класса, а не на уровне объекта
        public static ComplexNumber operator -(ComplexNumber num1, ComplexNumber num2)
        {
            return new ComplexNumber(num1.Real - num2.Real, num1.Imaginary - num2.Imaginary);
        }

        /// <summary>
        /// Перегрузка оператора * для класса ComplexNumber
        /// </summary>
        /// <param name="num1">Множитель #1 - комплексное число класса ComplexNumber</param>
        /// <param name="num2">Множитель #2 - комплексное число класса ComplexNumber</param>
        /// <returns>Произведение - комплексное число типа ComplexNumber</returns>
        // Static указывает на то, что перегруженный оператор работает на уровне 
        // класса, а не на уровне объекта
        public static ComplexNumber operator *(ComplexNumber num1, ComplexNumber num2)
        {
            return new ComplexNumber(num1.Real * num2.Real - num1.Imaginary * num2.Imaginary, num1.Real * num2.Imaginary + num1.Imaginary * num2.Real);
        }

        /// <summary>
        /// Перегрузка оператора / для класса ComplexNumber
        /// </summary>
        /// <param name="num1">Делимое - комплексное число класса ComplexNumber</param>
        /// <param name="num2">Делитель - комплексное число класса ComplexNumber</param>
        /// <returns>Частное - комплексное число типа ComplexNumber</returns>
        // Static указывает на то, что перегруженный оператор работает на уровне 
        // класса, а не на уровне объекта
        public static ComplexNumber operator /(ComplexNumber num1, ComplexNumber num2)
        {
            return new ComplexNumber((num1.Real * num2.Real + num1.Imaginary * num2.Imaginary) / (Math.Pow(num2.Real , 2) + Math.Pow(num2.Imaginary, 2)), (num1.Imaginary * num2.Real - num1.Real * num2.Imaginary) / (Math.Pow(num2.Real, 2) + Math.Pow(num2.Imaginary, 2)));
        }


        /// <summary>
        /// Находит модуль комплексного числа
        /// </summary>
        /// <returns>Ответ типа double</returns>
        public double Module()
        {
            return Math.Sqrt(Math.Pow(this.Real, 2) + Math.Pow(this.Imaginary, 2));
        }


        /// <summary>
        /// Находит аргумент комплексного числа с учетом полуплоскости чисел a, b
        /// </summary>
        /// <returns>Ответ типа double</returns>
        public double Argument()
        {

            if (this.Real > 0)
            {
                return Math.Atan(this.Imaginary / this.Real);
            }
            if (this.Real < 0)
            {
                if (this.Imaginary >= 0)
                {
                    return Math.PI + Math.Atan(this.Imaginary / this.Real);
                }
                else if (this.Imaginary < 0)
                {
                    return -Math.PI + Math.Atan(this.Imaginary / this.Real);
                }
            }
            if (this.Real == 0)
            {
                if (this.Imaginary > 0)
                {
                    return Math.PI / 2;
                }
                if (this.Imaginary < 0)
                {
                    return -Math.PI / 2;
                }
            }
            return 0;
        }


        /// <summary>
        /// Метод вывода комплексного числа в строку, "i" уже имеется в выводе
        /// </summary>
        /// <returns>Строка вида "(<Вещественная_часть>) +- (<Мнимая_часть>)i"</returns>
        // Пишем override для метода, т.к. этот метод был описан в классе Object, а в
        // С# все созданные классы наследуются от Object(он находится в вершине иерархии наследования)       
        public override string ToString()
        {
            string sign = Imaginary >= 0 ? "+" : "";
            return $"{Real:F4}" + sign + $"{Imaginary:F4}i";
        }
    }
}

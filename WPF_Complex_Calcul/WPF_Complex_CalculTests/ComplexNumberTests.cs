using Microsoft.VisualStudio.TestTools.UnitTesting;
using Complex_calculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// Пространство имен, где хранится весь код тестирования класса ComplexNumber для решения Complex_calculator
namespace Complex_calculator.Tests
{
    /**
    * @author $Eargosha$
    *
    * @date - $14.02.2024$ 
    */
    /// <summary>
    /// Класс тестирования ComplexNumber
    /// </summary>
    [TestClass()]
    public class ComplexNumberTests
    {

        /// <summary>
        /// Тест для оператора +
        /// </summary>
        [TestMethod()]
        public void AdditionOperatorTest()
        {
            // Arrange
            ComplexNumber num1 = new ComplexNumber(3.0, 4.0);
            ComplexNumber num2 = new ComplexNumber(1.0, 2.0);
            ComplexNumber num3 = new ComplexNumber(-1.0, -2.0);

            double expected_Real = 4.0;
            double expected_Imaginary = 6.0;
            double expected_Real2 = 0.0;
            double expected_Imaginary2 = 0.0;

            // Act
            ComplexNumber actual = num1 + num2;
            ComplexNumber actual2 = num2 + num3;

            // Assert
            Assert.AreEqual(expected_Real, actual.Real);
            Assert.AreEqual(expected_Imaginary, actual.Imaginary);
            Assert.AreEqual(expected_Real2, actual2.Real);
            Assert.AreEqual(expected_Imaginary2, actual2.Imaginary);
        }

        /// <summary>
        /// Тест для оператора -
        /// </summary>
        [TestMethod()]
        public void SubtractionOperatorTest()
        {
            // Arrange
            ComplexNumber num1 = new ComplexNumber(3.0, 4.0);
            ComplexNumber num2 = new ComplexNumber(1.0, 2.0);
            ComplexNumber num3 = new ComplexNumber(-1.0, -2.0);

            double expected_Real = 2.0;
            double expected_Imaginary = 2.0;
            double expected_Real2 = 2.0;
            double expected_Imaginary2 = 4.0;

            // Act
            ComplexNumber actual = num1 - num2;
            ComplexNumber actual2 = num2 - num3;

            // Assert
            Assert.AreEqual(expected_Real, actual.Real);
            Assert.AreEqual(expected_Imaginary, actual.Imaginary);
            Assert.AreEqual(expected_Real2, actual2.Real);
            Assert.AreEqual(expected_Imaginary2, actual2.Imaginary);
        }

        /// <summary>
        /// Тест для оператора *
        /// </summary>
        [TestMethod()]
        public void MultiplicationOperatorTest()
        {
            // Arrange
            ComplexNumber num1 = new ComplexNumber(3.0, 4.0);
            ComplexNumber num2 = new ComplexNumber(1.0, 2.0);
            ComplexNumber num3 = new ComplexNumber(-1.0, -2.0);

            double expected_Real = -5.0;
            double expected_Imaginary = 10.0;
            double expected_Real2 = 3.0;
            double expected_Imaginary2 = -4.0;

            // Act
            ComplexNumber actual = num1 * num2;
            ComplexNumber actual2 = num2 * num3;

            // Assert
            Assert.AreEqual(expected_Real, actual.Real);
            Assert.AreEqual(expected_Imaginary, actual.Imaginary);
            Assert.AreEqual(expected_Real2, actual2.Real);
            Assert.AreEqual(expected_Imaginary2, actual2.Imaginary);
        }

        /// <summary>
        /// Тест для оператора /
        /// </summary>
        [TestMethod()]
        public void DivisionnOperatorTest()
        {
            // Arrange
            ComplexNumber num1 = new ComplexNumber(3.0, 4.0);
            ComplexNumber num2 = new ComplexNumber(1.0, 2.0);
            ComplexNumber num3 = new ComplexNumber(-1.0, -2.0);

            double expected_Real = 2.200;
            double expected_Real2 = -1.000;
            double expected_Imaginary = -0.400;
            double expected_Imaginary2 = 0.000;

            // Act
            ComplexNumber actual = num1 / num2;
            ComplexNumber actual2 = num2 / num3;

            // Assert
            Assert.AreEqual(expected_Real, actual.Real);
            Assert.AreEqual(expected_Imaginary, actual.Imaginary);
            Assert.AreEqual(expected_Real2, actual2.Real);
            Assert.AreEqual(expected_Imaginary2, actual2.Imaginary);
        }

        /// <summary>
        /// Тест для метода Module
        /// </summary>
        [TestMethod()]
        public void ModuleTest()
        {
            // Arrange
            ComplexNumber num1 = new ComplexNumber(3.0, 4.0);
            ComplexNumber num2 = new ComplexNumber(-1.0, 4.0);
            ComplexNumber num3 = new ComplexNumber(-1.0, -2.0);
            double expected = 5.0;
            double expected2 = 4.123;
            double expected3 = 2.236;
            /////////// добавить еще тестов во все

            // Act
            double actual = num1.Module();
            double actual2 = num2.Module();
            double actual3 = num3.Module();

            // Assert
            Assert.AreEqual(Math.Round(expected, 3), Math.Round(actual, 3));
            Assert.AreEqual(Math.Round(expected2, 3), Math.Round(actual2, 3));
            Assert.AreEqual(Math.Round(expected3, 3), Math.Round(actual3, 3));
        }

        /// <summary>
        /// Тест для метода Argument
        /// </summary>
        [TestMethod()]
        public void ArgumentTest()
        {
            // Arrange 5 чисел, т.к. есть 5 разных формул вычисления
            ComplexNumber num1 = new ComplexNumber(3.0, 4.0);
            ComplexNumber num2 = new ComplexNumber(-1.0, 4.0);
            ComplexNumber num3 = new ComplexNumber(-1.0, -2.0);
            ComplexNumber num4 = new ComplexNumber(0.0, 4.0);
            ComplexNumber num5 = new ComplexNumber(0.0, -2.0);

            double expected1 = 0.9273;
            double expected2 = 1.8158;
            double expected3 = -2.0344;
            double expected4 = 1.5708;
            double expected5 = -1.5708;

            // Act
            double actual1 = num1.Argument();
            double actual2 = num2.Argument();
            double actual3 = num3.Argument();
            double actual4 = num4.Argument();
            double actual5 = num5.Argument();


            // Assert
            Assert.AreEqual(Math.Round(expected1, 3), Math.Round(actual1, 3));
            Assert.AreEqual(Math.Round(expected2, 3), Math.Round(actual2, 3));
            Assert.AreEqual(Math.Round(expected3, 3), Math.Round(actual3, 3));
            Assert.AreEqual(Math.Round(expected4, 3), Math.Round(actual4, 3));
            Assert.AreEqual(Math.Round(expected5, 3), Math.Round(actual5, 3));

        }

        /// <summary>
        /// Тест для метода ToString, в нем есть тернарный оператор
        /// </summary>
        [TestMethod()]
        public void ToStringTest()
        {
            // Arrange
            ComplexNumber num1 = new ComplexNumber(-4.0, 8.0);
            string expected = "-4.0000+8.0000i";

            // Act
            string actual = num1.ToString();

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
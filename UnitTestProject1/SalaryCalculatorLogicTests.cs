using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalaryCalculator;
using System;

namespace SalaryCalculator.Tests
{
    /// <summary>
    /// Тестовый класс для проверки бизнес-логики расчёта зарплаты.
    /// </summary>
    [TestClass]
    public class SalaryCalculatorLogicTests
    {
        /// <summary>
        /// Проверка расчёта для ассистента без налога.
        /// </summary>
        [TestMethod]
        public void Calculate_Assistant_NoTax_ReturnsCorrectValues()
        {
            double hours = 40;
            var position = SalaryCalculatorLogic.Position.Assistant;
            bool applyTax = false;
            double expectedAccrued = 6000;
            double expectedTax = 0;

            SalaryCalculatorLogic.Calculate(hours, position, applyTax, out double accrued, out double tax);

            Assert.AreEqual(expectedAccrued, accrued);
            Assert.AreEqual(expectedTax, tax);
        }

        /// <summary>
        /// Проверка расчёта для доцента с налогом.
        /// </summary>
        [TestMethod]
        public void Calculate_Docent_WithTax_ReturnsCorrectValues()
        {
            double hours = 20;
            var position = SalaryCalculatorLogic.Position.Docent;
            bool applyTax = true;
            double expectedAccrued = 5000;
            double expectedTax = 650;

            SalaryCalculatorLogic.Calculate(hours, position, applyTax, out double accrued, out double tax);

            Assert.AreEqual(expectedAccrued, accrued);
            Assert.AreEqual(expectedTax, tax);
        }

        /// <summary>
        /// Проверка расчёта для профессора с налогом.
        /// </summary>
        [TestMethod]
        public void Calculate_Professor_WithTax_ReturnsCorrectValues()
        {
            double hours = 50;
            var position = SalaryCalculatorLogic.Position.Professor;
            bool applyTax = true;
            double expectedAccrued = 17500;
            double expectedTax = 2275;

            SalaryCalculatorLogic.Calculate(hours, position, applyTax, out double accrued, out double tax);

            Assert.AreEqual(expectedAccrued, accrued);
            Assert.AreEqual(expectedTax, tax);
        }

        /// <summary>
        /// Проверка форматирования – при дробных часах налог округляется до двух знаков.
        /// </summary>
        [TestMethod]
        public void Calculate_FractionalHours_ReturnsFormattedValues()
        {
            double hours = 12.5;
            var position = SalaryCalculatorLogic.Position.Docent;
            bool applyTax = true;
            double expectedAccrued = 3125.00;
            double expectedTax = 406.25;

            SalaryCalculatorLogic.Calculate(hours, position, applyTax, out double accrued, out double tax);

            Assert.AreEqual(expectedAccrued, accrued);
            Assert.AreEqual(expectedTax, tax);
        }

        /// <summary>
        /// Проверка, что при нулевых часах выбрасывается ArgumentException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_ZeroHours_ThrowsArgumentException()
        {
            SalaryCalculatorLogic.Calculate(0, SalaryCalculatorLogic.Position.Assistant, false, out _, out _);
        }

        /// <summary>
        /// Проверка, что при отрицательных часах выбрасывается ArgumentException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_NegativeHours_ThrowsArgumentException()
        {
            SalaryCalculatorLogic.Calculate(-10, SalaryCalculatorLogic.Position.Assistant, false, out _, out _);
        }

        /// <summary>
        /// Проверка, что при большом количестве часов расчёт корректен (нет переполнения).
        /// </summary>
        [TestMethod]
        public void Calculate_LargeHours_ReturnsCorrectValues()
        {
            double hours = 1000;
            var position = SalaryCalculatorLogic.Position.Professor;
            bool applyTax = true;
            double expectedAccrued = 350000;
            double expectedTax = 45500;

            SalaryCalculatorLogic.Calculate(hours, position, applyTax, out double accrued, out double tax);

            Assert.AreEqual(expectedAccrued, accrued);
            Assert.AreEqual(expectedTax, tax);
        }

        /// <summary>
        /// Проверка, что при неверной должности выбрасывается ArgumentException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_InvalidPosition_ThrowsArgumentException()
        {
            var invalidPosition = (SalaryCalculatorLogic.Position)99;

            SalaryCalculatorLogic.Calculate(10, invalidPosition, false, out _, out _);
        }

        /// <summary>
        /// Проверка, что налог считается только когда чекбокс включён.
        /// </summary>
        [TestMethod]
        public void Calculate_TaxAppliedOnlyWhenFlagTrue()
        {
            double hours = 10;
            var position = SalaryCalculatorLogic.Position.Assistant;

            SalaryCalculatorLogic.Calculate(hours, position, false, out double accruedNoTax, out double taxNoTax);
            SalaryCalculatorLogic.Calculate(hours, position, true, out double accruedWithTax, out double taxWithTax);

            Assert.AreEqual(accruedNoTax, accruedWithTax);
            Assert.AreEqual(0, taxNoTax);
            Assert.AreEqual(accruedWithTax * 0.13, taxWithTax);
        }

        /// <summary>
        /// Проверка, что расчёт для всех должностей выполняется корректно.
        /// </summary>
        [TestMethod]
        public void Calculate_AllPositions_ReturnsCorrectRates()
        {
            double hours = 1;
            bool applyTax = false;

            SalaryCalculatorLogic.Calculate(hours, SalaryCalculatorLogic.Position.Assistant, applyTax, out double accruedAssistant, out _);
            SalaryCalculatorLogic.Calculate(hours, SalaryCalculatorLogic.Position.Docent, applyTax, out double accruedDocent, out _);
            SalaryCalculatorLogic.Calculate(hours, SalaryCalculatorLogic.Position.Professor, applyTax, out double accruedProfessor, out _);

            Assert.AreEqual(150, accruedAssistant);
            Assert.AreEqual(250, accruedDocent);
            Assert.AreEqual(350, accruedProfessor);
        }
    }
}
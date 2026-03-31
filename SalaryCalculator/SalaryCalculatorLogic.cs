using System;

namespace SalaryCalculator
{
    /// <summary>
    /// Класс, содержащий бизнес-логику расчёта зарплаты.
    /// </summary>
    public static class SalaryCalculatorLogic
    {
        /// <summary>
        /// Должности преподавателей.
        /// </summary>
        public enum Position
        {
            Assistant,   // ассистент, 150 руб/час
            Docent,      // доцент, 250 руб/час
            Professor    // профессор, 350 руб/час
        }

        /// <summary>
        /// Вычисляет начисленную сумму и налог.
        /// </summary>
        /// <param name="hours">Количество отработанных часов (положительное число).</param>
        /// <param name="position">Должность преподавателя.</param>
        /// <param name="applyTax">Признак необходимости удержания налога 13%.</param>
        /// <param name="accrued">Начисленная сумма (выходной параметр).</param>
        /// <param name="tax">Сумма налога (выходной параметр).</param>
        /// <exception cref="ArgumentException">Если часы <= 0 или должность неизвестна.</exception>
        public static void Calculate(double hours, Position position, bool applyTax, out double accrued, out double tax)
        {
            if (hours <= 0)
                throw new ArgumentException("Количество часов должно быть положительным.", nameof(hours));

            double rate;
            switch (position)
            {
                case Position.Assistant:
                    rate = 150;
                    break;
                case Position.Docent:
                    rate = 250;
                    break;
                case Position.Professor:
                    rate = 350;
                    break;
                default:
                    throw new ArgumentException("Неизвестная должность.", nameof(position));
            }

            accrued = hours * rate;
            tax = applyTax ? accrued * 0.13 : 0;
        }
    }
}
using System;
using System.Windows;

namespace SalaryCalculator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(txtHours.Text, out double hours) || hours <= 0)
            {
                MessageBox.Show("Введите корректное количество часов (положительное число).",
                                "Ошибка ввода",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            SalaryCalculatorLogic.Position position;
            if (rbAssistant.IsChecked == true)
                position = SalaryCalculatorLogic.Position.Assistant;
            else if (rbDocent.IsChecked == true)
                position = SalaryCalculatorLogic.Position.Docent;
            else
                position = SalaryCalculatorLogic.Position.Professor;

            bool applyTax = chkTax.IsChecked == true;
            SalaryCalculatorLogic.Calculate(hours, position, applyTax, out double accrued, out double tax);

            tbAccrued.Text = $"{accrued:F2} руб.";
            tbTax.Text = $"{tax:F2} руб.";
        }
    }
}
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Calculator
{
    public partial class MainWindow : Window
    {
        private string currentInput = "";
        private double currentValue = 0;
        private string currentOperation = "";

        public MainWindow()
        {
            InitializeComponent();
            Focus(); // Установлюю фокус на вікно, щоб увімкнути введення з клавіатури
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            AppendInput(button.Content.ToString());
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            currentOperation = button.Content.ToString();
            currentValue = double.Parse(currentInput);
            currentInput = "";
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            CalculateResult();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (char.IsDigit((char)e.Key) || e.Key == Key.Add || e.Key == Key.Subtract
                || e.Key == Key.Multiply || e.Key == Key.Divide)
            {
                string keyText = e.Key.ToString().Replace("D", "");
                if (keyText == "OemPlus") keyText = "+"; 
                if (keyText == "OemMinus") keyText = "-"; 
                if (keyText == "Multiply") keyText = "*"; 
                if (keyText == "Divide") keyText = "/"; 
                AppendInput(keyText);
            }
            else if (e.Key == Key.Enter)
            {
                CalculateResult();
            }
            else if (e.Key == Key.Back)
            {
                DeleteButton_Click(null, null); // Активувати видалення, коли натиснуто клавішу Backspace
            }
            else if (e.Key == Key.Escape)
            {
                ClearAllButton_Click(null, null); // Очищається все, коли натискається клавіша Escape
            }
        }

        private void AppendInput(string input)
        {
            currentInput += input;
            ResultTextBox.Text = currentInput;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(currentInput))
            {
                currentInput = currentInput.Substring(0, currentInput.Length - 1);
                ResultTextBox.Text = currentInput;
            }
        }

        private void CalculateResult()
        {
            double secondValue = double.Parse(currentInput);
            double result = 0;

            switch (currentOperation)
            {
                case "+":
                    result = currentValue + secondValue;
                    break;
                case "-":
                    result = currentValue - secondValue;
                    break;
                case "*":
                    result = currentValue * secondValue;
                    break;
                case "/":
                    if (secondValue != 0)
                        result = currentValue / secondValue;
                    else
                        ResultTextBox.Text = "Error";
                    break;
                default:
                    break;
            }

            ResultTextBox.Text = result.ToString();
            currentInput = result.ToString();
        }

        private void ClearEntryButton_Click(object sender, RoutedEventArgs e)
        {
            currentInput = "";
            ResultTextBox.Text = "";
        }

        private void ClearAllButton_Click(object sender, RoutedEventArgs e)
        {
            currentInput = "";
            currentValue = 0;
            currentOperation = "";
            ResultTextBox.Text = "";
        }
    }
}

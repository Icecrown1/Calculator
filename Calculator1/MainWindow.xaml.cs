using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Calculator
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string currentNumber = string.Empty;
        private string operation = string.Empty;
        private double result = 0.0;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Result
        {
            get { return result.ToString(); }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string tag = (sender as Button).Tag.ToString();

            if (tag == "+" || tag == "-" || tag == "*" || tag == "/")
            {
                if (!string.IsNullOrEmpty(currentNumber))
                {
                    double number = Convert.ToDouble(currentNumber);
                    if (string.IsNullOrEmpty(operation))
                    {
                        result = number;
                    }
                    else
                    {
                        switch (operation)
                        {
                            case "+":
                                result += number;
                                break;
                            case "-":
                                result -= number;
                                break;
                            case "*":
                                result *= number;
                                break;
                            case "/":
                                result /= number;
                                break;
                        }
                    }
                    operation = tag;
                    currentNumber = string.Empty;
                }
            }
            else if (tag == "=")
            {
                if (!string.IsNullOrEmpty(currentNumber))
                {
                    double number = Convert.ToDouble(currentNumber);
                    switch (operation)
                    {
                        case "+":
                            result += number;
                            break;
                        case "-":
                            result -= number;
                            break;
                        case "*":
                            result *= number;
                            break;
                        case "/":
                            result /= number;
                            break;
                    }
                    operation = string.Empty;
                    currentNumber = result.ToString();
                }
            }
            else
            {
                currentNumber += tag;
            }

            OnPropertyChanged("Result"); // Оповестить об изменении свойства Result
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            currentNumber = string.Empty;
            operation = string.Empty;
            result = 0.0;
            OnPropertyChanged("Result"); // Обновить привязку данных в TextBox
        }
    }
}
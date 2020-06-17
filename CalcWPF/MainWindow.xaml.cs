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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalcWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string leftop = ""; // Левый операнд
        string operation = ""; // Знак операции
        string rightop = ""; // Правый операнд
        bool check = true;
        bool checkzero = true;
        public MainWindow()
        {
            InitializeComponent();

            foreach (UIElement c in LayoutRoot.Children)
            {
                if (c is Button)
                {
                    ((Button)c).Click += Button_Click;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (textBlock.Text == "0" && checkzero)
            {
                checkzero = false;
                textBlock.Text = "";
            }
            string s = (string)((Button)e.OriginalSource).Content;
            textBlock.Text += s;
            int num;
            bool result = int.TryParse(s, out num);
            if (leftop == "" && (s == "+" || s == "-" || s == "*" || s == "/" || s == "^" || s == "div" || s == "mod"))
            {
                MessageBox.Show("Сначала введите число!");
                Clear();
            }
            if (result == true)
            {
                if (operation == "") leftop += s;
                else rightop += s;
            }
            else
            {
                if (s == "=")
                {
                    if (leftop == "" || rightop == "")
                    {
                        MessageBox.Show("Введите другую операцию!");
                        Clear();
                    }
                    else
                    {
                        if (check)
                        {
                            Update_RightOp();
                            textBlock.Text += rightop;
                            operation = "";
                            check = false;
                        }
                        else
                        {
                            leftop = rightop;
                            textBlock.Text = leftop;
                        }
                    }
                }
                else if (s == "Clear") Clear();
                else if (s == "Delete")
                {
                    check = true;
                    if (operation == "" && !leftop.Equals(""))
                    {
                        leftop = leftop.Remove(leftop.Length - 1);
                        textBlock.Text = leftop;
                    }
                    else if (!rightop.Equals(""))
                    {
                        rightop = rightop.Remove(rightop.Length - 1);
                        textBlock.Text = leftop + operation + rightop;
                    }
                    if (leftop.Equals("")) Clear(); /*textBlock.Text = "0"*/
                }
                else if (s == "+/-")
                {
                    check = true;
                    try
                    {
                        if (leftop != "0")
                        {
                            if (rightop != "0")
                            {
                                if (operation == "")
                                {
                                    if (leftop[0] != '-') leftop = leftop.Insert(0, "-");
                                    else leftop = leftop.Remove(0, 1);
                                    textBlock.Text = leftop;
                                }
                                else
                                {
                                    if (rightop[0] != '-')
                                    {
                                        rightop = rightop.Insert(0, "-");
                                        textBlock.Text = leftop + operation + '(' + rightop + ')';
                                    }
                                    else
                                    {
                                        rightop = rightop.Remove(0, 1);
                                        textBlock.Text = leftop + operation + rightop;
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Ноль не может быть отрицательным!");
                                textBlock.Text = leftop + operation + rightop;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ноль не может быть отрицательным!");
                            Clear();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Для смены знака, сначала введите число!");
                        Clear();
                    }
                }
                else if (s == "1/x")
                {
                    check = true;
                    try
                    {
                        double n = double.Parse(leftop);
                        if (n != 0)
                        {
                            leftop = Math.Pow(n, -1).ToString();
                            textBlock.Text = leftop;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Введите число!");
                        Clear();
                    }
                }
                else if (s == "x!")
                {
                    check = true;
                    try
                    {
                        double n = double.Parse(leftop);
                        int factorial = 1;
                        for (int i = 2; i <= n; i++) factorial *= i;
                        if (factorial < int.MaxValue) leftop = factorial.ToString();
                        textBlock.Text = leftop;
                    }
                    catch
                    {
                        MessageBox.Show("Сначала введите число факториала");
                        textBlock.Text = "0";
                    }
                }
                else if (s == "√")
                {
                    check = true;
                    try
                    {
                        double n = double.Parse(leftop);
                        if (n >= 0)
                        {
                            leftop = Math.Sqrt(n).ToString();
                            textBlock.Text = leftop;
                        }
                        else
                        {
                            MessageBox.Show("Нельзя извлечь корень из отрицательного числа");
                            Clear();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Сначала введите число из которого хотите извлечь корень!");
                        Clear();
                    }
                }
                else if (s == "x^2")
                {
                    check = true;
                    try
                    {
                        if (rightop == "")
                        {
                            int n = int.Parse(leftop);
                            if (n * n < int.MaxValue) leftop = (n * n).ToString();
                            textBlock.Text = leftop;
                        }
                        else
                        {
                            int n = int.Parse(rightop);
                            if (n * n < int.MaxValue) rightop = (n * n).ToString();
                            textBlock.Text = rightop;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Сначала введите число, которое хотите возвести в квадрат!");
                        Clear();
                    }
                }
                else if (s == "sin")
                {
                    check = true;
                    try
                    {
                        double n = double.Parse(leftop);
                        leftop = Math.Sin(n).ToString();
                        textBlock.Text = leftop;
                    }
                    catch
                    {
                        if (leftop == "" || leftop == "0")
                        {
                            MessageBox.Show("Сначала введите число!");
                            Clear();
                        }
                    }
                }
                else if (s == "cos")
                {
                    check = true;
                    try
                    {
                        leftop = Math.Cos(double.Parse(leftop)).ToString();
                        textBlock.Text = leftop;
                    }
                    catch
                    {
                        if (leftop == "" || leftop == "0")
                        {
                            MessageBox.Show("Сначала введите число!");
                            Clear();
                        }
                    }
                }
                else if (s == "tan")
                {
                    check = true;
                    try
                    {
                        double n = double.Parse(leftop);
                        leftop = Math.Tan(n).ToString();
                        textBlock.Text = leftop;
                    }
                    catch
                    {
                        if (leftop == "" || leftop == "0")
                        {
                            MessageBox.Show("Сначала введите число!");
                            Clear();
                        }
                    }
                }
                else if (s == "e")
                {
                    Clear();
                    if (leftop == "")
                    {
                        leftop = Math.Exp(1).ToString();
                        textBlock.Text = leftop;
                    }
                    else
                    {
                        rightop = Math.Exp(1).ToString();
                        textBlock.Text = rightop;
                    }
                }
                else if (s == "e^x")
                {
                    try
                    {
                        double n = double.Parse(leftop);
                        leftop = Math.Exp(n).ToString();
                        textBlock.Text = leftop;
                    }
                    catch
                    {
                        if (leftop == "" || leftop == "0")
                        {
                            MessageBox.Show("Сначала введите степень, в которую хотите возвести e!");
                            Clear();
                        }
                    }
                }
                else if (s == "π")
                {
                    Clear();
                    leftop = Math.PI.ToString();
                    textBlock.Text = leftop;
                }
                else
                {
                    if (rightop != "")
                    {
                        Update_RightOp();
                        leftop = rightop;
                        rightop = "";
                    }
                    operation = s;
                }
            }
        }

        private void Clear()
        {
            leftop = "";
            rightop = "";
            operation = "";
            textBlock.Text = "0";
            check = true;
            checkzero = true;
        }

        private void Update_RightOp()
        {
            double num1 = double.Parse(leftop);
            double num2 = double.Parse(rightop);
            check = true;
            switch (operation)
            {
                case "+": rightop = (num1 + num2).ToString();
                    break;
                case "-": rightop = (num1 - num2).ToString();
                    break;
                case "*": rightop = (num1 * num2).ToString();
                    break;
                case "/": if (rightop != "0") rightop = (num1 / num2).ToString();
                    else
                    {
                        MessageBox.Show("На ноль делить нельзя");
                        //textBlock.Text = leftop + operation;
                        Clear();
                    }
                    break;
                case "mod": if (rightop != "0") rightop = (num1 % num2).ToString();
                    else
                    {
                        MessageBox.Show("На ноль делить нельзя");
                        //textBlock.Text = leftop + operation;
                        Clear();
                    }
                    break;
                case "div": if (rightop != "0") rightop = Math.Truncate(num1 / num2).ToString();
                    else
                    {
                        MessageBox.Show("На ноль делить нельзя");
                        //textBlock.Text = leftop + operation;
                        Clear();
                    }
                    break;
                case "^": rightop = Math.Pow(num1, num2).ToString();
                    break;
            }
        }
    }
}
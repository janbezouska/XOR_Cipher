using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace QuadraticEquation
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButCalc_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(tbA.Text) || string.IsNullOrEmpty(tbB.Text) || string.IsNullOrEmpty(tbC.Text))
            {
                tbX1.Text = "zadejte čísla";
            }
            else
            {
                Tuple<double?, double?> results = QuadEquation(double.Parse(tbA.Text), double.Parse(tbB.Text), double.Parse(tbC.Text));
                if (results.Item1 == null)
                    tbX1.Text = results.Item1.ToString();
                else
                    tbX1.Text = "žádné řešení";

            }
        }

        public static double Discriminant(double a, double b, double c)
        {
            return Math.Pow(b, 2) - 4 * a * c;
        }

        public Tuple<double?, double?> QuadEquation(double a, double b, double c)
        {
            double discriminant = Discriminant(a, b, c);
            if (a != 0)
            {
                if (discriminant > 0)
                    return Tuple.Create<double?, double?>((-b + Math.Sqrt(discriminant)) / (2 * a), (-b - Math.Sqrt(discriminant)) / (2 * a));
                if (discriminant == 0)
                    return Tuple.Create<double?, double?>((-b + Math.Sqrt(discriminant)) / (2 * a), null);
                else
                    return Tuple.Create<double?, double?>(null, null);
            }
            else
            {
                return Tuple.Create<double?, double?>(-c / b, null);
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.-]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}

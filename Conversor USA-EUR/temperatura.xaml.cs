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
using System.Windows.Shapes;

namespace Conversor_USA_EUR
{

    public partial class temperatura : Window
    {
        public temperatura()
        {
            InitializeComponent();
        }

        private bool ValidarFormato(string input, TextBox textBox)
        {
            if (!Regex.IsMatch(input, @"^-?\d*([,]?\d{0,2})?$"))
            {
                textBox.Text = input.Remove(input.Length - 1);
                textBox.CaretIndex = textBox.Text.Length;
                MessageBox.Show($"Introduce un decimal con coma", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        public double controlNumeroValNum1()
        {
            double resultado = 0;
            string valorIntroducido = valnum1.Text;
            if (double.TryParse(valorIntroducido, out double resultadoFinal))
            {
                if (!ValidarFormato(valorIntroducido, valnum1))
                    return -1;

                valorIntroducido = valorIntroducido.Replace(',', '.');
                resultado = resultadoFinal;
            }
            else
            {
                MessageBox.Show($"Factor de conversión no válido. Ingrese un valor numérico", "Error", MessageBoxButton.OK, MessageBoxImage.Error);



            }
            return resultado;

        }

        public double controlNumeroValNum2()
        {
            string valorIntroducido = valnum2.Text;
            double resultado = 0;

            if (double.TryParse(valorIntroducido, out double resultadoFinal))
            {
                if (!ValidarFormato(valorIntroducido, valnum2))
                    return -1;
                valorIntroducido = valorIntroducido.Replace(',', '.');
                resultado = resultadoFinal;
            }
            else
            {
                MessageBox.Show($"Factor de conversión no válido. Ingrese un valor numérico", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }

            return resultado;

        }

        private void valnum2_TextChanged_1(object sender, TextChangedEventArgs e)
        {

            if (valnum1.Text.Length == 0 && valnum2.Text.Length != 0)
            {
                calcular.IsEnabled = true;
            }

        }

        private void reiniciar_Click(object sender, RoutedEventArgs e)
        {
            valnum1.Text = "";
            valnum2.Text = "";
            valnum2.IsEnabled = true;
            valnum1.IsEnabled = true;
            calcular.IsEnabled = false;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (valnum1.Text.Length != 0 && valnum2.Text.Length == 0)
            {
                calcular.IsEnabled = true;
            }
        }

        private void calcular_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double factorC = 0.5556;

                if (valnum1.Text.Length == 0 && valnum2.Text.Length != 0)
                {
                    double resultado = controlNumeroValNum2();

                    //este valor es el cero absoluto no existe temperatura mas baja posible
                    if (resultado >= -459.67)
                    {
                        double celsius = (resultado - 32) * factorC;
                        valnum1.Text = celsius.ToString("F2");
                        calcular.IsEnabled = false;
                        valnum2.IsEnabled = false;
                        valnum1.IsEnabled = false;
                    }
                    else
                    {
                        MessageBox.Show("-459.67 este valor es el cero absoluto, no existe temperatura mas baja posible ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                    }

                }
                else if (valnum2.Text.Length == 0 && valnum1.Text.Length != 0)
                {
                    double resultado2 = controlNumeroValNum1();

                    if (resultado2 >= -273.15)
                    {
                        double farenheit = (resultado2 * 1.8) + 32;
                        valnum2.Text = farenheit.ToString("F2");
                        calcular.IsEnabled = false;
                        valnum2.IsEnabled = false;
                        valnum1.IsEnabled = false;
                    }
                    else
                    {
                        MessageBox.Show("-273.15 este valor es el cero absoluto, no existe temperatura mas baja posible ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    
                }
                else
                {
                    MessageBox.Show("Solo se debe introducir un valor a la vez.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

}

using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Conversor_USA_EUR
{
    
    public partial class ConversionDivisas : Window
    {
        
        public ConversionDivisas()
        {
            InitializeComponent();
            calcular.IsEnabled = false;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (valnum1.Text.Length != 0 && valnum2.Text.Length == 0)
            {
                calcular.IsEnabled = true;
            }

        }

        private bool ValidarFormato(string input, TextBox textBox)
        {
            if ((input.Contains(" ")))
            {
                MessageBox.Show($"No pongas espacio en medio", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                textBox.Clear();
                return false;
            }

            if (input.StartsWith("-"))
            {
                MessageBox.Show($"No se aceptan números negativos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                textBox.Clear();
                return false;
            }
            if (!Regex.IsMatch(input, @"^\d*([,]\d{0,2})?$"))
            {
                textBox.Clear();
                MessageBox.Show($"Ingrese una coma con 2 decimales", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
        public double controlNumeroValNum1()
        {
            string valorIntroducido = valnum1.Text;
            double resultado = 0;
            if (double.TryParse(valorIntroducido, out double resultadoFinal))
                {
                if (!ValidarFormato(valorIntroducido, valnum1))
                    return -1;
                resultado = resultadoFinal;
                }
                else
                {
                    MessageBox.Show($"Factor de conversión no válido. Ingrese un valor numérico", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return -1;
                }
            return resultado;

        }

        public double controlNumeroValfactor_conversion()
        {
            string valorIntroducido = factor_conversion.Text;
            double resultado = 0;
            if (double.TryParse(valorIntroducido, out double resultadoFinal))
                {
                if (!ValidarFormato(valorIntroducido, factor_conversion))
                    return -1;
                resultado = resultadoFinal;
                }
            else
            {
                MessageBox.Show($"Factor de conversión no válido. Ingrese un valor numérico", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return -1;
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
                resultado = resultadoFinal;
            }
            else
            {
                MessageBox.Show($"Factor de conversión no válido. Ingrese un valor numérico", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return -1;
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


        private void calcular_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double factorC = controlNumeroValfactor_conversion();

                if (factorC <= 0)
                {
                    MessageBox.Show("El factor de conversión debe ser mayor que cero.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    factor_conversion.Clear();
                    return;
                }
                if(factorC == -1)
                {
                    factor_conversion.Clear();
                    return;
                }

                if (valnum1.Text.Length != 0 && valnum2.Text.Length == 0)
                {
                    double resultado = controlNumeroValNum1();
                    if (resultado >= 0)
                    {
                        double euros = resultado / factorC;
                        valnum2.Text = euros.ToString("F2");
                        calcular.IsEnabled = false;
                        valnum2.IsEnabled = false;
                        valnum1.IsEnabled = false;
                    }else if(resultado == -1)
                    {
                        valnum1.Clear();
                    }
                }
                else if (valnum2.Text.Length != 0 && valnum1.Text.Length == 0)
                {
                    double resultado2 = controlNumeroValNum2();
                    if (resultado2 >= 0)
                    {
                        double dollars = resultado2 * factorC;
                        valnum1.Text = dollars.ToString("F2");
                        calcular.IsEnabled = false;
                        valnum2.IsEnabled = false;
                        valnum1.IsEnabled = false;
                    }
                    else if (resultado2 == -1)
                    {
                        valnum2.Clear();
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


        private void reiniciar_Click(object sender, RoutedEventArgs e)
        {
            valnum1.Text = "";
            valnum2.Text = "";
            valnum1.IsEnabled = true;
            valnum2.IsEnabled = true;
            calcular.IsEnabled = false;
        }
    }

}


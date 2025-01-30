using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Conversor_USA_EUR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            calcular.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (conversionSelecion.SelectedIndex == 1){
               
                ConversionDivisas conversionDivisas = new ConversionDivisas();
                conversionDivisas.ShowDialog();
            }
            else if(conversionSelecion.SelectedIndex == 0)
                {

                    ConversionLongitud conversionLongitud = new ConversionLongitud();
                    conversionLongitud.ShowDialog();
                }

            else if (conversionSelecion.SelectedIndex == 2)
            {
                temperatura temperatura = new temperatura();
                temperatura.ShowDialog();

            }
            else if (conversionSelecion.SelectedIndex == 4)
            {
                altura altura = new altura();
                altura.ShowDialog();
            }
            else if (conversionSelecion.SelectedIndex == 3)
            {
                medida medida = new medida();
                medida.ShowDialog();
            }
            else

            {
                MessageBox.Show($"Elige una opción válida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            AcercaDe acercaDe = new AcercaDe();
            acercaDe.ShowDialog();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void conversionSelecion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            calcular.IsEnabled = true;
        }
    }
}
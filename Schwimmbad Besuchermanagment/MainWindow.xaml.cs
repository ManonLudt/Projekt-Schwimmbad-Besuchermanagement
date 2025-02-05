using System;
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

namespace Schwimmbad_Besuchermanagment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Click_Login(object sender, RoutedEventArgs e)
        {
            Dashboard DashboardWindow = new Dashboard();
            this.Visibility = Visibility.Hidden;
            DashboardWindow.Show();
        }

        private void RegistryClick(object sender, RoutedEventArgs e)
        {
            Registrierung RegistrierungWindow = new Registrierung();
            RegistrierungWindow.Show();
        }
    }
}

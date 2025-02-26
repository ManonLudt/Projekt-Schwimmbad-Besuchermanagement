using Microsoft.Data.SqlClient;
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

namespace Besuchermanagement_Benutzerverwaltung
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
            string username = txtUsername.Text; 
            string password = pwdPassword.Password; 

            if (VerifyUserCredentials(username, password))
            {
                Dashboard DashboardWindow = new Dashboard();
                this.Visibility = Visibility.Hidden;
                DashboardWindow.Show();
            }
            else
            {
                txtUsername.BorderBrush = Brushes.Red;
                pwdPassword.BorderBrush = Brushes.Red;
                ErrorAusgabe.Text = "Benutzername falsch oder existiert nicht!";
            }
        }
        private static bool VerifyUserCredentials(string username, string password)
        {
            // Verbindung zur Datenbank aufbauen
            SqlConnectionStringBuilder sqlSb = new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDb)\MSSQLLocalDB",
                InitialCatalog = "BesuchermanagmentDatenbank",
                IntegratedSecurity = true,
                MultipleActiveResultSets = true
            };

            // Verbindung zur Datenbank aufbauen
            using (SqlConnection con = new SqlConnection(sqlSb.ConnectionString))
            {
                try
                {
                    con.Open();

                    string sqlSelectUser = "SELECT COUNT(1) FROM Benutzer WHERE Benutzername = @username AND Passwort = @password";

                    using (SqlCommand command = new SqlCommand(sqlSelectUser, con))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);

                        int result = Convert.ToInt32(command.ExecuteScalar());

                        return result > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Fehler bei der Datenbankverbindung: " + ex.Message);
                    return false;
                }
            }
        }

        //Checkbox
        private void ShowPasswordCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            txtPassword.Text = pwdPassword.Password;

            txtPassword.Visibility = Visibility.Visible;
        }

        private void ShowPasswordCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            txtPassword.Visibility = Visibility.Collapsed;
        }

    }
}
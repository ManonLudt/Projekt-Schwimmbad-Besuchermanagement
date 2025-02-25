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
            string username = txtUsername.Text; // Textfeld für Benutzername
            string password = pwdPassword.Password; // Passwortfeld

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

        private void RegistryClick(object sender, RoutedEventArgs e)
        {
            Registrierung RegistrierungWindow = new Registrierung();
            RegistrierungWindow.Show();
        }

        private void ChangePasswortClick(object sender, RoutedEventArgs e)
        {
            PasswortÄndern PasswortÄndernWindow = new PasswortÄndern();
            PasswortÄndernWindow.Show();
        }

        private static bool VerifyUserCredentials(string username, string password)
        {
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

                    // SQL-Abfrage zum Überprüfen des Benutzernamens und Passworts
                    string sqlSelectUser = "SELECT COUNT(1) FROM Benutzer WHERE Benutzername = @username AND Passwort = @password";

                    using (SqlCommand command = new SqlCommand(sqlSelectUser, con))
                    {
                        // Parameter für SQL-Befehl festlegen, um SQL-Injection zu verhindern
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password); // Achte darauf, dass du das Passwort sicherst speicherst

                        // Ergebnis der Abfrage abrufen
                        int result = Convert.ToInt32(command.ExecuteScalar());

                        // Wenn `result` > 0, wurde der Benutzer gefunden, andernfalls nicht
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

        private void ShowPasswordCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            // Kopiere den Inhalt der PasswordBox in die TextBox
            txtPassword.Text = pwdPassword.Password;

            // Mache die TextBox sichtbar
            txtPassword.Visibility = Visibility.Visible;
        }

        // Wenn die CheckBox deaktiviert wird
        private void ShowPasswordCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            // Leere die TextBox und mache sie unsichtbar
            txtPassword.Visibility = Visibility.Collapsed;
        }

    }
}

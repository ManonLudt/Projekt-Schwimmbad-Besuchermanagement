using Microsoft.Data.SqlClient;
using Schwimmbad_Besuchermanagment.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace SchwimmbadProjekt_Besuchermanagment
{
    /// <summary>
    /// Interaktionslogik für ReservierungAusgabe.xaml
    /// </summary>
    public partial class ReservierungAusgabe : Page
    {
        private BesuchermanagmentDatenbankContext _context = new BesuchermanagmentDatenbankContext();

        //private ListCollectionView DisplayView;
        private ICollectionView DisplayView;
        public ReservierungAusgabe()
        {
            InitializeComponent();
            UpdateGesamtanzahl();
            UpdateAnwesend();
            UpdateAbwesend();

            _context.Besuchers.Load();

            DisplayView = CollectionViewSource.GetDefaultView(_context.Besuchers.Local.ToObservableCollection());
            DataContext = DisplayView;
        }

        private void UpdateGesamtanzahl()
        {
            // Verbindung zur Datenbank aufbauen
            SqlConnectionStringBuilder sqlSb = new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDb)\MSSQLLocalDB", // Verbindungsstring anpassen
                InitialCatalog = "BesuchermanagmentDatenbank", // Datenbankname anpassen
                IntegratedSecurity = true,
                MultipleActiveResultSets = true
            };

            // SQL-Verbindung verwenden
            using (SqlConnection con = new SqlConnection(sqlSb.ConnectionString))
            {
                try
                {
                    con.Open();

                    // SQL-Anweisung, um die Anzahl der Reservierungen zu zählen
                    string query = "SELECT COUNT(*) FROM Reservierung";

                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        // Die Anzahl der Einträge aus der Tabelle "Reservierung" ermitteln
                        int anzahlReservierungen = (int)command.ExecuteScalar();

                        // Den Wert in der TextBox "Gesamt" anzeigen
                        Gesamt.Text = anzahlReservierungen.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fehler: " + ex.Message);
                }
            }
        }

        private void UpdateAnwesend()
        {
            // Verbindung zur Datenbank aufbauen
            SqlConnectionStringBuilder sqlSb = new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDb)\MSSQLLocalDB", // Verbindungsstring anpassen
                InitialCatalog = "BesuchermanagmentDatenbank", // Datenbankname anpassen
                IntegratedSecurity = true,
                MultipleActiveResultSets = true
            };

            // SQL-Verbindung verwenden
            using (SqlConnection con = new SqlConnection(sqlSb.ConnectionString))
            {
                try
                {
                    con.Open();

                    // SQL-Anweisung, um die Anzahl der Reservierungen zu zählen, bei denen Anwesend = 1 ist
                    string query = "SELECT COUNT(*) FROM Reservierung WHERE Anwesend = 1";

                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        // Die Anzahl der Einträge aus der Tabelle "Reservierung" ermitteln, bei denen Anwesend = 1
                        int anzahlAnwesende = (int)command.ExecuteScalar();

                        // Den Wert in der TextBox "Gesamt" anzeigen
                        Anwesend.Text = anzahlAnwesende.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fehler: " + ex.Message);
                }
            }
        }

        private void UpdateAbwesend()
        {
            // Verbindung zur Datenbank aufbauen
            SqlConnectionStringBuilder sqlSb = new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDb)\MSSQLLocalDB", // Verbindungsstring anpassen
                InitialCatalog = "BesuchermanagmentDatenbank", // Datenbankname anpassen
                IntegratedSecurity = true,
                MultipleActiveResultSets = true
            };

            // SQL-Verbindung verwenden
            using (SqlConnection con = new SqlConnection(sqlSb.ConnectionString))
            {
                try
                {
                    con.Open();

                    // SQL-Anweisung, um die Anzahl der Reservierungen zu zählen, bei denen Anwesend = 1 ist
                    string query = "SELECT COUNT(*) FROM Reservierung WHERE Anwesend is NULL";

                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        // Die Anzahl der Einträge aus der Tabelle "Reservierung" ermitteln, bei denen Anwesend = 1
                        int anzahlAbwesende = (int)command.ExecuteScalar();

                        // Den Wert in der TextBox "Gesamt" anzeigen
                        Abwesend.Text = anzahlAbwesende.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fehler: " + ex.Message);
                }
            }
        }

        private void ReservierungLöschen_Click(object sender, RoutedEventArgs e)
        {
            ReservierungLöschen ReservierungLöschenWindow = new ReservierungLöschen();
            ReservierungLöschenWindow.Show();
        }

        private void ReservierungAnlegen_Click(object sender, RoutedEventArgs e)
        {
            ReservierungAnlegen ReservierungAnlegenWindow = new ReservierungAnlegen();
            ReservierungAnlegenWindow.Show();
        }

        private void ReservierungÄndern_Click(object sender, RoutedEventArgs e)
        {
            ReservierungÄndern ReservierungÄndernWindow = new ReservierungÄndern();
            ReservierungÄndernWindow.Show();
        }
    }
}

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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

namespace Schwimmbad_Besuchermanagment
{
    /// <summary>
    /// Interaktionslogik für DashboardAusgabe.xaml
    /// </summary>
    public partial class DashboardAusgabe : Page
    {
        private BesuchermanagmentDatenbankContext _context = new BesuchermanagmentDatenbankContext();

        //private ListCollectionView DisplayView;
        private ICollectionView DisplayView;

        public DashboardAusgabe()
        {
            InitializeComponent();
            UpdateGesamtanzahl();
            UpdateAnwesend();
            UpdateAbwesend();

            _context.Reservierungs.Load();                    

            DisplayView = CollectionViewSource.GetDefaultView(_context.Reservierungs.Local.ToObservableCollection());
            DataContext = DisplayView;
        }

        private void UpdateGesamtanzahl()
        {
            // Verbindung zur Datenbank aufbauen
            SqlConnectionStringBuilder sqlSb = new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDb)\MSSQLLocalDB", 
                InitialCatalog = "BesuchermanagmentDatenbank", 
                IntegratedSecurity = true,
                MultipleActiveResultSets = true
            };

            // SQL-Verbindung verwenden
            using (SqlConnection con = new SqlConnection(sqlSb.ConnectionString))
            {
                try
                {
                    con.Open();

                    string query = "SELECT COUNT(*) FROM Reservierung";

                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        int anzahlReservierungen = (int)command.ExecuteScalar();

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
                DataSource = @"(LocalDb)\MSSQLLocalDB", 
                InitialCatalog = "BesuchermanagmentDatenbank",
                IntegratedSecurity = true,
                MultipleActiveResultSets = true
            };

            // SQL-Verbindung verwenden
            using (SqlConnection con = new SqlConnection(sqlSb.ConnectionString))
            {
                try
                {
                    con.Open();

                    string query = "SELECT COUNT(*) FROM Reservierung WHERE Anwesend = 1";

                    using (SqlCommand command = new SqlCommand(query, con))
                    {
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
                DataSource = @"(LocalDb)\MSSQLLocalDB",
                InitialCatalog = "BesuchermanagmentDatenbank",
                IntegratedSecurity = true,
                MultipleActiveResultSets = true
            };

            // SQL-Verbindung verwenden
            using (SqlConnection con = new SqlConnection(sqlSb.ConnectionString))
            {
                try
                {
                    con.Open();

                    string query = "SELECT COUNT(*) FROM Reservierung WHERE Anwesend = 0";

                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        int anzahlAbwesende = (int)command.ExecuteScalar();

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

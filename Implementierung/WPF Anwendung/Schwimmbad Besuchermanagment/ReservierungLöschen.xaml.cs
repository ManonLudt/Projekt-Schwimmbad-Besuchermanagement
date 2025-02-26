using Microsoft.Data.SqlClient;
using Schwimmbad_Besuchermanagment.Models;
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
using System.Windows.Shapes;

namespace Schwimmbad_Besuchermanagment
{
    /// <summary>
    /// Interaktionslogik für ReservierungLöschen.xaml
    /// </summary>
    public partial class ReservierungLöschen : Window
    {
        public ReservierungLöschen()
        {
            InitializeComponent();
        }

        private void ReservierungLöschen_Click(object sender, RoutedEventArgs e)
        {
            // Überprüfen, ob eine gültige ReservierungsID eingegeben wurde
            if (string.IsNullOrEmpty(txtReservierungID.Text))
            {
                MessageBox.Show("Bitte eine ReservierungID eingeben!");
                return;
            }

            if (!int.TryParse(txtReservierungID.Text, out int idReservierung))
            {
                MessageBox.Show("Ungültige ReservierungID!");
                return;
            }

            // Überprüfen, ob die ReservierungsID nicht 0 ist
            if (idReservierung == 0)
            {
                MessageBox.Show("Die ReservierungsID darf nicht 0 sein!");
                return;
            }

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

                    string query = "DELETE FROM Reservierung WHERE Id_Reservierung = @IdReservierung";

                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        command.Parameters.AddWithValue("@IdReservierung", idReservierung);

                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Reservierung erfolgreich gelöscht!");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Reservierung mit dieser ID existiert nicht.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fehler: " + ex.Message);
                }
            }
        }
    }
}


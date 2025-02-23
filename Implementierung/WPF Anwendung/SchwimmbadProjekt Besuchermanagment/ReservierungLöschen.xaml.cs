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
using System.Windows.Shapes;

namespace SchwimmbadProjekt_Besuchermanagment
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
            int? idReservierung = string.IsNullOrEmpty(txtReservierungID.Text) ? (int?)null : Convert.ToInt32(txtReservierungID.Text);

            // Prüfen, ob die ID gültig ist
            if (idReservierung == null)
            {
                MessageBox.Show("Bitte geben Sie eine gültige Reservierungs-ID ein!");
                return;
            }

            // Überprüfen, ob alle Eingabefelder ausgefüllt sind
            //if (string.IsNullOrEmpty(idKartenText) || string.IsNullOrEmpty(bezeichnungText) ||
            //    string.IsNullOrEmpty(preisText) || string.IsNullOrEmpty(anzahlText))
            //{
            //    ErrorAusgabe.Text = "Bitte füllen Sie alle Felder aus.";
            //    txtTicketID.BorderBrush = Brushes.Red;
            //    txtTicketBezeichnung.BorderBrush = Brushes.Red;
            //    txtTicketPreis.BorderBrush = Brushes.Red;
            //    txtTicketAnzahl.BorderBrush = Brushes.Red;
            //}


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

                    // SQL-Anweisung zum Löschen des Tickets
                    string query = "DELETE FROM Reservierung WHERE Id_Reservierung = @IdReservierung";

                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        // Parameter für die SQL-Anweisung hinzufügen
                        command.Parameters.AddWithValue("@IdReservierung", idReservierung);

                        // Ausführen der SQL-Anweisung
                        int result = command.ExecuteNonQuery();

                        // Prüfen, ob das Ticket erfolgreich gelöscht wurde
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

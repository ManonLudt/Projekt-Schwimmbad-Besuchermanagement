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

namespace Schwimmbad_Besuchermanagment
{
    /// <summary>
    /// Interaktionslogik für KundeLöschen.xaml
    /// </summary>
    public partial class KundeLöschen : Window
    {
        public KundeLöschen()
        {
            InitializeComponent();
        }

        private void KundeLöschen_Click(object sender, RoutedEventArgs e)
        {
            int? idKunde = null;

            // Überprüfen, ob die KundenID eingegeben wurde und sicherstellen, dass sie eine gültige Zahl ist und keine 0
            if (!string.IsNullOrEmpty(txtKundeID.Text))
            {
                if (!int.TryParse(txtKundeID.Text, out int tempID))
                {
                    MessageBox.Show("Ungültige KundenID!");
                    return;  // Rückgabe, falls ID ungültig
                }

                if (tempID == 0)
                {
                    MessageBox.Show("Die KundenID darf nicht 0 sein!");
                    return;  // Rückgabe, falls ID ungültig
                }
                idKunde = tempID;
            }

            // Prüfen, ob die ID gültig ist
            if (idKunde == null)
            {
                MessageBox.Show("Bitte eine KundenID eingeben!");
                return;
            }

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

                    // SQL-Anweisung zum Löschen des Besuchers
                    string query = "DELETE FROM Besucher WHERE Id_Besucher = @IdKunde";

                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        // Parameter für die SQL-Anweisung hinzufügen
                        command.Parameters.AddWithValue("@IdKunde", idKunde);

                        // Ausführen der SQL-Anweisung
                        int result = command.ExecuteNonQuery();

                        // Prüfen, ob der Kunde erfolgreich gelöscht wurde
                        if (result > 0)
                        {
                            MessageBox.Show("Kunde erfolgreich gelöscht!");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Kunde mit dieser ID existiert nicht.");
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

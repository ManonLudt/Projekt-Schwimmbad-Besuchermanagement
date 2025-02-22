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
    /// Interaktionslogik für KundenAnlegen.xaml
    /// </summary>
    public partial class KundenAnlegen : Window
    {
        public KundenAnlegen()
        {
            InitializeComponent();
        }
        private void KundeAnlegen_Click(object sender, RoutedEventArgs e)
        {
            // Eingabewerte aus den Textboxen holen
            int? idBesucher = string.IsNullOrEmpty(txtKundeID.Text) ? (int?)null : Convert.ToInt32(txtKundeID.Text);
            string vorname = txtKundenVorname.Text;
            string nachname = txtKundenNachname.Text;
            int? alterKunde = string.IsNullOrEmpty(txtKundenalter.Text) ? (int?)null : Convert.ToInt32(txtKundenalter.Text);

            // Status aus den Checkboxen ermitteln
            string status = "";
            if (chkKeinen.IsChecked == true)
            {
                status = "Keinen";
            }
            else if (chkSchueler.IsChecked == true)
            {
                status = "Schüler";
            }
            else if (chkStudent.IsChecked == true)
            {
                status = "Student";
            }

            // Überprüfen, ob alle Eingabefelder ausgefüllt sind
            if (string.IsNullOrEmpty(vorname) || string.IsNullOrEmpty(nachname) || alterKunde == null || string.IsNullOrEmpty(status))
            {
                ErrorAusgabe.Text = "Bitte füllen Sie alle Felder aus.";
                return;  // Frühzeitiger Rückgabepunkt bei Fehler
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

                    // SQL-Anweisung zum Einfügen des Besuchers
                    string query = "INSERT INTO Besucher (Id_Besucher, Vorname, Nachname, Alter, Status) VALUES (@IdBesucher, @Vorname, @Nachname, @Alter, @Status)";

                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        // Parameter für die SQL-Anweisung hinzufügen
                        command.Parameters.AddWithValue("@IdBesucher", (object)idBesucher ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Vorname", vorname);
                        command.Parameters.AddWithValue("@Nachname", nachname);
                        command.Parameters.AddWithValue("@Alter", (object)alterKunde ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Status", status);

                        // Ausführen der SQL-Anweisung
                        int result = command.ExecuteNonQuery();

                        // Prüfen, ob die Einfügung erfolgreich war
                        if (result > 0)
                        {
                            MessageBox.Show("Kunde erfolgreich erstellt!");
                        }
                        else
                        {
                            MessageBox.Show("Fehler beim Erstellen des Kunden.");
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

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
    /// Interaktionslogik für KundeAnlegen.xaml
    /// </summary>
    public partial class KundeAnlegen : Window
    {
        public KundeAnlegen()
        {
            InitializeComponent();
        }

        private void KundeAnlegen_Click(object sender, RoutedEventArgs e)
        {
            // Eingabewerte aus den Textboxen holen
            int? idBesucher = null;
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
                idBesucher = tempID;
            }

            string vorname = txtKundenVorname.Text;
            string nachname = txtKundenNachname.Text;

            int? alterKunde = null;
            if (!string.IsNullOrEmpty(txtKundenalter.Text))
            {
                if (!int.TryParse(txtKundenalter.Text, out int tempAlter))
                {
                    MessageBox.Show("Ungültiges Alter!");
                    return;  // Rückgabe, falls Alter ungültig
                }

                if (tempAlter <= 9)
                {
                    MessageBox.Show("Alter muss größer als 9 sein");
                    return;  // Rückgabe, falls Alter ungültig
                }
                alterKunde = tempAlter;
            }

            // Status aus den Checkboxen ermitteln
            string status = "";
            if (chkKeinen.IsChecked == true)
            {
                status = " ";
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
                MessageBox.Show("Bitte füllen Sie alle Felder aus!");
                return;  // Frühzeitiger Rückgabepunkt bei Fehler
            }

            // Überprüfen, ob die KundenID 0 ist
            if (idBesucher == 0)
            {
                MessageBox.Show("Die KundenID darf nicht 0 sein!");
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

                    string setIdentityOnQuery = "SET IDENTITY_INSERT Besucher ON;";
                    using (SqlCommand setIdentityCommand = new SqlCommand(setIdentityOnQuery, con))
                    {
                        setIdentityCommand.ExecuteNonQuery();
                    }

                    // SQL-Anweisung zum Einfügen des Besuchers
                    string query = "INSERT INTO Besucher (Id_Besucher, Vorname, Nachname, AlterKunde, Status) VALUES (@IdBesucher, @Vorname, @Nachname, @Alter, @Status)";

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
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Fehler beim Erstellen des Kunden.");
                        }
                    }

                    string setIdentityOffQuery = "SET IDENTITY_INSERT Besucher OFF;";
                    using (SqlCommand setIdentityOffCommand = new SqlCommand(setIdentityOffQuery, con))
                    {
                        setIdentityOffCommand.ExecuteNonQuery();
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

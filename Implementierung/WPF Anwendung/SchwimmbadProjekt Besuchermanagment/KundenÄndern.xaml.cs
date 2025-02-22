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
    /// Interaktionslogik für KundenÄndern.xaml
    /// </summary>
    public partial class KundenÄndern : Window
    {
        public KundenÄndern()
        {
            InitializeComponent();
        }
        private void KundeÄndern_Click(object sender, RoutedEventArgs e)
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

            // Überprüfen, ob die ID vorhanden ist (der Kunde muss existieren)
            if (idBesucher == null)
            {
                MessageBox.Show("Bitte geben Sie eine gültige KundeID ein.");
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

                    // Erstelle die SQL-Anweisung zum Aktualisieren des Kunden
                    string query = "UPDATE Besucher SET ";
                    List<string> updateFields = new List<string>();

                    // Wenn Vorname eingegeben wurde, zum Update-Statement hinzufügen
                    if (!string.IsNullOrEmpty(vorname))
                    {
                        updateFields.Add("Vorname = @Vorname");
                    }

                    // Wenn Nachname eingegeben wurde, zum Update-Statement hinzufügen
                    if (!string.IsNullOrEmpty(nachname))
                    {
                        updateFields.Add("Nachname = @Nachname");
                    }

                    // Wenn Alter eingegeben wurde, zum Update-Statement hinzufügen
                    if (alterKunde.HasValue)
                    {
                        updateFields.Add("Alter = @Alter");
                    }

                    // Wenn Status ausgewählt wurde, zum Update-Statement hinzufügen
                    if (!string.IsNullOrEmpty(status))
                    {
                        updateFields.Add("Status = @Status");
                    }

                    // Wenn es Felder gibt, die aktualisiert werden sollen
                    if (updateFields.Count > 0)
                    {
                        query += string.Join(", ", updateFields) + " WHERE Id_Besucher = @IdBesucher";
                    }
                    else
                    {
                        MessageBox.Show("Bitte füllen Sie mindestens ein Feld aus, um Änderungen vorzunehmen.");
                        return; // Keine Änderungen, wenn keine Eingabe erfolgt
                    }

                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        // Parameter für die SQL-Anweisung hinzufügen
                        command.Parameters.AddWithValue("@IdBesucher", idBesucher);

                        if (!string.IsNullOrEmpty(vorname))
                            command.Parameters.AddWithValue("@Vorname", vorname);
                        if (!string.IsNullOrEmpty(nachname))
                            command.Parameters.AddWithValue("@Nachname", nachname);
                        if (alterKunde.HasValue)
                            command.Parameters.AddWithValue("@Alter", alterKunde.Value);
                        if (!string.IsNullOrEmpty(status))
                            command.Parameters.AddWithValue("@Status", status);

                        // Ausführen der SQL-Anweisung
                        int result = command.ExecuteNonQuery();

                        // Prüfen, ob die Aktualisierung erfolgreich war
                        if (result > 0)
                        {
                            MessageBox.Show("Kunde erfolgreich aktualisiert!");
                        }
                        else
                        {
                            MessageBox.Show("Fehler beim Aktualisieren des Kunden oder Kunde nicht gefunden.");
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
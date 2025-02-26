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
            // ID wird geprüft
            int? idBesucher = null;
            if (!string.IsNullOrEmpty(txtKundeID.Text))
            {
                if (!int.TryParse(txtKundeID.Text, out int tempID))
                {
                    MessageBox.Show("Ungültige KundenID!");
                    return;  
                }

                if (tempID == 0)
                {
                    MessageBox.Show("Die KundenID darf nicht 0 sein!");
                    return;  
                }
                idBesucher = tempID;
            }

            string vorname = txtKundenVorname.Text;
            string nachname = txtKundenNachname.Text;

            //Alter wird geprüft
            int? alterKunde = null;
            if (!string.IsNullOrEmpty(txtKundenalter.Text))
            {
                if (!int.TryParse(txtKundenalter.Text, out int tempAlter))
                {
                    MessageBox.Show("Ungültiges Alter!");
                    return;  
                }

                if (tempAlter <= 9)
                {
                    MessageBox.Show("Alter muss größer als 9 sein");
                    return; 
                }
                alterKunde = tempAlter;
            }

            // Überprüfen, ob mindestens eines der Felder ausgefüllt ist
            if (idBesucher == null && string.IsNullOrEmpty(vorname) && string.IsNullOrEmpty(nachname) && alterKunde == null)
            {
                MessageBox.Show("Bitte füllen Sie mindestens ein Feld aus.");
                return;  
            }

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

                    string query = "UPDATE Besucher SET ";
                    List<string> updateFields = new List<string>();

                    if (!string.IsNullOrEmpty(vorname))
                    {
                        updateFields.Add("Vorname = @Vorname");
                    }

                    if (!string.IsNullOrEmpty(nachname))
                    {
                        updateFields.Add("Nachname = @Nachname");
                    }

                    if (alterKunde.HasValue)
                    {
                        updateFields.Add("AlterKunde = @Alter");
                    }

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
                        return; 
                    }

                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        command.Parameters.AddWithValue("@IdBesucher", idBesucher);

                        if (!string.IsNullOrEmpty(vorname))
                            command.Parameters.AddWithValue("@Vorname", vorname);
                        if (!string.IsNullOrEmpty(nachname))
                            command.Parameters.AddWithValue("@Nachname", nachname);
                        if (alterKunde.HasValue)
                            command.Parameters.AddWithValue("@Alter", alterKunde.Value);
                        if (!string.IsNullOrEmpty(status))
                            command.Parameters.AddWithValue("@Status", status);

                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Kunde erfolgreich aktualisiert!");
                            this.Close();
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

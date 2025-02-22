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
    /// Interaktionslogik für KundenAusgaben.xaml
    /// </summary>
    public partial class KundenAusgabe : Page
    {
        // Der Kontext wird als Klassenvariable angelegt, damit alle Methoden auf denselben Kontext zugreifen können.
        private BesuchermanagmentDatenbankContext _context = new BesuchermanagmentDatenbankContext();

        //private ListCollectionView DisplayView;
        private ICollectionView DisplayView;
        public KundenAusgabe()
        {
            InitializeComponent();

            _context.Besuchers
            .Load();                         // Lade alle Entitäten


            DisplayView = CollectionViewSource.GetDefaultView(_context.Besuchers.Local.ToObservableCollection());
            DataContext = DisplayView;
        }

        private void KundeLöschen_Click(object sender, RoutedEventArgs e)
        {
            KundeLöschen KundeLöschenWindow = new KundeLöschen();
            KundeLöschenWindow.Show();
        }

        private void KundenAnlegen_Click(object sender, RoutedEventArgs e)
        {
            KundeAnlegen KundeAnlegenWindow = new KundeAnlegen();
            KundeAnlegenWindow.Show();
        }

        private void KundeÄndern_Click(object sender, RoutedEventArgs e)
        {
            KundenÄndern KundeÄndernWindow = new KundenÄndern();
            KundeÄndernWindow.Show();
        }
    }
}



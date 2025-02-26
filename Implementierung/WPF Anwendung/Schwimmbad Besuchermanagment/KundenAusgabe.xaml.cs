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
    /// Interaktionslogik für KundenAusgabe.xaml
    /// </summary>
    public partial class KundenAusgabe : Page
    {
        private BesuchermanagmentDatenbankContext _context = new BesuchermanagmentDatenbankContext();

        private ICollectionView DisplayView;
        public KundenAusgabe()
        {
            InitializeComponent();
            _context.Besuchers
            .Load();                         

            DisplayView = CollectionViewSource.GetDefaultView(_context.Besuchers.Local.ToObservableCollection());
            DataContext = DisplayView;
        }

        //Click-Events
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

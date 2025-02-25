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
    /// Interaktionslogik für TicketAusgabe.xaml
    /// </summary>
    public partial class TicketAusgabe : Page
    {
        // Der Kontext wird als Klassenvariable angelegt, damit alle Methoden auf denselben Kontext zugreifen können.
        private BesuchermanagmentDatenbankContext _context = new BesuchermanagmentDatenbankContext();

        //private ListCollectionView DisplayView;
        private ICollectionView DisplayView;
        public TicketAusgabe()
        {
            InitializeComponent();
            _context.Tickets.Load();
            DisplayView = CollectionViewSource.GetDefaultView(_context.Tickets.Local.ToObservableCollection());
            DataContext = DisplayView;
        }

        private void TicketLöschen_Click(object sender, RoutedEventArgs e)
        {
            TicketLöschen TicketLöschenWindow = new TicketLöschen();
            TicketLöschenWindow.Show();
        }

        private void TicketAnlegen_Click(object sender, RoutedEventArgs e)
        {
            TicketAnlegen TicketAnlegenWindow = new TicketAnlegen();
            TicketAnlegenWindow.Show();
        }

        private void TicketÄndern_Click(object sender, RoutedEventArgs e)
        {
            TicketÄndern TicketÄndernWindow = new TicketÄndern();
            TicketÄndernWindow.Show();
        }
    }
}

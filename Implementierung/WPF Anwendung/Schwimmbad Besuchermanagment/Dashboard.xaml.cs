﻿using System;
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
    /// Interaktionslogik für Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        //click-Events
        private void DashboardMenüClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new DashboardAusgabe();
        }

        private void KundeMenüClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new KundenAusgabe();
        }

        private void TicketMenüClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new TicketAusgabe();
        }

        private void BenutzerMenüClick(object sender, RoutedEventArgs e)
        {
        }

        private void LogoutMenüClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}

using Domein.Interfaces;
using Domein.Service;
using GUI.ViewModels;
using SQLserver.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GUI {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        public void Application_Startup(object sender, StartupEventArgs e) {
            MainWindow window = new MainWindow()
            {
                Title = "Voetbal truitjes beheer",
                DataContext = new KlantenViewModel()
            };

            window.Show();
        }
    }
}

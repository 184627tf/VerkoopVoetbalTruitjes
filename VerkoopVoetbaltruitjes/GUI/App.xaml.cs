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
            };

            window.Show();
        }
    }
}

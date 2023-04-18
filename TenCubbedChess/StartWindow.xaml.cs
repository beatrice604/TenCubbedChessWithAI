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

namespace TenCubbedChess
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        private void btnAI_Click(object sender, RoutedEventArgs e)
        {
            var gameType = 0;//0=AI
            MainWindow mainWindow = new MainWindow(gameType);
            this.Close();
            mainWindow.Show();
        }

        private void btnHuman_Click(object sender, RoutedEventArgs e)
        {
           ServerClientWindow serverClientWindow = new ServerClientWindow();
            this.Close();
            serverClientWindow.Show();  
        }
    }
}

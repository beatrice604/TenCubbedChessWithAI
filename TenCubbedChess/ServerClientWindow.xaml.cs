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
    /// Interaction logic for ServerClientWindow.xaml
    /// </summary>
    public partial class ServerClientWindow : Window
    {
        public ServerClientWindow()
        {
            InitializeComponent();
        }


        private void btnServerClient(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            int gameType=0;
            switch(btn!.Name)
            {
                case "btnServer":
                    gameType = 1;
                    break;
                case "btnClient":
                    gameType = 2;
                    break;
                default:
                    break;
            }
            if (gameType!=0)
            {
                MainWindow mainWindow = new MainWindow(gameType);
                mainWindow.Show();
                this.Close();
            }
        }
    }
}

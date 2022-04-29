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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopCheckers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainVindowViewModel();
        }

        private void StackPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is StackPanel panel) 
            {
                if (panel.DataContext is BoardField field)
                {
                    var position = field.Position;
                    if (position != null)
                    {
                        (DataContext as MainVindowViewModel)?.OnFieldClicked(position.Row, position.Column);
                    }
                }
            }
        }

        private async void Start_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainVindowViewModel)?.StartGame();
        }
    }
}

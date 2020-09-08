using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FFXIVRelicTracker.Views
{
    /// <summary>
    /// Interaction logic for AddCharacterWindow.xaml
    /// </summary>
    public partial class AddCharacterWindow : Window
    {
        public AddCharacterWindow()
        {
            InitializeComponent();
            this.DataContext = MainWindow.DataContextProperty;
        }

        private void ContinueBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            NewServer.Text = "";
            this.Close();
        }
    }
}

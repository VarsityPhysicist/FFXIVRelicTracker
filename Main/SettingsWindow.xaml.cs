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

namespace FFXIVRelicTracker.Main
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            this.DataContext = MainWindow.DataContextProperty;

            this.FontSize = Application.Current.Windows[0].FontSize;
        }

        private void ContinueBtn_Click(object sender, RoutedEventArgs e)
        {

            double FontSetting = Application.Current.Windows[0].FontSize;
            double.TryParse(this.FontButton.Text, out FontSetting);

            if (FontSetting == 0) { FontSetting = Application.Current.Windows[0].FontSize; }

            foreach (Window window in Application.Current.Windows)
            {
                window.FontSize = FontSetting;
            }
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            FontButton.Text = Application.Current.Windows[0].FontSize.ToString(); 
            this.Close();
        }
    }
}

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
using System.Collections.ObjectModel;

namespace LivestreamerSpotlight
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();

            player_browse.Click += player_browse_Click;
            save.Click += save_Click;

            service.Items.Add("twitch");
            service.Items.Add("hitbox");
            quality.Items.Add("best");
            quality.Items.Add("high");
            quality.Items.Add("medium");
            quality.Items.Add("low");
            quality.Items.Add("audio");

            service.SelectedItem = Properties.Settings.Default.service;
            quality.SelectedItem = Properties.Settings.Default.quality;
            player.Text = Properties.Settings.Default.player;

            if (Properties.Settings.Default.chat == true)
            {
                chat_yes.IsChecked = true;
            } else
            {
                chat_no.IsChecked = true;
            }
        }

        private void player_browse_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".exe";
            dlg.Filter = "Executable (*.exe)|*.exe";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                player.Text = filename;
            }
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.player = player.Text;
            Properties.Settings.Default.service = service.Text;
            Properties.Settings.Default.quality = quality.Text;

            if (chat_yes.IsChecked == true)
            {
                Properties.Settings.Default.chat = true;
            } else
            {
                Properties.Settings.Default.chat = false;
            }

            Properties.Settings.Default.Save();
            this.Close();
        }
    }
}

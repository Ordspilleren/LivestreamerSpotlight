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
using System.Diagnostics;
using System.IO;

namespace LivestreamerSpotlight
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MouseLeftButtonDown += (o, e) => DragMove();

            // Set textbox and combobox to last used values
            streamName.Text = Properties.Settings.Default.LastEntry;
            streamQuality.SelectedIndex = Properties.Settings.Default.LastQuality;

            // !!Temporary solution for selecting a video player!!
            if (Properties.Settings.Default.Player == "")
            {
                MessageBox.Show("Please choose a video player");

                Microsoft.Win32.OpenFileDialog choosePlayer = new Microsoft.Win32.OpenFileDialog();

                choosePlayer.DefaultExt = ".exe";
                choosePlayer.Filter = "Executable Files (*.exe)|*.exe";

                Nullable<bool> result = choosePlayer.ShowDialog();

                if (result.HasValue && result.Value)
                {
                    Properties.Settings.Default.Player = choosePlayer.FileName;
                }
                else
                {
                    Application.Current.Shutdown();
                }
            }
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Application.Current.Shutdown();
        }

        string[] args = Environment.GetCommandLineArgs();
        async void EnterPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                // !!Temporary solution for passing stream URL to player, does not support multiple services!!
                Service service = new Service();
                string url = await service.TwitchTV(streamName.Text, streamQuality.Text);

                if (url != null)
                {
                    Process startStream = new Process();
                    startStream.StartInfo.FileName = Properties.Settings.Default.Player;
                    startStream.StartInfo.Arguments = url;
                    startStream.StartInfo.CreateNoWindow = true;
                    startStream.StartInfo.UseShellExecute = false;
                    startStream.Start();

                    // Settings are saved here instead of in a seperate event handler. This prevents them from being saved when the user explicitly exits with Escape.
                    // It also ensures that entered information is valid.
                    Properties.Settings.Default.LastQuality = streamQuality.SelectedIndex;
                    Properties.Settings.Default.LastEntry = streamName.Text;
                    Properties.Settings.Default.Save();
                    Application.Current.Shutdown();
                }
                else
                {
                    MessageBox.Show("Stream not found!");
                }
            }
        }
    }
}

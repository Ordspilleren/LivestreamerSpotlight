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
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            SettingsWindow settings = new SettingsWindow();

            if (e.Key == Key.Escape)
                Application.Current.Shutdown();

            if (e.Key == Key.F1)
                settings.Show();
        }

        void EnterPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                string service = "twitch.tv";

                switch (Properties.Settings.Default.service)
                {
                    case "twitch":
                        service = "twitch.tv/";
                        break;
                    case "hitbox":
                        service = "hitbox.tv/";
                        break;
                }

                Process startStream = new Process();
                startStream.StartInfo.FileName = Environment.CurrentDirectory + "/livestreamer/livestreamer.exe";
                startStream.StartInfo.Arguments = @"--player " + '"' + Properties.Settings.Default.player + '"' + " --player-passthrough=http,hls,rtmp " + service + streamName.Text + " " + Properties.Settings.Default.quality;
                startStream.StartInfo.CreateNoWindow = true;
                startStream.StartInfo.UseShellExecute = false;
                startStream.Start();

                if (Properties.Settings.Default.chat == true)
                {
                    Process startChat = new Process();
                    startChat.StartInfo.FileName = "chrome";
                    startChat.StartInfo.Arguments = "--app=http://www.twitch.tv/chat/embed?channel=" + streamName.Text + "&popout_chat=true";
                    startChat.Start();
                }
                Application.Current.Shutdown();
            }
        }

    }
}

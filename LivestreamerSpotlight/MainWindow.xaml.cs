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

            this.Visibility = Visibility.Hidden;
            MouseLeftButtonDown += (o, e) => DragMove();
            HotKey _hotKey = new HotKey(Key.S, KeyModifier.Shift | KeyModifier.Win, OnHotKeyHandler);
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(await Services.TwitchTV("Aui_2000", "low"));
        }

        private void OnHotKeyHandler(HotKey hotKey)
        {
            this.Visibility = Visibility.Visible;
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Visibility = Visibility.Hidden;
        }

        void EnterPressed(object sender, KeyEventArgs e)
        {
            var config = File.ReadAllLines(Environment.CurrentDirectory + "/settings.cfg")
                .Select(l => l.Split(new[] { '=' }))
                .ToDictionary(s => s[0].Trim(), s => s[1].Trim());

            if (e.Key == Key.Return)
            {
                string service = "twitch.tv";

                switch (config["service"])
                {
                    case "twitch":
                        service = "twitch.tv/";
                        break;
                    case "hitbox":
                        service = "hitbox.tv/";
                        break;
                }

                Process startStream = new Process();
                startStream.StartInfo.FileName = "livestreamer";
                startStream.StartInfo.Arguments = @"--player " + '"' + config["player"] + '"' + " --player-passthrough=http,hls,rtmp " + service + streamName.Text + " " + config["quality"];
                startStream.StartInfo.CreateNoWindow = true;
                startStream.StartInfo.UseShellExecute = false;
                startStream.Start();

                if (config["chat"] == "yes")
                {
                    Process startChat = new Process();
                    startChat.StartInfo.FileName = "chrome";
                    startChat.StartInfo.Arguments = "--app=http://www.twitch.tv/chat/embed?channel=" + streamName.Text + "&popout_chat=true";
                    startChat.Start();
                }
                this.Visibility = Visibility.Hidden;
            }
        }

    }
}

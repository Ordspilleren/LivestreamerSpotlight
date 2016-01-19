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

            //this.Visibility = Visibility.Hidden;
            MouseLeftButtonDown += (o, e) => DragMove();
            //HotKey _hotKey = new HotKey(Key.S, KeyModifier.Shift | KeyModifier.Win, OnHotKeyHandler);
        }

        private void OnHotKeyHandler(HotKey hotKey)
        {
            //this.Visibility = Visibility.Visible;
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                //this.Visibility = Visibility.Hidden;
                Application.Current.Shutdown();
        }

        async void EnterPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                string[] args = Environment.GetCommandLineArgs();
                Process startStream = new Process();
                startStream.StartInfo.FileName = args[2];
                startStream.StartInfo.Arguments = await Services.TwitchTV(streamName.Text, streamQuality.Text);
                startStream.StartInfo.CreateNoWindow = true;
                startStream.StartInfo.UseShellExecute = false;
                startStream.Start();

                //this.Visibility = Visibility.Hidden;
                Application.Current.Shutdown();
            }
        }

    }
}

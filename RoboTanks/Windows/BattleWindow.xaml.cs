using RoboTanks.Battle;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RoboTanks
{
    /// <summary>
    /// Interaction logic for BattleWindow.xaml
    /// </summary>
    public partial class BattleWindow : Window
    {
        private Map __map = null;
        private Map _map
        {
            get { return __map; }
            set
            {
                __map = value;
                canvas.Map = value;
            }
        }

        private BattleGround _bg;
        private BackgroundWorker _bw;

        public BattleWindow()
        {
            InitializeComponent();
        }

        public BattleWindow(string map, string[] tanks)
        {
            InitializeComponent();
            var mapfile = Path.Combine(Settings.MapsPath, map + ".map");
            if (File.Exists(mapfile))
                _map = new Map(File.OpenRead(mapfile));
            else
                _map = new Map(16, 16);

            var aifiles = tanks.Select(ai => Path.Combine(Settings.MapsPath, ai + ".dll")).ToArray();
            var ais = new IAI[tanks.Length];
            for (int i = 0; i < tanks.Length; i++)
            {
                if (File.Exists(aifiles[i]))
                {

                }
                else
                    ais[i] = new TestAI();
            }

            _bg = new BattleGround(_map, ais);
            _bw = new BackgroundWorker();
            _bw.WorkerReportsProgress = true;
            _bw.WorkerSupportsCancellation = true;
            _bw.DoWork += _bw_DoWork;
            _bw.ProgressChanged += _bw_ProgressChanged;
            _bw.RunWorkerAsync();
        }

        private void _bw_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!_bw.CancellationPending)
            {
                _bw.ReportProgress(0);
                Thread.Sleep(100);
            }
        }

        private void _bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            _bg.Update();
            canvas.InvalidateVisual();
        }
    }
}

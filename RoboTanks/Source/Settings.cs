using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RoboTanks
{
    public class Settings
    {
        public static Version CurrentVersion { get { return Assembly.GetExecutingAssembly().GetName().Version; } }
        public static string AppDataPath { get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Porohkun", "RoboTanks"); } }
        public static string AppPath { get { return AppDomain.CurrentDomain.BaseDirectory; } }
        public static string TanksPath { get { return Path.Combine(AppPath, "Tanks"); } }
        public static string MapsPath { get { return Path.Combine(AppPath, "Maps"); } }
    }
}
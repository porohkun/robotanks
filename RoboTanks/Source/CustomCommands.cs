using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RoboTanks
{
    static class CustomCommands
    {
        public static RoutedCommand ZoomIn = new RoutedCommand();
        public static RoutedCommand ZoomOut = new RoutedCommand();

        public static RoutedCommand S_dirt = new RoutedCommand();
        public static RoutedCommand S_grass = new RoutedCommand();
        public static RoutedCommand S_gravel = new RoutedCommand();
        public static RoutedCommand S_water = new RoutedCommand();
        public static RoutedCommand S_none = new RoutedCommand();
        public static RoutedCommand S_rock = new RoutedCommand();
        public static RoutedCommand S_tree = new RoutedCommand();
        public static RoutedCommand S_wall = new RoutedCommand();
        public static RoutedCommand S_tank1 = new RoutedCommand();
        public static RoutedCommand S_tank2 = new RoutedCommand();
        public static RoutedCommand S_tank3 = new RoutedCommand();
        public static RoutedCommand S_tank4 = new RoutedCommand();
    }
}

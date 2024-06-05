using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace LB3_Alkhimovich
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    /// 

    public class Params
    {
        private int tB1=1;
        private Color tB2=Colors.AliceBlue;
        private Color tB3=Colors.Aqua;

        public int TB1 { get => tB1; set => tB1 = value; }
        public Color TB2 { get => tB2; set => tB2 = value; }
        public Color TB3 { get => tB3; set => tB3 = value; }
    }
    public partial class App : Application
    {
    }
}

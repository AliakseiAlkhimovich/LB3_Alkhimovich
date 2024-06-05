using LB3_Alkhimovich.Properties;
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
using Microsoft.Win32;
namespace LB3_Alkhimovich
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Setting : Window
    {
        public int tB1 = 1;
        public Color tB2 = Colors.AliceBlue;
        public Color tB3 = Colors.Aqua;

        public Setting()
        {
            InitializeComponent();
            InitializeComboBoxes();
        }

        public Setting(Params setting):this()
        {
            this.DataContext = setting;
            tB1 = setting.TB1;
            tB2 = setting.TB2;
            tB3 = setting.TB3;
        }

        private void InitializeComboBoxes()
        {


            // Инициализация ComboBox для цвета фона
            var backgroundColorComboBox = FindName("BackgroundColorComboBox") as ComboBox;
            backgroundColorComboBox.ItemsSource = new List<string> { "LightBlue", "LightGreen", "LightYellow", "White", "Black" };

            // Инициализация ComboBox для цвета линии фигуры
            var shapeLineColorComboBox = FindName("ShapeLineColorComboBox") as ComboBox;
            shapeLineColorComboBox.ItemsSource = new List<string> { "Black", "Red", "Green", "Blue" };

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}

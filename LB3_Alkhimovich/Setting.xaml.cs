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

namespace LB3_Alkhimovich
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Setting : Window
    {
        public Setting()
        {
            InitializeComponent();
            InitializeComboBoxes();
        }

        private void InitializeComboBoxes()
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            // Инициализация ComboBox для толщины линии
            var lineThicknessComboBox = FindName("LineThicknessComboBox") as ComboBox;
            lineThicknessComboBox.ItemsSource = new List<int> {1,2,3,4,5,6,7,8,9,10};
            lineThicknessComboBox.Text = mainWindow.TB1.Text;

            // Инициализация ComboBox для цвета фона
            var backgroundColorComboBox = FindName("BackgroundColorComboBox") as ComboBox;
            backgroundColorComboBox.ItemsSource = new List<string> { "LightBlue", "LightGreen", "LightYellow", "White", "Black" };
            backgroundColorComboBox.Text = mainWindow.TB2.Text; // Установка первого элемента по умолчанию

            // Инициализация ComboBox для цвета линии фигуры
            var shapeLineColorComboBox = FindName("ShapeLineColorComboBox") as ComboBox;
            shapeLineColorComboBox.ItemsSource = new List<string> { "Black", "Red", "Green", "Blue" };
            shapeLineColorComboBox.Text = mainWindow.TB3.Text; // Установка первого элемента по умолчанию
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            
            int selectedLineThickness = (int)LineThicknessComboBox.SelectedItem;
            string selectedBackgroundColor = (string)BackgroundColorComboBox.SelectedItem;
            string selectedShapeLineColor = (string)ShapeLineColorComboBox.SelectedItem;

            
            mainWindow.TB1.Text = selectedLineThickness.ToString();
            mainWindow.TB2.Text = selectedBackgroundColor;
            mainWindow.TB3.Text = selectedShapeLineColor;
            //mainWindow = Application.Current.MainWindow as MainWindow;
            //mainWindow.CloseSettingsWindow();
            Close();
        }
    }
}

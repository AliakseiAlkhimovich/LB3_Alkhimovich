using Microsoft.Win32;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.IO;
using System.Collections.Generic;

namespace LB3_Alkhimovich
{
    public partial class MainWindow : Window
    {
        // private Setting settingsWindow;

        public Params Params;

        public MainWindow()
        {
            SplashScreen splash = new SplashScreen("1056985.png");
            splash.Show(true, true);

            InitializeComponent();
            Params = new Params();
            this.DataContext = Params;
            Loaded += (s, e) => drawingCanvas.Focus();
            CommandBinding saveCommandBinding = new CommandBinding(ApplicationCommands.Save, Save_Executed, Save_CanExecute);

            CommandBinding openCommandBinding = new CommandBinding(ApplicationCommands.Open, Open_Executed);

            CommandBinding newCommandBinding = new CommandBinding(ApplicationCommands.New, New_Executed);

            CommandBinding propCommandBinding = new CommandBinding(ApplicationCommands.Properties, Properties_Executed);

            List<CommandBinding> list = new List<CommandBinding> {
        saveCommandBinding,
        openCommandBinding,
        newCommandBinding,
        propCommandBinding
      };
            this.CommandBindings.AddRange(list);

        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DrawStar(e.GetPosition(drawingCanvas));
            }
        }

        private void DrawStar(Point position)
        {
            Polygon star = new Polygon
            {
                Stroke = new SolidColorBrush(Params.TB3),
                Fill = new SolidColorBrush(Params.TB2),
                StrokeThickness = Params.TB1
            };

            // Определение точек для четырехконечной звезды
            PointCollection points = new PointCollection {
        new Point(position.X, position.Y - 70), // Верхняя точка
          new Point(position.X + 10, position.Y - 10), // Правая верхняя точка
          new Point(position.X + 70, position.Y), // Правая точка
          new Point(position.X + 10, position.Y + 10), // Правая нижняя точка
          new Point(position.X, position.Y + 70), // Нижняя точка
          new Point(position.X - 10, position.Y + 10), // Левая нижняя точка
          new Point(position.X - 70, position.Y), // Левая точка
          new Point(position.X - 10, position.Y - 10) // Левая верхняя точка
      };

            star.Points = points;
            drawingCanvas.Children.Add(star);
        }

        void newfile()
        {
            drawingCanvas.Children.Clear();
            this.Title = "Графический Редактор";
        }
        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            newfile();
        }

        void prop()
        {
            //if (settingsWindow == null || !settingsWindow.IsLoaded)
            //{

            Setting settingsWindow = new Setting(Params);
            if (settingsWindow.ShowDialog() == false)
            {
                Params.TB1 = settingsWindow.tB1;
                Params.TB2 = settingsWindow.tB2;
                Params.TB3 = settingsWindow.tB3;
                this.DataContext = null;
                this.DataContext = Params;
            }
        }
        private void OpenSettings_Click(object sender, RoutedEventArgs e)
        {
            prop();
        }

        private void ShowMouseCoordinates(Point mousePosition)
        {
            // Assuming TBX and TBY are TextBox controls in your MainWindow
            TBX.Text = mousePosition.X.ToString(); // X-coordinate
            TBY.Text = mousePosition.Y.ToString(); // Y-coordinate
        }
        private void drawingCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePosition = e.GetPosition(drawingCanvas);
            ShowMouseCoordinates(mousePosition);
        }

        private void closeWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // Обработчик возможности выполнения команды Save
        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            // Условие возможности выполнения команды
            e.CanExecute = drawingCanvas.Children.Count != 0;
        }

        void save()
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt|Данные (*.dat)|*.dat|Все файлы (*.*)|*.*"
            };

            if (sfd.ShowDialog() == true)
            {
                // Логика сохранения данных в файл
                SaveDataToFile(sfd.FileName);
            }
        }

        // Обработчик выполнения команды Save
        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            save();
        }

        // Обработчик выполнения команды Save
        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            open();
        }

        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            newfile();
        }

        private void Properties_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            prop();
        }

        // Метод для сохранения данных в файл
        private void SaveDataToFile(string fileName)
        {
            // Создаем StringBuilder для сбора данных
            StringBuilder sb = new StringBuilder();

            // Перебираем все дочерние элементы Canvas
            foreach (var child in drawingCanvas.Children)
            {
                if (child is Polygon polygon)
                {
                    // Для каждой звезды записываем информацию
                    sb.AppendLine($"Star");
                    sb.AppendLine($"Stroke: {polygon.Stroke}");
                    sb.AppendLine($"Fill: {polygon.Fill}");
                    sb.AppendLine($"StrokeThickness: {polygon.StrokeThickness}");
                    sb.AppendLine($"Points:");
                    foreach (var point in polygon.Points)
                    {
                        sb.AppendLine($"{point.X};{point.Y}");
                    }
                    sb.AppendLine(); // Добавляем пустую строку для разделения фигур
                }
            }

            // Записываем данные в файл
            File.WriteAllText(fileName, sb.ToString());

            // Обновляем заголовок окна
            this.Title = fileName;
        }

        void open()
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*",
                Title = "Выберите файл с данными фигур"
            };

            if (ofd.ShowDialog() == true)
            {
                // Вызываем метод загрузки данных, передавая имя файла
                LoadData(ofd.FileName);
            }
        }

        private void LoadDataFromFile_Click(object sender, RoutedEventArgs e)
        {
            open();
        }

        private void LoadData(string fileName)
        {

            // Читаем все строки из файла
            string[] lines = File.ReadAllLines(fileName);

            // Переменные для хранения информации о фигуре
            Color strokeColor = Colors.Black;
            Color fillColor = Colors.Transparent;
            double strokeThickness = 1.0;
            PointCollection points = new PointCollection();

            // Перебираем строки файла
            foreach (string line in lines)
            {
                if (line.StartsWith("Star"))
                {
                    // Если начинается новая фигура, сбрасываем точки
                    points = new PointCollection();
                }
                else if (line.StartsWith("Stroke:"))
                {
                    // Извлекаем цвет обводки
                    strokeColor = (Color)ColorConverter.ConvertFromString(line.Replace("Stroke:", "").Trim());
                }
                else if (line.StartsWith("Fill:"))
                {
                    // Извлекаем цвет заливки
                    fillColor = (Color)ColorConverter.ConvertFromString(line.Replace("Fill:", "").Trim());
                }
                else if (line.StartsWith("StrokeThickness:"))
                {
                    // Извлекаем толщину обводки
                    strokeThickness = double.Parse(line.Replace("StrokeThickness:", "").Trim());
                }
                else if (line.StartsWith("Points:"))
                {
                    // Ничего не делаем, следующие строки будут точками
                }
                else if (line.Trim() == "")
                {
                    // Если строка пустая, создаем фигуру и добавляем на Canvas
                    Polygon polygon = new Polygon
                    {
                        Stroke = new SolidColorBrush(strokeColor),
                        Fill = new SolidColorBrush(fillColor),
                        StrokeThickness = strokeThickness,
                        Points = points
                    };
                    drawingCanvas.Children.Add(polygon);
                }
                else
                {
                    // Извлекаем точки и добавляем в коллекцию
                    string[] pointData = line.Split(';');
                    Point point = new Point(double.Parse(pointData[0]), double.Parse(pointData[1]));
                    points.Add(point);
                }
            }
            this.Title = fileName;
        }
    }
}
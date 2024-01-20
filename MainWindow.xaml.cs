using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;


namespace infograf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private int currentCase = 0;
        private bool isChartInitialized = false;

        public MainWindow()
        {
            InitializeComponent();
            AnimateButtons();
        }


        private void AnimateButtons()
        {
            AnimateOpacity(BackButton, 2);
            AnimateOpacity(ForwardButton, 2);
        }

        private void AnimateOpacity(UIElement element, double durationInSeconds)
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(durationInSeconds)
            };
            element.BeginAnimation(UIElement.OpacityProperty, animation);
        }

        private void CreateBarChart()
        {
            if (!isChartInitialized)
            {
                // Ініціалізація барів тут
                isChartInitialized = true;
            }
            UpdateBars();
        }

        private void UpdateBars()
        {
            MyCanvas.Children.Clear(); // Очищення поточних барів

            // Оновлення висот барів залежно від поточного кейсу
            switch (currentCase)
            {
                case 0:
                    break;
                case 1:
                    // Висоти для кейсу 1
                    AddBar(50, 70, 10);
                    AddBar(170, 70, 30);
                    AddBar(290, 70, 100);
                    AddBar(410, 70, 100);
                    break;
                case 2:
                    // Висоти для кейсу 2
                    AddBar(50, 70, 20);
                    AddBar(170, 70, 40);
                    AddBar(290, 70, 50);
                    AddBar(410, 70, 30);
                    break;
                case 3:
                    // Висоти для кейсу 3
                    AddBar(50, 70, 50);
                    AddBar(170, 70, 20);
                    AddBar(290, 70, 36);
                    AddBar(410, 70, 34);
                    break;
                case 4:
                    // Висоти для кейсу 4
                    AddBar(50, 70, 45);
                    AddBar(170, 70, 23);
                    AddBar(290, 70, 98);
                    AddBar(410, 70, 100);
                    break;
                case 5:
                    // Висоти для кейсу 5
                    AddBar(50, 70, 27);
                    AddBar(170, 70, 45);
                    AddBar(290, 70, 23);
                    AddBar(410, 70, 78);
                    break;
                default:
                    break;
            }
        }

        private void AddBar(double x, double width, double originalMaxHeight)
        {
            var color = GetColorByHeight(originalMaxHeight);
            double maxHeight = 3 * originalMaxHeight;  // Масштабування висоти
            double startY = 370 - maxHeight;

            var bar = new Rectangle
            {
                Width = width,
                Height = 0, // Стартова висота 0 для анімації
                Fill = new SolidColorBrush(color)
            };

            Canvas.SetLeft(bar, x);
            Canvas.SetTop(bar, startY + maxHeight);

            MyCanvas.Children.Add(bar);

            // Створення тексту для бару
            TextBlock percentageText = new TextBlock
            {
                Text = $"{originalMaxHeight}%",
                Foreground = new SolidColorBrush(Colors.Black),
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top
            };

            Canvas.SetLeft(percentageText, x + (width / 2) - (percentageText.ActualWidth / 2));
            Canvas.SetTop(percentageText, startY - 20); // Розташування тексту над баром

            MyCanvas.Children.Add(percentageText);

            // Анімації для бару
            DoubleAnimation heightAnimation = new DoubleAnimation
            {
                To = maxHeight,
                Duration = TimeSpan.FromSeconds(2)
            };

            ColorAnimation colorAnimation = new ColorAnimation
            {
                From = Colors.Blue,
                To = color,
                Duration = TimeSpan.FromSeconds(2)
            };

            DoubleAnimation topAnimation = new DoubleAnimation
            {
                To = startY,
                Duration = TimeSpan.FromSeconds(2)
            };

            bar.BeginAnimation(Rectangle.HeightProperty, heightAnimation);
            bar.BeginAnimation(Canvas.TopProperty, topAnimation);
            var solidColorBrush = bar.Fill as SolidColorBrush;
            if (solidColorBrush != null)
            {
                solidColorBrush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);
            }
        }


        private Color GetColorByHeight(double height)
        {

            if (height <= 70)
            {
                // Інтерполяція між зеленим і жовтим кольорами
                double percentage = height / 70.0; // Відсоток до 70
                byte green = (byte)(255 * (1 - percentage));
                byte red = (byte)(255 * percentage);
                return Color.FromRgb(red, 255, green);
            }
            else
            {
                // Інтерполяція між жовтим і червоним кольорами
                double percentage = (height - 70) / 30.0; // Відсоток після 70
                byte green = (byte)(255 * (1 - percentage));
                return Color.FromRgb(255, green, 0);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentCase == 0 || currentCase == 1)
            {
                currentCase = 5;
            }
            else
            {
                currentCase--;
            }
            CreateBarChart();
        }

        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentCase == 0 || currentCase == 5)
            {
                currentCase = 1;
            }
            else if (currentCase < 5) // Тепер максимальне значення - 5
            {
                currentCase++;
            }
            CreateBarChart();
        }
    }
}
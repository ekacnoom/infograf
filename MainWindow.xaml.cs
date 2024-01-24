using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Documents;


namespace infograf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private int currentCase = 0;
        private bool isChartInitialized = false;
        int barCount = 0;

        public MainWindow()
        {
            InitializeComponent();
            AnimateButtons();
            CreateCaseIndicator(); // Створення індикатора кейсу
        }

        private TextBlock? caseIndicator; // Індикатор кейсу

        private void CreateCaseIndicator()
        {
            caseIndicator = new TextBlock
            {
                FontWeight = FontWeights.Bold,
                FontSize = 16,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 10, 0, 0)
            };

            Canvas.SetTop(caseIndicator, 10);
            Canvas.SetRight(caseIndicator, 10);

            MyCanvas.Children.Add(caseIndicator);
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
            MyCanvas.Children.Clear();
            CreateCaseIndicator();

            barCount = 0;

            // Оновлення висот барів залежно від поточного кейсу
            if (caseIndicator != null)
            {
                switch (currentCase)
                {
                    case 0:
                        break;
                    case 1:
                        // Висоти для кейсу 1
                        caseIndicator.Text = "Оплата праці";
                        AddBar(50, 70, 76);
                        AddBar(170, 70, 81);
                        AddBar(290, 70, 89);
                        AddBar(410, 70, 82);
                        UpdateInfoText("Оплата праці – це витрати державного бюджету на заробітну плату працівників державного сектору. " +
                            "Включає заробітну плату, податки та соціальні внески, які платить держава.", 
                            "https://lb.ua/blog/tetiana_bohdan/505515_byudzhetni_vidatki_2021_roku_zleti_i.html");
                        break;
                    case 2:
                        // Висоти для кейсу 2
                        caseIndicator.Text = "Субсидії та поточні трансферти підприємствам";
                        AddBar(50, 70, 14);
                        AddBar(170, 70, 20);
                        AddBar(290, 70, 32);
                        AddBar(410, 70, 22);
                        UpdateInfoText("Субсидії та поточні трансферти підприємствам – це державні виплати, які не пов'язані з придбанням " +
                            "товарів чи послуг. Це включає дотації підприємствам для підтримки їх конкурентоспроможності, допомогу в складних " +
                            "фінансових умовах, стимулювання економічного розвитку.", 
                            "https://lb.ua/blog/tetiana_bohdan/505515_byudzhetni_vidatki_2021_roku_zleti_i.html");
                        break;
                    case 3:
                        // Висоти для кейсу 3
                        caseIndicator.Text = "Соціальне забезпечення";
                        AddBar(50, 70, 84);
                        AddBar(170, 70, 79);
                        AddBar(290, 70, 79);
                        AddBar(410, 70, 68);
                        UpdateInfoText("Соціальне забезпечення – це державні витрати на підтримку громадян у випадках, які вимагають соціальної " +
                            "допомоги: пенсії, допомога по безробіттю, соціальні виплати малозабезпеченим, інвалідам та іншим соціально " +
                            "вразливим категоріям населення.",
                            "https://lb.ua/blog/tetiana_bohdan/505515_byudzhetni_vidatki_2021_roku_zleti_i.html");
                        break;
                    case 4:
                        // Висоти для кейсу 4
                        caseIndicator.Text = "Капітальні видатки";
                        AddBar(50, 70, 41);
                        AddBar(170, 70, 39);
                        AddBar(290, 70, 40);
                        AddBar(410, 70, 40);
                        UpdateInfoText("Капітальні видатки – це частина державних видатків, які спрямовуються на придбання або підтримку " +
                            "довгострокових активів, таких як будівництво інфраструктури, закупівля обладнання, реконструкція та модернізація " +
                            "існуючих об'єктів.",
                            "https://lb.ua/blog/tetiana_bohdan/505515_byudzhetni_vidatki_2021_roku_zleti_i.html");
                        break;
                    default:
                        caseIndicator.Text = "";
                        break;
                }
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
                VerticalAlignment = VerticalAlignment.Bottom
            };

            Canvas.SetLeft(percentageText, x + width / 2);
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

            string[] years = { "2018", "2019", "2020", "2021" };
            if (barCount < years.Length)
            {
                TextBlock yearLabel = new TextBlock
                {
                    Text = years[barCount],
                    Foreground = new SolidColorBrush(Colors.Black),
                    FontWeight = FontWeights.Bold,
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                Canvas.SetLeft(yearLabel, x + 20);
                Canvas.SetTop(yearLabel, 380);
                MyCanvas.Children.Add(yearLabel);
            }

            barCount++;
        }


        private Color GetColorByHeight(double height)
        {

            if (height <= 70)
            {
                // Інтерполяція між зеленим і жовтим кольорами
                double percentage = height / 70.0;
                byte green = (byte)(255 * (1 - percentage));
                byte red = (byte)(255 * percentage);
                return Color.FromRgb(red, 255, green);
            }
            else
            {
                // Інтерполяція між жовтим і червоним кольорами
                double percentage = (height - 70) / 30.0;
                byte green = (byte)(255 * (1 - percentage));
                return Color.FromRgb(255, green, 0);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentCase == 0 || currentCase == 1)
            {
                currentCase = 4;
            }
            else
            {
                currentCase--;
            }
            CreateBarChart();
        }

        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentCase == 0 || currentCase == 4)
            {
                currentCase = 1;
            }
            else if (currentCase < 4)
            {
                currentCase++;
            }
            CreateBarChart();
        }

        public void UpdateInfoText(string description, string source)
        {
            // Очищаємо попередній контент
            InfoTextBlock.Inlines.Clear();

            // Додаємо опис
            InfoTextBlock.Inlines.Add(new Run($"Опис показника: {description}\n"));

            // Створюємо гіперпосилання
            Hyperlink sourceLink = new Hyperlink(new Run($"Джерело даних: {source}"))
            {
                NavigateUri = new Uri(source)
            };
            // Використовуємо обробник подій
            sourceLink.RequestNavigate += Hyperlink_RequestNavigate;

            // Додаємо гіперпосилання до TextBlock
            InfoTextBlock.Inlines.Add(sourceLink);
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            var psi = new System.Diagnostics.ProcessStartInfo
            {
                FileName = e.Uri.AbsoluteUri,
                UseShellExecute = true
            };
            System.Diagnostics.Process.Start(psi);
            e.Handled = true;
        }

    }
}

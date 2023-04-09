using System.Windows;
using System.Windows.Media;

namespace Crypto_App
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Логика при нажатии кнопки "Подписать" на первой вкладке приложения
        /// </summary>
        private void signButton_Click(object sender, RoutedEventArgs e)
        {
            //Получить данные из представления
            int encryptorType = signCypherCombobox.SelectedIndex;
            string message = signDataTextbox.Text,
                openKey, privateKey, hash, sign;
            try
            {
                ViewModel.SignViewModel viewModel = new ViewModel.SignViewModel(encryptorType, message);
                viewModel.GetResults(out openKey, out privateKey, out hash, out sign);
                //Вывести результат
                signResultsTextbox.Foreground = Brushes.Green;
                signResultsTextbox.Text = $"Готово!\nХеш документа: {hash}\nОткрытый ключ: {openKey}\nЗакрытый ключ: {privateKey}" +
                    $"\nПодпись: {sign}";
            }
            catch
            {
                signResultsTextbox.Foreground = Brushes.Red;
                signResultsTextbox.Text = "Что-то пошло не так...";
            }
        }
        /// <summary>
        /// Логика при нажатии кнопки "Проверить" на второй вкладке приложения
        /// </summary>
        private void checkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Получить данные из представления
                int encryptorType = checkCypherCombobox.SelectedIndex;
                string publicKey = checkKeyTextbox.Text,
                    message = checkMessageTextbox.Text,
                    sign = checkSignTextbox.Text;
                ViewModel.CheckViewModel viewModel = new ViewModel.CheckViewModel(encryptorType, message, sign, publicKey);
                if (viewModel.GetResults())
                {
                    checkResultsLabel.Foreground = Brushes.Green;
                    checkResultsLabel.Content = "Подпись корректна";
                }
                else
                {
                    checkResultsLabel.Foreground = Brushes.Red;
                    checkResultsLabel.Content = "Подпись некорректна";
                }
            }
            catch
            {
                checkResultsLabel.Foreground = Brushes.Red;
                checkResultsLabel.Content = "Что-то пошло не так...";
            }
            
        }

    }
}

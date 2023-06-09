﻿using System.Windows;
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
            string message = signDataTextbox.Text;
            try
            {
                ViewModel.SignViewModel viewModel = new ViewModel.SignViewModel(encryptorType, message);
                //Вывести результат
                signResultsTextbox.Foreground = Brushes.Green;
                signResultsTextbox.Text = viewModel.GetResults();
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
                bool succeed;
                checkResultsLabel.Content = viewModel.GetResults(out succeed);
                checkResultsLabel.Foreground = succeed ? Brushes.Green : Brushes.Red;
            }
            catch
            {
                checkResultsLabel.Foreground = Brushes.Red;
                checkResultsLabel.Content = "Что-то пошло не так...";
            }
            
        }

    }
}

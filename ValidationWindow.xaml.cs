using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfAppModuleSix
{
    /// <summary>
    /// Логика взаимодействия для ValidationWindow.xaml
    /// </summary>
    public partial class ValidationWindow : Window
    {
        private string fullNamePattern = @"^[А-ЯЁ][а-яё]+ [А-ЯЁ][а-яё]+ [А-ЯЁ][а-яё]+$";
        private string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        private string innPattern = @"^\d{10}$|^\d{12}$";
        private string mobilePhonePattern = @"^\+7 \d{3} \d{3}-\d{2}-\d{2}$";
        public ValidationWindow()
        {
            InitializeComponent();
        }

        private async void Get_Data_Button_Click(object sender, RoutedEventArgs e)
        {
            string url = "http://localhost:4444/TransferSimulator/inn";
            string data = await ServerRequest.GetRequest(url);

            if (data != null)
            {
                DataTextBlock.Text = GetDataFromJSON(data);
            }
        }

        private string GetDataFromJSON(string data)
        {
            return data
                .Substring(data.IndexOf(":") + 2)
                .Replace("\"", "")
                .Replace("}", "");
        }

        private void Send_Result_Button_Click(object sender, RoutedEventArgs e)
        {
            if(DataTextBlock.Text.Equals(""))
            {
                MessageBox.Show("Данные с сервера ещё не получены!",
                        "Ошибка проверки данных",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
            }
            else
            {
                CheckData(DataTextBlock.Text, innPattern);
            }
        }

        private void CheckData(string data, string pattern)
        {
            if(!Regex.IsMatch(data, pattern))
            {
                WordWriter.WriteToWord(
                    "C:\\Users\\admin\\source\\repos\\WpfAppModuleSix\\ТестКейс.docx",
                    1,
                    1,
                    "Не успешно",
                    "Не успешно",
                    data
                    );

                ResultTextBlock.Text = "Данные провали валидацию!";
                ResultTextBlock.Foreground = Brushes.Red;
            }
            else
            {
                WordWriter.WriteToWord(
                    "C:\\Users\\admin\\source\\repos\\WpfAppModuleSix\\ТестКейс.docx",
                    1,
                    1,
                    "Успешно",
                    "Успешно",
                    data
                    );

                ResultTextBlock.Text = "Данные успешно прошли валидацию!";
                ResultTextBlock.Foreground = Brushes.Green;
            }
        }
    }
}

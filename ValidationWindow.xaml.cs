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
        public ValidationWindow()
        {
            InitializeComponent();
        }

        private async void Get_Data_Button_Click(object sender, RoutedEventArgs e)
        {
            string url = "http://localhost:4444/TransferSimulator/fullName";
            string data = await ServerRequest.GetRequest(url);

            DataTextBlock.Text = GetDataFromJSON(data);
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
                CheckData(DataTextBlock.Text);
            }
        }

        private void CheckData(string data)
        {
            if(!Regex.IsMatch(data, fullNamePattern))
            {
                WordWriter.WriteToWord(
                    "C:\\Users\\admin\\source\\repos\\WpfAppModuleSix\\ТестКейс.docx",
                    1,
                    1,
                    data
                    );

                ResultTextBlock.Text = "Данные ФИО провали валидацию!";
                ResultTextBlock.Foreground = Brushes.Red;
            }
            else
            {
                WordWriter.WriteToWord(
                    "C:\\Users\\admin\\source\\repos\\WpfAppModuleSix\\ТестКейс.docx",
                    1,
                    1,
                    data
                    );

                ResultTextBlock.Text = "Данные ФИО успешно прошли валидацию!";
                ResultTextBlock.Foreground = Brushes.Green;
            }
        }
    }
}

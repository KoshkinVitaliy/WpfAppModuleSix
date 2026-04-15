using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Word = Microsoft.Office.Interop.Word;

namespace WpfAppModuleSix
{
    public class WordWriter
    {
        private static int rowIndex = 3;

        public static void WriteToWord(
            string filePath,
            int tableIndex, 
            int columnIndex,
            string expectedResult,
            string actualResult,
            string data
            )
        {
            Word.Application wordApp = new Word.Application();
            wordApp.Visible = false;

            try
            {
                Word.Document document = wordApp.Documents.Open(filePath);
                Word.Table table = document.Tables[tableIndex];

                if(columnIndex < 1 || columnIndex > table.Columns.Count)
                {
                    MessageBox.Show("Неправильный индекс колонки!",
                        "Ошибка записи в текстовый файл",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                else if (rowIndex <= table.Rows.Count) 
                {
                    table.Cell(rowIndex, columnIndex).Range.Text = data;
                    table.Cell(rowIndex, columnIndex + 1).Range.Text = expectedResult;
                    table.Cell(rowIndex, columnIndex + 2).Range.Text = actualResult;
                    rowIndex++;
                }
                else
                {
                    table.Rows.Add();
                    table.Cell(rowIndex, columnIndex).Range.Text = data;
                    table.Cell(rowIndex, columnIndex + 1).Range.Text = expectedResult;
                    table.Cell(rowIndex, columnIndex + 2).Range.Text = actualResult;
                    rowIndex++;
                }

                document.Save();
                document.Close();

                MessageBox.Show("Данные успешно записаны в ТестКейс.docx",
                        "Запись в текстовый файл",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);

            }
            catch(Exception ex)
            {
                MessageBox.Show($"{ex.Message}",
                        "Ошибка записи в текстовый файл",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
            }
            finally
            {
                wordApp.Quit();
            }
        }
    }
}

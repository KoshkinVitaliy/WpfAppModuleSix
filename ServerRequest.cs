using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace WpfAppModuleSix
{
    public class ServerRequest
    {
        public async static Task<string> GetRequest(string url)
        {
            
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage httpResponse = await client.GetAsync(url);
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        return await httpResponse.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        return $"Ошибка: {httpResponse.StatusCode}";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не удалось подключиться к серверу! {ex.Message}",
                        "Ошибка подключения",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);

                    return null;
                }
                
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace WpfAppModuleSix
{
    public class ServerRequest
    {
        public async static Task<string> GetRequest(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponse = await client.GetAsync(url);
                if(httpResponse.IsSuccessStatusCode)
                {
                    return await httpResponse.Content.ReadAsStringAsync();
                }
                else
                {
                    return $"Ошибка: {httpResponse.StatusCode}";
                }
            }
        }
    }
}

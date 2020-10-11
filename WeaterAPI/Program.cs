using RestSharp;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WeaterAPI
{
    class Program
    {
        public static string CutStr(string str,string paramName, int indent,char lastChar=',')
        {
            int found = str.IndexOf(paramName);

            string cur = str.Substring(found + indent);
            string final = str.Substring(found + indent, cur.IndexOf(lastChar));
            return final;
        }       

        public static string Translate(string strP)
        {           
            string str = strP.Replace(" ", "%20");
            var client = new RestClient("https://google-translate20.p.rapidapi.com/translate?sl=en&text="+str+"&tl=ru");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "google-translate20.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "57d8a05eb6msh3dd8d2e89034fd9p116572jsne1ddd31df2de");
            IRestResponse response = client.Execute(request);
            
            string final = CutStr(response.Content, "translation", 14, '"');
            return final;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Введите город в котором хотите узнать погоду");
            string city = Console.ReadLine().ToString();

           string URL = "https://api.weatherbit.io/v2.0/current";
            string urlParameters = $"?city={city}&key=5f96dc6e7d4e4cfab1f0f6a4a89850a4";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                string str = response.Content.ReadAsStringAsync().Result;
                if (str == string.Empty)
                {
                    Console.WriteLine("Введён некорректный город");
                    Console.ReadKey();
                    return;
                }
                // Parse the response body.
                string pogoda = CutStr(str, "description", 14, '"');

                Console.WriteLine($"Температура: "+ CutStr(str,"temp",6)+ "°C");                
                Console.WriteLine($"Погода: " + Translate(pogoda));                  
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            Console.ReadKey();
        }
    }
}

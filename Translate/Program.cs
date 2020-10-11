using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translate
{
    class Program
    {
        public static string CutStr(string str, string paramName, int indent, char lastChar = ',')
        {
            int found = str.IndexOf(paramName);

            string cur = str.Substring(found + indent);
            string final = str.Substring(found + indent, cur.IndexOf(lastChar));
            return final;
        }

        static void Main(string[] args)
        {
            // %20
            var client = new RestClient("https://google-translate20.p.rapidapi.com/translate?sl=en&text=Overcastclouds&tl=ru");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "google-translate20.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "57d8a05eb6msh3dd8d2e89034fd9p116572jsne1ddd31df2de");
            IRestResponse response = client.Execute(request);

            Console.WriteLine(response.Content);
            CutStr(response.Content, "translation",14,'"');
            Console.ReadKey();
        }
    }
}

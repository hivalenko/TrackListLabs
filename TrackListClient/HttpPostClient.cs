using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace TrackListClient
{
    public class HttpPostClient
    {
        public static async Task<string> Exec(String dest, String xml)
        {
            String res = "";
            
            Uri uri =  new Uri(Program.Ipport);
            
            //String encodedContent = Uri.EscapeDataString(xml);
            HttpClient client = new HttpClient
            {
                BaseAddress = uri
            };
            HttpResponseMessage result = await client.PostAsync(dest, new StringContent(xml));
            res = await result.Content.ReadAsStringAsync();
            
            XElement root = XElement.Load(new MemoryStream(Encoding.ASCII.GetBytes(res)));

            foreach (XElement lineElement in root.Elements())
            {
                string line = lineElement.Value;
                Console.WriteLine(line);
            }

            return res;
        }

        public static string ConstructXml(string[] lines)
        {
            StringBuilder stringBuilder = new StringBuilder("<root>");
            foreach (var line in lines)
            {
                stringBuilder.Append("<line>")
                    .Append(line)
                    .Append("</line>");
            }

            stringBuilder.Append("</root>");
            return stringBuilder.ToString();
           
        }
    }
}
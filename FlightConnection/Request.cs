using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightConnection
{
    public enum RequestAction { GetAvailableDirections, GetDirectionsByEndpoints };
    public class Request
    {
        public string CodeDeparture { get; set; }
        public string CodeArrival { get; set; }
        public RequestAction ClientRequest { get; set; }

        public Request() 
        {
            CodeDeparture = "";
            CodeArrival = "";
            ClientRequest = RequestAction.GetAvailableDirections;
        }

        public Request(string codeDeparture)
        {
            //for one departure code, there is action get available directions from this departure code
            this.CodeDeparture = codeDeparture;
            this.ClientRequest = RequestAction.GetAvailableDirections;
        }

        public Request(string codeDeparture, string codeArrival)
        {
            //for two endoints there is command for getting all variants with transfers by two enpoints
            this.CodeDeparture = codeDeparture;
            this.CodeArrival = codeArrival;
            this.ClientRequest = RequestAction.GetDirectionsByEndpoints;
        }

        public static byte[] SerializeRequest(Request req)
        {
            string data = JsonConvert.SerializeObject(req);
            return System.Text.Encoding.UTF8.GetBytes(data);  
        }

        public static Request DeserializeRequest(byte[] buffer, int bytes)
        {
            string data = System.Text.Encoding.UTF8.GetString(buffer, 0, bytes);
            return JsonConvert.DeserializeObject<Request>(data);
        }
    }
}

using FlightConnection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientWinForms
{
    public class FlightController
    {
        string ipAddress;
        int port;

        public FlightController()
        {
            ipAddress = "127.0.0.1";
            port = 22222;
        }

        private string SendRequest(byte[] buffer) 
        {
            
            TcpClient client = new TcpClient(ipAddress, port);
            NetworkStream stream = client.GetStream(); 
            stream.Write(buffer, 0, buffer.Length);
            //read from stream reply
            byte[] bufferReply = new byte[100000];
            int bytes = stream.Read(bufferReply, 0, bufferReply.Length);
            return System.Text.Encoding.UTF8.GetString(bufferReply, 0, bytes);
        }

        public List<Direction> GetAvailableDirections(string codeFrom) 
        {
            string reply = SendRequest(Request.SerializeRequest(new Request(codeFrom)));
            return JsonConvert.DeserializeObject<List<Direction>>(reply);
        }

        public List<Direction> GetDirectionsWithTransfers(string codeFrom, string codeTo)
        {
            string reply = SendRequest(Request.SerializeRequest(new Request(codeFrom, codeTo)));
            return JsonConvert.DeserializeObject<List<Direction>>(reply);
        }
    }
}

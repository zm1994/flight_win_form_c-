using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerController server = new ServerController();
            server.StartServer();
        }

        public static void StartServer(IMongoCollection<BsonDocument> collection)
        {
            try
            {
                TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 22222);
                listener.Start();

                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    NetworkStream stream = client.GetStream();
                    StreamWriter sw = new StreamWriter(stream);
                    
                    var filter = Builders<BsonDocument>.Filter.Eq("code_airport", "gka");
                    var document = collection.Find(filter)
                                                .Project(Builders<BsonDocument>.Projection
                                                .Exclude("_id"))
                                                .FirstOrDefault();
                    sw.Write(document.ToString());
                    sw.Flush();
                    client.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

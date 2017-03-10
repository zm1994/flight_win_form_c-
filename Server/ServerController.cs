using FlightConnection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
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
    public class ServerController
    {
        string ipAddress;
        int port;
        MongoClient client;
        IMongoDatabase database;
        IMongoCollection<BsonDocument> collectionAirports;
        IMongoCollection<BsonDocument> collectionDirections;
        IMongoCollection<BsonDocument> collectionAvailableDirections;

        public ServerController()
        {
            ipAddress = "127.0.0.1";
            port = 22222;
            client = new MongoClient("mongodb://localhost:27017");
            database = client.GetDatabase("flight_connection");
            collectionAirports = database.GetCollection<BsonDocument>("airports");
            collectionAvailableDirections = database.GetCollection<BsonDocument>("available_directions");
            collectionDirections = database.GetCollection<BsonDocument>("directions_with_transfers");
        }

        public void StartServer()
        {
            TcpListener listener = new TcpListener(IPAddress.Parse(ipAddress), port);
            listener.Start();
            while (true)
            {
                try
                {
                    TcpClient client = listener.AcceptTcpClient();
                    AnaliseRequest(client);
                    client.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void AnaliseRequest(TcpClient client) 
        {
            NetworkStream stream = client.GetStream();
            //read from stream request
            byte[] data = new byte[1024];
            int bytes = stream.Read(data, 0, data.Length);
            Request req = Request.DeserializeRequest(data, bytes);
            string reply = "";
            //parse command in request
            if (req.ClientRequest == RequestAction.GetAvailableDirections)
               reply = JsonConvert.SerializeObject(GetAvailableDirections(req.CodeDeparture));
            else if(req.ClientRequest == RequestAction.GetDirectionsByEndpoints)
                reply = JsonConvert.SerializeObject(GetDirectionsByEndpoints(req.CodeDeparture, req.CodeArrival));
            SendReply(stream, reply);
        }

        private List<Direction> GetDirectionsByEndpoints(string codeDeparture, string codeArrival)
        {
            //first get departure airport
            Airport departureAirport = FindAirport(codeDeparture);
            //first get arrival airport
            Airport arrivalAirport = FindAirport(codeArrival);
            //find direction with transfers (ids airports directions)
            List<DirectionWithTransfers> directionIdsWithTransfers = FindDirectionsWithTransfers(departureAirport.id_airport, arrivalAirport.id_airport);
            //get ids transfers from all direction variants
            int[] arrIdsTransfers = directionIdsWithTransfers.SelectMany(direct => direct.transfers_id).ToArray();
            //find airport transfers on all directions
            List<Airport> transfers = FindListAirport(arrIdsTransfers);
            List<Direction> directionsWithTransfers = new List<Direction>();
            foreach(var directionId in directionIdsWithTransfers)
            {
                List<Airport> transfersInCurrentDirection = new List<Airport>();
                foreach (var transferId in directionId.transfers_id)
                    transfersInCurrentDirection.Add(transfers.FirstOrDefault(airport => airport.id_airport == transferId));
                List<Airport> airportsInCurrentDirection = new List<Airport>();
                //add departure transfers and arrival airport
                airportsInCurrentDirection.Add(departureAirport);
                airportsInCurrentDirection.AddRange(transfersInCurrentDirection);
                airportsInCurrentDirection.Add(arrivalAirport);
                directionsWithTransfers.Add(new Direction(airportsInCurrentDirection));
            }

            return directionsWithTransfers;
        }

        private List<DirectionWithTransfers> FindDirectionsWithTransfers(int idDeparture, int idArrival)
        {
            //get ids airports departure and arrival from database 
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("departure_id", idDeparture) & builder.Eq("arrival_id", idArrival);
            var directions = collectionDirections.Find(filter)
                                                 .Project(Builders<BsonDocument>.Projection
                                                 .Exclude("_id"))
                                                 .ToList();

            List<DirectionWithTransfers> directionsWithTransfers = new List<DirectionWithTransfers>();
            foreach (var iter in directions)
                directionsWithTransfers.Add(BsonSerializer.Deserialize<DirectionWithTransfers>(iter));
            return directionsWithTransfers;
        }

        private void SendReply(NetworkStream stream, string reply) 
        {
            byte[] bufferReply = System.Text.Encoding.UTF8.GetBytes(reply);
            stream.Write(bufferReply, 0, bufferReply.Length);
        }

        private Airport FindAirport(string codeDeparture) 
        {
            //get object departure from database
            var filter = Builders<BsonDocument>.Filter.Eq("code_airport", codeDeparture);
            var airport = collectionAirports.Find(filter)
                                            .Project(Builders<BsonDocument>.Projection
                                            .Exclude("_id"))
                                            .FirstOrDefault();
            if (airport != null)
                return BsonSerializer.Deserialize<Airport>(airport);
            else
                return new Airport();
        }

        private List<Airport> FindListAirport(int[] idsAirports)
        {
            //get object departure from database
            var filter = Builders<BsonDocument>.Filter.In("id_airport", idsAirports);
            var document = collectionAirports.Find(filter)
                                             .Project(Builders<BsonDocument>.Projection
                                             .Exclude("_id"))
                                             .ToList();
            List<Airport> airports = new List<Airport>();
            foreach (var iter in document)
                airports.Add(BsonSerializer.Deserialize<Airport>(iter));
            return airports;
        }

        private List<AvailableDirection> FindAvailableDirections(int idDepartureAirport) 
        {
            //get ids airports departure and arrival from database 
            var filter = Builders<BsonDocument>.Filter.Eq("departure_id", idDepartureAirport);
            var directions = collectionAvailableDirections.Find(filter)
                                                          .Project(Builders<BsonDocument>.Projection
                                                          .Exclude("_id"))
                                                          .ToList();
            
            List<AvailableDirection> availDirections = new List<AvailableDirection>();
            foreach (var iter in directions)
                availDirections.Add(BsonSerializer.Deserialize<AvailableDirection>(iter));
            return availDirections;
        }

        private List<Direction> GetAvailableDirections(string codeDeparture)
        {
            //first get departure airport
            Airport departureAirport = FindAirport(codeDeparture);
            //get ids airports for arrival
            List<AvailableDirection> availDirections = FindAvailableDirections(departureAirport.id_airport);
            //get ids arrival airports for selecting full info for this airports
            int[] arrayArrivalAirports = availDirections.Select(p => p.arrival_id).ToArray();
            List<Airport> arrivalAirports = FindListAirport(arrayArrivalAirports);
            List<Direction> directions = new List<Direction>();
            foreach (var itemArrival in arrivalAirports)
                directions.Add(new Direction(new List<Airport>() { departureAirport, itemArrival }));

            return directions;
        }
    }
}

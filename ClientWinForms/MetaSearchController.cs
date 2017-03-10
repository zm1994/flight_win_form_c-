using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWinForms
{
    public abstract class MetaSearchController
    {
        protected string departureCode;
        protected string arrivalCode;
        protected uint adults;
        protected uint children;
        protected uint infants;
        protected string source;
        protected string request;
        protected string formatArrivalDate;
        protected string formatDepartureDate;
        
        public MetaSearchController() { }
        
        public MetaSearchController(string codeDeparture, string codeArrival, 
            uint adults, uint children, uint infants, string dateFrom, string dateTo = "") 
        {
            this.departureCode = codeDeparture;
            this.arrivalCode = codeArrival;
            this.adults = adults;
            this.children = children;
            this.infants = infants;
            this.formatDepartureDate = dateFrom;
            this.formatArrivalDate = dateTo;
        }

        protected string ConvertDateTimeFormat(string val) 
        {
            //remove year, and dot
            if (val.Length > 0)
                return val.Remove(val.LastIndexOf('.') + 1).Replace(".", "");
            else
                return "";
        }

        abstract protected void GenerateRequest();
        
        public void Search() 
        {
            GenerateRequest();
            Process.Start(request);
        }
    }

    public class AviaSalesMetaSearchController: MetaSearchController
    {
        public AviaSalesMetaSearchController()
        {
            this.source = "https://search.aviasales.ru/";
        }

        public AviaSalesMetaSearchController(string codeDeparture, string codeArrival, 
            uint adults, uint children, uint infants, string dateFrom, string dateTo = ""):
                base(codeDeparture, codeArrival, adults, children, infants, dateFrom, dateTo)
        {
            this.source = "https://search.aviasales.ru/";
        }

        protected override void GenerateRequest() 
        {
            this.request = this.source + departureCode.ToUpper() + ConvertDateTimeFormat(formatDepartureDate) + 
                arrivalCode.ToUpper() + ConvertDateTimeFormat(formatArrivalDate) + 
                adults.ToString() + children.ToString() + infants.ToString();
        }
    }

    public class TripMyDreamMetaSearchController : MetaSearchController
    {
        protected string typeFlightClass;
        public TripMyDreamMetaSearchController()
        {
            this.source = "https://avia.tripmydream.com/ru/flights/search/";
        }

        public TripMyDreamMetaSearchController(string codeDeparture, string codeArrival,
            uint adults, uint children, uint infants, string dateFrom, string dateTo = "", string flightClass = "E") :
            base(codeDeparture, codeArrival, adults, children, infants, dateFrom, dateTo)
        {
            this.source = "https://avia.tripmydream.com/ru/flights/search/";
            this.typeFlightClass = flightClass;
        }

        protected override void GenerateRequest()
        {
            this.request = this.source + departureCode.ToUpper() + arrivalCode.ToUpper() +
                ConvertDateTimeFormat(formatDepartureDate) + ConvertDateTimeFormat(formatArrivalDate) + typeFlightClass +
                adults.ToString() + children.ToString() + infants.ToString();
        }
    }
}

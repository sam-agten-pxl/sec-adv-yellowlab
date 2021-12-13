using CsvHelper.Configuration.Attributes;

namespace api
{
    public class Seatholder 
    {
        [Name("Postcode")]
        public string AreaCode {get;set;}

        [Name("Buyertype")]
        public string BuyerType {get;set;}

        [Name("City")]
        public string City {get;set;}

        [Name("Section")]
        public string Section {get;set;}
    }
}
using System;

namespace Codetest{

    public enum Gender
    {
    Male,
    Female
    }

    public class ClientModel
    {
        public Guid Id {get; set;}
        public string Gender {get;set;}
        public string FirstName {get;set;}
        public string LastName {get;set;}
        public string Email {get;set;}
        public string Company {get;set;}
        public string Address {get;set;}
        public string Address2  {get;set;}
        public string City {get;set;}
        public string PostalCode {get;set;}
        public string Information {get;set;}
        public string HomePhone{get;set;}
        public string MobilePhone {get;set;}

    }

}

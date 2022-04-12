using ARRMM.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARRMM.Models
{
    public class PersonModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Address { get; set; }
        public string NicNumber { get; set; }
        public DateTime? NicIssueDate { get; set; }
        public string NicIssuePlace { get; set; }
        public bool IsLocalDomicile { get; set; }
        public string NameOfTribe { get; set; }
        public string NameOfDistrict { get; set; }
        public string Occupation { get; set; }
        public string PlaceOfBusiness { get; set; }
        public float? SharesPercentage { get; set; }
        public PersonType Type { get; set; }
        public int? CountryId { get; set; }
        public ApplicationModel Application { get; set; }
        public CompanyModel Company { get; set; }
        public CountryModel Country { get; set; }
    }
}

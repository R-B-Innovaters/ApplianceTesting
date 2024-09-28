namespace ApplianceTesting.Models
{
    public class LocationViewModel
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public bool LocationStatus { get; set; }
    }

    public class CityViewModel
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public bool CityStatus { get; set; }
    }

}


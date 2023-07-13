namespace FARMCENTRALPROTOTYPE.Models
{
    public class Farmer
    {
        public string FarmerID { get; set; }
        public string FarmerName { get; set; }
        public string FarmerSurname { get; set; }
        public string FarmerEmail { get; set; }
        public string FarmerPassword { get; set; }

        public Farmer()
        {
            
        }

        public Farmer(string farmerID, string farmerName, string farmerSurname, string farmerEmail, string farmerPassword)
        {
            FarmerID = farmerID;
            FarmerName = farmerName;
            FarmerSurname = farmerSurname;
            FarmerEmail = farmerEmail;
            FarmerPassword = farmerPassword;
        }
    }
}

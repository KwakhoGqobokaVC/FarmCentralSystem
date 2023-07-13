namespace FARMCENTRALPROTOTYPE.Models
{
    public class Product
    {
        public string ProductID { get; set; }
        public string ProductType { get; set; }
        public string ProductDate { get; set; }
        public string ProductQuantity { get; set; }
        public string ProductPrice { get; set; }
        public string FarmerID { get; set; }

        public Product()
        {
            
        }

        public Product(string productID, string productType, string productDate, string productQuantity, string productPrice, string farmerID)
        {
            ProductID = productID;
            ProductType = productType;
            ProductDate = productDate;
            ProductQuantity = productQuantity;
            ProductPrice = productPrice;
            FarmerID = farmerID;
        }
    }
}

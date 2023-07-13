using FARMCENTRALPROTOTYPE.Models;
using System.Data;
using System.Data.SqlClient;

namespace FARMCENTRALPROTOTYPE.Data
{
    public class DBFarmCentral
    {
        private string conString;
        private IConfiguration _config;



        public DBFarmCentral(IConfiguration configuration)
        {
            _config = configuration;
            conString = _config.GetConnectionString("localDBConnect");
        }
        public List<Farmer> AllFarmers()
        {
            List<Farmer> stList = new List<Farmer>();
            SqlConnection myConnection = new SqlConnection(conString);
            SqlDataAdapter myDataAdapter = new SqlDataAdapter("SELECT * FROM FARMERS", myConnection);
            DataTable myTable = new DataTable();
            DataRow myRow;

            myConnection.Open();
            myDataAdapter.Fill(myTable);
            string id, name, surname, email, password;
            

            if (myTable.Rows.Count > 0)
            {
                for (int i = 0; i < myTable.Rows.Count; i++)
                {
                    myRow = myTable.Rows[i];

                    stList.Add(new Farmer((string)myRow[0], (string)myRow[1], (string)myRow[2], (string)myRow[3], (string)myRow[4]));


                }
            }

            myConnection.Close();
            return stList;
        }

        public List<Product> AllProducts()
        {
            List<Product> stPList = new List<Product>();
            SqlConnection myConnection = new SqlConnection(conString);
            SqlDataAdapter myDataAdapter = new SqlDataAdapter("SELECT * FROM PRODUCTS", myConnection);
            DataTable myTable = new DataTable();
            DataRow myRow;

            myConnection.Open();
            myDataAdapter.Fill(myTable);
            string Pid, Ptype, Pdate, Pquantity, Pprice, Pfarmerid;
            


            if (myTable.Rows.Count > 0)
            {
                for (int i = 0; i < myTable.Rows.Count; i++)
                {
                    myRow = myTable.Rows[i];

                    stPList.Add(new Product((string)myRow[0], (string)myRow[1], (string)myRow[2], (string)myRow[3], (string)myRow[4], (string)myRow[5]));


                }
            }

            myConnection.Close();
            return stPList;
        }
        public List<Product> AllFarmersProducts(string id)
        {
            List<Product> stFPList = new List<Product>();
            SqlConnection myConnection = new SqlConnection(conString);
            SqlDataAdapter myDataAdapter = new SqlDataAdapter($"SELECT * FROM PRODUCTS where FARMER_ID = '{id}' ", myConnection);
            DataTable myTable = new DataTable();
            DataRow myRow;

            myConnection.Open();
            myDataAdapter.Fill(myTable);
            string Pid, Ptype, Pdate, Pquantity, Pprice, Pfarmerid;



            if (myTable.Rows.Count > 0)
            {
                for (int i = 0; i < myTable.Rows.Count; i++)
                {
                    myRow = myTable.Rows[i];

                    stFPList.Add(new Product((string)myRow[0], (string)myRow[1], (string)myRow[2], (string)myRow[3], (string)myRow[4], (string)myRow[5]));


                }
            }

            myConnection.Close();
            return stFPList;
        }
        public Farmer GetFarmer(string id)
        {
            Farmer fr = new Farmer();
            SqlConnection myConnection = new SqlConnection(conString);
            SqlCommand cmdSelect = new SqlCommand($"SELECT * FROM FARMERS WHERE FARMER_ID = '{id}'", myConnection);
            myConnection.Open();
            using (SqlDataReader reader = cmdSelect.ExecuteReader())
            {
                while (reader.Read())
                {
                    fr = new Farmer((string)reader[0], (string)reader[1], (string)reader[2], (string)reader[3], (string)reader[4]);
                }
            }
            myConnection.Close();
            return fr;

        }

        public string GetFarmerID(string email)
        {
            string id = "";
            SqlConnection myConnection = new SqlConnection(conString);
            SqlCommand cmdSelect = new SqlCommand($"SELECT FARMER_ID FROM FARMERS WHERE FARMER_EMAIL = '{email}'", myConnection);
            myConnection.Open();
            using (SqlDataReader reader = cmdSelect.ExecuteReader())
            {
                while (reader.Read())
                {
                    id = reader.GetString(0);
                }
            }
            myConnection.Close();
            return id;
        }


        public Product GetFarmerProduct(string id)
        {
            Product pr = new Product();
            SqlConnection myConnection = new SqlConnection(conString);
            SqlCommand cmdSelect = new SqlCommand($"SELECT * FROM PRODUCTS WHERE FARMER_ID = '{id}'", myConnection);
            myConnection.Open();
            using (SqlDataReader reader = cmdSelect.ExecuteReader())
            {
                while (reader.Read())
                {
                    pr = new Product((string)reader[0], (string)reader[1], (string)reader[2], (string)reader[3], (string)reader[4], (string)reader[5]);
                }
            }
            myConnection.Close();
            return pr;

        }

        public Product GetProduct(string id)
        {
            Product pr = new Product();
            SqlConnection myConnection = new SqlConnection(conString);
            SqlCommand cmdSelect = new SqlCommand($"SELECT * FROM PRODUCTS WHERE PRODUCT_ID = '{id}'", myConnection);
            myConnection.Open();
            using (SqlDataReader reader = cmdSelect.ExecuteReader())
            {
                while (reader.Read())
                {
                    pr = new Product((string)reader[0], (string)reader[1], (string)reader[2], (string)reader[3], (string)reader[4], (string)reader[5]);
                }
            }
            myConnection.Close();
            return pr;

        }

        public void AddFarmer(Farmer fr)
        {
            using (SqlConnection myConnection = new SqlConnection(conString))
            {
                SqlCommand cmdInsert = new SqlCommand($"INSERT INTO FARMERS VALUES('{fr.FarmerID}','{fr.FarmerName}','{fr.FarmerSurname}','{fr.FarmerEmail}','{fr.FarmerPassword}')", myConnection);
                myConnection.Open();
                cmdInsert.ExecuteNonQuery();
            }
        }

        public void AddProduct(Product Pr)
        {
            using (SqlConnection myConnection = new SqlConnection(conString))
            {
                SqlCommand cmdInsert = new SqlCommand($"INSERT INTO PRODUCTS VALUES('{Pr.ProductID}','{Pr.ProductType}','{Pr.ProductDate}','{Pr.ProductQuantity}','{Pr.ProductPrice}','{Pr.FarmerID}')", myConnection);
                myConnection.Open();
                cmdInsert.ExecuteNonQuery();
            }
        }

        public void UpdateFarmer(string id, Farmer fr)
        {
            using (SqlConnection myCon = new SqlConnection(conString))
            {
                SqlCommand cmdUpdate = new SqlCommand($"UPDATE FARMERS SET FARMER_ID='{fr.FarmerID}',FARMER_NAME='{fr.FarmerName}',FARMER_SURNAME='{fr.FarmerSurname}',FARMER_EMAIL='{fr.FarmerEmail}',FARMER_PASSWORD='{fr.FarmerPassword}' WHERE FARMER_ID = '{id}'", myCon);

                myCon.Open();
                cmdUpdate.ExecuteNonQuery();
            }
        }
        public void UpdateProduct(string id, Product pr)
        {
            using (SqlConnection myCon = new SqlConnection(conString))
            {
                SqlCommand cmdUpdate = new SqlCommand($"UPDATE PRODUCTS SET PRODUCT_TYPE ='{pr.ProductType}',PRODUCT_DATE='{pr.ProductDate}',PRODUCT_QUANTITY='{pr.ProductQuantity}',PRODUCT_PRICE='{pr.ProductPrice}',FARMER_ID='{pr.FarmerID}' WHERE PRODUCT_ID = '{id}'", myCon);

                myCon.Open();
                cmdUpdate.ExecuteNonQuery();
            }
        }

        public void UpdateFarmerProduct(string id, Product pr)
        {
            using (SqlConnection myCon = new SqlConnection(conString))
            {
                SqlCommand cmdUpdate = new SqlCommand($"UPDATE PRODUCTS SET PRODUCT_ID ='{pr.ProductID}',PRODUCT_TYPE ='{pr.ProductType}',PRODUCT_DATE='{pr.ProductDate}',PRODUCT_QUANTITY='{pr.ProductQuantity}',PRODUCT_PRICE='{pr.ProductPrice}',FARMER_ID='{pr.FarmerID}' WHERE PRODUCT_ID = '{id}'", myCon);

                myCon.Open();
                cmdUpdate.ExecuteNonQuery();
            }
        }





        public void DeleteFarmer(string id)
        {
            using (SqlConnection myConn = new SqlConnection(conString))
            {
                SqlCommand cmdDelete = new SqlCommand($"DELETE FROM FARMERS WHERE FARMER_ID = '{id}'", myConn);
                myConn.Open();
                cmdDelete.ExecuteNonQuery();

            }
        }
        public void DeleteProducts(string id)
        {
            using (SqlConnection myConn = new SqlConnection(conString))
            {
                SqlCommand cmdDelete = new SqlCommand($"DELETE FROM PRODUCTS WHERE PRODUCT_ID = '{id}'", myConn);
                myConn.Open();
                cmdDelete.ExecuteNonQuery();

            }
        }
    }
}

using FARMCENTRALPROTOTYPE.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FARMCENTRALPROTOTYPE.Models;

namespace FARMCENTRALPROTOTYPE.Controllers
{
    public class ProductController : Controller
    {
        DBFarmCentral dbFC;
        public ProductController(IConfiguration config)
        {
            dbFC = new DBFarmCentral(config);
        }
        // GET: ProductController
        public ActionResult Index()
        {
            string farmerID=HttpContext.Session.GetString("FARMER_ID");
            List<Product> stFPlist = dbFC.AllFarmersProducts(farmerID);
            
            return View(stFPlist);
        }
        


        // GET: ProductController/Details/5
        public ActionResult Details(string id)
        {
            Product product = dbFC.GetProduct(id);
            return View(product);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                string ProductID = collection["txtProductID"];
                string ProductType = collection["txtProductType"];
                string ProductDate = collection["txtProductDate"];
                string ProductQuantity = collection["txtProductQuantity"];
                string ProductPrice = collection["txtProductPrice"];
                string FarmerID = collection["txtFarmerID"];


                Product product = new Product(ProductID, ProductType, ProductDate, ProductQuantity, ProductPrice, FarmerID);
                dbFC.AddProduct(product);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(string id)
        {
            Product pr = dbFC.GetProduct(id);
            return View(pr);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, IFormCollection collection)
        {
            try
            {
                string productid = collection["txtProductID"];
                string type = collection["txtProductType"];
                string date = collection["txtProductDate"];
                string quantity = collection["txtProductQuantity"];
                string price = collection["txtProductPrice"];
                string farmerid = collection["txtFarmerID"];
                

                Product pr = new Product(productid, type, date, quantity, price, farmerid);

                dbFC.UpdateFarmerProduct(id, pr);

                return RedirectToAction(nameof(Index));
               

            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(string id)
        {
            Product pr = dbFC.GetProduct(id);
            return View(pr);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                dbFC.DeleteProducts(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

using FARMCENTRALPROTOTYPE.Data;
using FARMCENTRALPROTOTYPE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FARMCENTRALPROTOTYPE.Controllers
{
    public class FarmerController : Controller
    {
        DBFarmCentral dbFC;

        public FarmerController(IConfiguration config)
        {
            dbFC = new DBFarmCentral(config);

        }
        // GET: FarmerController
        public ActionResult Index()
        {
            List<Farmer> stlist = dbFC.AllFarmers();
            return View(stlist);
        }
        public ActionResult Index2(string productType, string productDate)
        {
            List<Product> stlist = dbFC.AllProducts();
            if (!string.IsNullOrEmpty(productType))
            {
                stlist = stlist.Where(p => p.ProductType.Contains(productType)).ToList();
                ViewBag.FilteredProductType = productType; // Store the filtered value to maintain it in the view
            }

            if (!string.IsNullOrEmpty(productDate))
            {
                stlist = stlist.Where(p => p.ProductDate.Contains(productDate)).ToList();
                ViewBag.FilteredProductDate = productDate; // Store the filtered value to maintain it in the view
            }
            return View(stlist);
        }

        // GET: FarmerController/Details/5
        public ActionResult Details(string id)
        {
            Farmer farmer = dbFC.GetFarmer(id);
            return View(farmer);
        }

        // GET: FarmerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FarmerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                string FarmerID = collection["txtFarmerID"];
                string FarmerName = collection["txtFarmerName"];
                string FarmerSurname = collection["txtFarmerSurname"];
                string FarmerEmail = collection["txtFarmerEmail"];
                string FarmerPassword = collection["txtFarmerPassword"];

                Farmer farmer = new Farmer(FarmerID, FarmerName, FarmerSurname, FarmerEmail, FarmerPassword);
                dbFC.AddFarmer(farmer);
                return RedirectToAction(nameof(Index));
               
            }
            catch
            {
                return View();
            }
        }

        // GET: FarmerController/Edit/5
        public ActionResult Edit(string id)
        {
            Farmer farmer = dbFC.GetFarmer(id);
            return View(farmer); ;
        }

        // POST: FarmerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, IFormCollection collection)
        {
            try
            {
                string name = collection["txtFarmerName"];
                string surname = collection["txtFarmerSurname"];
                string email = collection["txtFarmerEmail"];
                string password = collection["txtFarmerPassword"];

                Farmer fr = new Farmer(id, name, surname, email, password);

                dbFC.UpdateFarmer(id, fr);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FarmerController/Delete/5
        public ActionResult Delete(string id)
        {
            Farmer fr = dbFC.GetFarmer(id);
            return View(fr);
        }

        // POST: FarmerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                dbFC.DeleteFarmer(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
       

        public IActionResult Action()
        {

            return View("Index2");
        }
    }
}

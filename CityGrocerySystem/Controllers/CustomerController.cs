using CGS.DAL;
using CGS.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityGrocerySystem.Controllers
{
    public class CustomerController : Controller
    {
        private MasterDataDetails dataDetails = new MasterDataDetails();
        public IActionResult Index()
        {
            TilesData tilesData = new TilesData();
            tilesData = dataDetails.GetTilesData();
            return View(tilesData);
        }
        public IActionResult CategoryMaster()
        {
            List<CategoryMaster> categoryMaster = new List<CategoryMaster>();
            categoryMaster = dataDetails.GetcategoryList();
            return PartialView(categoryMaster);
        }
        public IActionResult GetProductData(int category = 0)
        {
            List<ProductDetail> products = new List<ProductDetail>();
            products = dataDetails.GetProductDetails(category);
            return View("ProductDetails", products);
        }
    }
}

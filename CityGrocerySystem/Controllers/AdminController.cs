using Microsoft.AspNetCore.Mvc;
using CGS.Model;
using CGS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityGrocerySystem.Controllers
{
    public class AdminController : Controller
    {
        private MasterDataDetails dataDetails = new MasterDataDetails();
        public IActionResult Index()
        {
            TilesData tilesData = new TilesData();
            tilesData = dataDetails.GetTilesData();
            return PartialView("Home",tilesData);
        }

        public IActionResult CategoryMaster()
        {
            List<CategoryMaster> categoryMaster = new List<CategoryMaster>();
            categoryMaster = dataDetails.GetcategoryList();
            return PartialView(categoryMaster);
        }

        public JsonResult SaveCategory(string category)
        {
            string result = dataDetails.SaveNewcategory(category);

            return Json(result);
        }

        public IActionResult GetUserData()
        {
            List<UserDetails> users = new List<UserDetails>();
            users = dataDetails.GetUserdata();
            return View("UserDetails",users);
        }
        public IActionResult GetProductData()
        {
            List<ProductDetail> products = new List<ProductDetail>();
            products = dataDetails.GetProductDetails();
            return View("ProductDetails",products);
        }

        [HttpPost]
        public JsonResult SaveProduct([FromBody] ProductDetail productdetail)
        {
            string result = dataDetails.SaveProduct(productdetail);

                return Json(result);
        }

        [HttpPost]
        public JsonResult EditUpdateProduct([FromBody] ProductDetail productdetail)
        {
            string result = dataDetails.EditUpdateProduct(productdetail);

            return Json(result);
        }
    }
}

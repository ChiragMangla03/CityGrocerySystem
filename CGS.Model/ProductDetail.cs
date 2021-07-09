using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGS.Model
{
    public class ProductDetail
    {
        public int product_id { get; set; }
        public string Product_name { get; set; }
        public string price { get; set; }

        public string CategoryName { get; set; }

        public int categoryId { get; set; }
        public int total_stock { get; set; }
        public int InStock { get; set; }

        public List<SelectList> CategoryList { get; set; }

        public string ImagePath { get; set; }
    }
}

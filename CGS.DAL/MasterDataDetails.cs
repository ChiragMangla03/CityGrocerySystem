using CGS.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace CGS.DAL
{
    public class MasterDataDetails
    {
        private static string ConnString = "Integrated Security=SSPI;Persist Security Info=False;Data Source=CHIRAG-P;database=CITYGroceryStore";
        public string RegisterNewUser(RegistrationDetails Userdetails)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(ConnString))
                {
                    conn.Execute("sp_StoreRegistrationDetails", new
                    {
                        user_name = Userdetails.user_name,
                        first_name = Userdetails.first_name,
                        last_name = Userdetails.last_name,
                        phone_no = Userdetails.phone_no,
                        alternate_phone_no = Userdetails.alternate_phone_no,
                        email_id = Userdetails.email_id,
                        address = Userdetails.address,
                        password = Userdetails.password

                    }, commandType: CommandType.StoredProcedure);
                }
                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public TilesData GetTilesData()
        {
            TilesData tiles = new TilesData();
            try
            {
                using (IDbConnection conn = new SqlConnection(ConnString))
                {
                    string qry = @"SELECT
	                                    (SELECT ISNULL(SUM(1),0) FROM tbl_user_detail ) AS total_users,
	                                    (SELECT ISNULL(SUM(1),0) FROM tblProducts) AS total_products ,
	                                    (SELECT ISNULL(SUM(1),0) FROM tbl_cartDetail) AS total_carts ,
	                                    (SELECT ISNULL(SUM(unit),0) FROM tbl_order_detail) AS total_orders ,
	                                    (SELECT ISNULL(SUM(1),0) FROM tbl_ProductCategory) AS total_categories; ";
                    tiles = conn.Query<TilesData>(qry, CommandType.Text).FirstOrDefault();
                }
                return tiles;
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        public List<CategoryMaster> GetcategoryList()
        {
            List<CategoryMaster> categorories = new List<CategoryMaster>();
            try
            {
                using (IDbConnection conn = new SqlConnection(ConnString))
                {
                    string qry = @"SELECT	CategoryID
		                                    , CategoryName
		                                    , ImagePath
                                    FROM    tbl_ProductCategory;";
                    categorories = conn.Query<CategoryMaster>(qry, new { CommandType.Text }).ToList();
                }
                return categorories;
            }
            catch
            {
                return null;
            }
        }

        public List<UserDetails> GetUserdata()
        {
            List<UserDetails> userDetails = new List<UserDetails>();
            using (IDbConnection conn = new SqlConnection(ConnString))
            {
                string qry = @"select	user_id
		                                , user_name
		                                , RTRIM(LTRIM(ISNULL(first_name,'') + ' ' + ISNULL(last_name,''))) user_display_name
		                                , last_name
		                                , phone_no
		                                , email_id
		                                , address
                                from tbl_user_detail;";
                userDetails = conn.Query<UserDetails>(qry, commandType: CommandType.Text).ToList();
            }
            return userDetails;
        }

        public List<ProductDetail> GetProductDetails(int categoryid = 0)
        {
            List<ProductDetail> products = new List<ProductDetail>();
            try
            {
                using (IDbConnection conn = new SqlConnection(ConnString))
                {
                    string qry = @"SELECT	product_id
		                                    , Product_name
		                                    , price
		                                    , CategoryName
		                                    , total_stock
		                                    , (total_stock - ISNULL(booked_stock,0)) AS InStock
                                    FROM	tblProducts A
                                    LEFT JOIN	tbl_ProductCategory B
                                    ON		A.categoryid	= b.CategoryID
									WHERE	A.categoryId	= CASE WHEN ISNULL(@categoryid,0) = 0 THEN A.categoryId ELSE @categoryid END";
                    products = conn.Query<ProductDetail>(qry,new { categoryid = categoryid }, commandType: CommandType.Text).ToList();
                    if (products.Count > 0)
                    {
                        var qry2 = @"SELECT	CategoryID AS value
		                                    , CategoryName AS text 
                                    FROM	tbl_ProductCategory
                                    ORDER BY 2";
                        products[0].CategoryList = conn.Query<SelectList>(qry2, commandType: CommandType.Text).ToList();
                    }
                }
                return products;
            }
            catch
            {
                return null;
            }
        }
        public int CheckValidUser(LoginDetails login)
        {
            int UserType = 0;
            try
            {
                using (IDbConnection conn = new SqlConnection(ConnString))
                {
                    UserType = conn.Query<int>("sp_CheckValiduser", new { user_name = login.user_name, password = login.password }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
                return UserType;
            }
            catch
            {
                return 0;
            }
        }
        public string SaveNewcategory(string category_name)
        {
            string result = "";
            try
            {
                using (IDbConnection conn = new SqlConnection(ConnString))
                {
                    string qry = @"IF NOT EXISTS(SELECT 1 FROM tbl_ProductCategory WHERE categoryName = '@category_name')
                                    BEGIN
                                        INSERT INTO tbl_ProductCategory (CategoryName) VALUES(@category_name);
                                     
                                        SELECT 1 As Result
                                    END

                                ELSE
                                    BEGIN
                                    SELECT 2 As Result
                                    END";

                    result = conn.Query<string>(qry, new { category_name = category_name }, commandType: CommandType.Text).FirstOrDefault();

                    return result;
                }
            }
            catch (Exception ex)
            {
                return "0";
            }
        }

        public string SaveProduct(ProductDetail product)
        {
            string result = "";
            try
            {
                using (IDbConnection conn = new SqlConnection(ConnString))
                {
                    string qry = @"IF NOT EXISTS(SELECT 1 FROM tblproducts WHERE Product_name = '@Product_name')
                                    BEGIN
                                        INSERT INTO tblproducts (
							                                        Product_name
							                                        , price
							                                        , categoryId
						                                        )
				                                        VALUES	(
							                                        @Product_name
							                                        , @price
							                                        , @category_Id
						                                        )
                                     
                                        SELECT 1 As Result
                                    END

                                ELSE
                                    BEGIN
                                    SELECT 2 As Result
                                    END";

                    result = conn.Query<string>(qry, new { Product_name = product.Product_name, price = product.price, category_Id = product.categoryId }, commandType: CommandType.Text).FirstOrDefault();

                    return result;
                }
            }
            catch (Exception ex)
            {
                return "0";
            }
        }

        public string EditUpdateProduct(ProductDetail product)
        {
            string result = "1";
            try
            {
                using (IDbConnection conn = new SqlConnection(ConnString))
                {
                    string qry = @" UPDATE	tblproducts
                                    SET		total_stock = ISNULL(total_stock,0) + CONVERT(INT,@InStock),
                                            price		= @price
                                    WHERE	product_id	= @product_id";

                    conn.Execute(qry, new { InStock = product.InStock, price=product.price, product_id = product.product_id }, commandType: CommandType.Text);

                    return result;
                }
            }
            catch (Exception ex)
            {
                return "0";
            }
        }
    }
}
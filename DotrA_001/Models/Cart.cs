using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotrA_001.Models;
using DotrADatabase.Models;

namespace DotrA_001.Models
{
    //可序列化
    [Serializable]
    public class Cart:IEnumerable<CartProduct>
    {
        //private DotrADbContext db = new DotrADbContext();
        public Cart()
        {
            this.CartProducts = new List<CartProduct>();
        }
        //儲存購物車商品
        public  List<CartProduct> CartProducts;


        //計算商品總數
        public int count {
            get {
                return this.CartProducts.Count;
            }      
        }
        //使用ProductID新增一筆商品資料
        public bool AddProduct(int ProductId)
        {

            //取得商品詳細資料
            var currentitem = this.CartProducts.Where(s => s.ProductId == ProductId).Select (s=>s).FirstOrDefault() ;
            if (currentitem == default(CartProduct))
            {
                using (DotrADbContext db = new DotrADbContext())
                {
                    var product = (from s in db.Products
                                   where s.ProductID == ProductId
                                   select s).FirstOrDefault();
                    if (product != default(Product))
                    {
                        this.AddProduct(product);
                    }
                }

            }
            else { 
            currentitem.ProductQuantity++;
            }
            return true;
        }
        //新增一筆商品資料(使用Product物件)
        public bool AddProduct(Product product)
        {
            var item = new Models.CartProduct()
            {
                ProductId = product.ProductID,
                ProductName = product.ProductName,
                ProductPrice = product.UnitPrice,
                ProductQuantity = 1

            };
            this.CartProducts.Add(item);
            return true;
        }

        //此次購買總價
        public decimal TotalAmount
        {
            get {
                 decimal totalamount=0m;
                foreach(var caritem in this.CartProducts)
                    {
                    totalamount = totalamount + caritem.Amount;
                }
                return totalamount;
            }
        }
         






        public IEnumerator<CartProduct> GetEnumerator()
        {
            return ((IEnumerable<CartProduct>)CartProducts).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<CartProduct>)CartProducts).GetEnumerator();
        }
    }
}
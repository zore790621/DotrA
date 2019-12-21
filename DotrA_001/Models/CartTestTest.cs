using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Database.Models;



namespace DotrA_001.Models
{
    public class CartTestTest : IEnumerable<CartItem>
    {
        //儲存所有商品
        private List<CartItem> cartItems;
        //建構式
        public CartTestTest()
        {
            this.cartItems = new List<CartItem>();
        }
        public int Count
        {
            get
            {
                return this.cartItems.Count;
            }
        }
        public decimal GetTotal
        {
            get
            {
                decimal getTotal = 0.0m;
                foreach (var cartItem in this.cartItems)
                {
                    getTotal = getTotal + cartItem.Total;
                }
                return getTotal;
            }
        }
        
        //新增一筆Product，使用ProductId
        public bool AddProduct(int ProductId)
        {
            var findItem = this.cartItems
                            .Where(s => s.ProductId == ProductId)
                            .Select(s => s)
                            .FirstOrDefault();

            //判斷相同Id的CartItem是否已經存在購物車內
            if (findItem == default(Models.CartItem))
            {   //不存在購物車內，則新增一筆
                using (DotrADb db = new DotrADb())
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
            else
            {   //存在購物車內，則將商品數量累加
                findItem.Quantity += 1;
            }
            return true;
        }
        //新增一筆Product，使用Product物件
        private bool AddProduct(Product product)
        {
            //將Product轉為CartItem
            var cartItem = new Models.CartItem()
            {
                ProductId = product.ProductID,
                ProductName = product.ProductName,
                Price = product.SalesPrice,
                Picture = product.Picture,
                Quantity = 1
            };

            //加入CartItem至購物車
            this.cartItems.Add(cartItem);
            return true;
        }

        //刪除一筆Product，使用ProductId
        public bool RemoveProduct(int ProductId)
        {
            var findItem = this.cartItems
                            .Where(s => s.ProductId == ProductId)
                            .Select(s => s)
                            .FirstOrDefault();

            //判斷相同Id的CartItem是否已經存在購物車內
            if (findItem == default(Models.CartItem))
            {
                //不存在購物車內，不需做任何動作
            }
            else
            {   //存在購物車內，將商品移除
                this.cartItems.Remove(findItem);
            }
            return true;
        }
        //清空購物車
        public bool ClearCart()
        {
            this.cartItems.Clear();
            return true;
        }
        //將購物車商品轉成OrderDetail的List
        public List<OrderDetail> ToOrderDetailList(int orderId)
        {
            var result = new List<OrderDetail>();
            foreach (var cartItem in this.cartItems)
            {
                result.Add(new OrderDetail()
                {
                    ProductID = cartItem.ProductId,
                    SubTotal = cartItem.Total,
                    Quantity = (short)cartItem.Quantity,
                    OrderID = orderId
                });
            }
            return result;
        }
        
        
        #region IEnumerator

        public IEnumerator<CartItem> GetEnumerator()
        {
            return this.cartItems.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.cartItems.GetEnumerator();
        }
        #endregion
    }
}
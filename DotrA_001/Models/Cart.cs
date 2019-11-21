using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotrA_001.Models
{
    //可序列化
    [Serializable]
    public class Cart:IEnumerable<CartProduct>
    {
        public Cart()
        {
            this.CartProducts = new List<CartProduct>();
        }
        public List<CartProduct> CartProducts;


        //計算商品總數
        public int count {
            get {
                return this.CartProducts.Count;
            }
        
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
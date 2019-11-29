using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using ConnectionKey;

namespace DotrA_001.Models
{
    public class ConnectionDotrADb
    {
        public static DotrADb CreateDBContext()
        {
            //加解密的class
            MyEncrypt myEncrypt = new MyEncrypt();

            //從App.Config(or Web.Config)取出的設定加密字串，並解密
            String db_Catalog = myEncrypt.Decrypt(ConfigurationManager.AppSettings["db_Catalog"]);
            String db_User = myEncrypt.Decrypt(ConfigurationManager.AppSettings["db_User"]);
            String db_Pwd = myEncrypt.Decrypt(ConfigurationManager.AppSettings["db_Pwd"]);

            string db_ConnectionStr = string.Format(ConfigurationManager.AppSettings["db_ConnectionStr"], db_Catalog, db_User, db_Pwd);

            //回傳上面我自訂的ContosoUniversityEntities Constructor(有參數)
            return new DotrADb(db_ConnectionStr);
        }
    }
}
using System.Configuration;
using System.Data.Entity;

namespace DotrA_001.Models
{
    public partial class DotrADb:DbContext
    {
        public DotrADb(string cnStr) : base(cnStr)
        {

        }
    }
}
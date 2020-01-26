using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTestProject.Model
{
   public class ProductModel
    {

        public int id { get; set; }
        public string companyName { get; set; }
        public string name { get; set; }
        public string price { get; set; }
        public System.DateTime date { get; set; }
        public string keyword { get; set; }
        public string discount { get; set; }

    }
}

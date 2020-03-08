using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//the namespace has the folder path at the end of it (".PartialClasses") which needs to be removed to reference the file in the main project
namespace exerciseTwo
{
    public partial class SalesOrderHeader
    {
        public override string ToString()
        {
            return string.Format("{0}:{1} {2:C}",
                OrderDate.ToShortDateString(),
                SalesOrderID,
                TotalDue);
        }
    }
}

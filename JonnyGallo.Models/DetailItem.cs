using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonnyGallo.Models
{
    public class DetailItem<T> : ObservableEntityData where T:ObservableEntityData
    {
        public DetailItem(T detailObj)
        {
            DetailInnerObject = detailObj;
        }
        public T DetailInnerObject { get; set; }

        public string Author { get; set; }
    }
}

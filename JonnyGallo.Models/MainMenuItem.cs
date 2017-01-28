using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JonnyGallo.Models
{
    public class MainMenuItem : ObservableEntityData, IMainMenuItem
    {
        public string Name { get; set; }
    }

    public interface IMainMenuItem
    {
        string Name { get; set; }
    }
}

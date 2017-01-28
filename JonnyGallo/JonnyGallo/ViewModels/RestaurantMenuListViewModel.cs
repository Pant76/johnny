using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JonnyGallo.Abstractions;
using JonnyGallo.Models;
using JonnyGallo.ViewModels.Base;
using Microsoft.Practices.ServiceLocation;
using MvvmHelpers;

namespace JonnyGallo.ViewModels
{
    public class RestaurantMenuListViewModel : BaseCategorizableItemsListViewModel<Dish>
    {

        public RestaurantMenuListViewModel(Category selectedParentCategory) : base("Menu",CategorySettings.TypeMenuCat, selectedParentCategory)
        {
            
        }
        
    }
}

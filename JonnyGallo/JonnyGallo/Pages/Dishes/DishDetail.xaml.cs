using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JonnyGallo.Models;
using Xamarin.Forms;

namespace JonnyGallo.Pages
{
    public partial class DishDetail : ContentPage
    {
        protected Dish ViewModel => BindingContext as Dish;
        public DishDetail()
        {
            InitializeComponent();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JonnyGallo.Models;
using JonnyGallo.ViewModels;
using JonnyGallo.ViewModels.Base;
using Xamarin.Forms;

namespace JonnyGallo.Pages.Categories
{
    public partial class RestaurantsList : ContentPage
    {
        protected RestaurantsListViewModel ViewModel => BindingContext as RestaurantsListViewModel;

        public RestaurantsList()
        {
            InitializeComponent();
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var cat = (Category)e.Item;
            Navigation.PushAsync(new RestaurantMenuList() {BindingContext = new RestaurantMenuListViewModel(cat)});
          
            // prevents the list from displaying the navigated item as selected when navigating back to the list
            ((ListView)sender).SelectedItem = null;

        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Title = "Ristoranti";
            await ViewModel.ExecuteLoadCategoriesCommand("logo.png");
        }

  
    }
}

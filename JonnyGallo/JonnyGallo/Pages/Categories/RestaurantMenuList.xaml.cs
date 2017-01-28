using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JonnyGallo.Models;
using JonnyGallo.ViewModels;
using Xamarin.Forms;

namespace JonnyGallo.Pages.Categories
{
    public partial class RestaurantMenuList : ContentPage
    {
        protected RestaurantMenuListViewModel ViewModel => BindingContext as RestaurantMenuListViewModel;

        public RestaurantMenuList()
        {
            InitializeComponent();
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var cat = (Category) e.Item;
            Navigation.PushAsync(new Dishes() { BindingContext = new DishesListViewModel("Piatti") },true);

            // prevents the list from displaying the navigated item as selected when navigating back to the list
            ((ListView)sender).SelectedItem = null;
            // throw new NotImplementedException();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Title = "Menu";
            await ViewModel.ExecuteLoadCategoriesCommand();
        }

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}

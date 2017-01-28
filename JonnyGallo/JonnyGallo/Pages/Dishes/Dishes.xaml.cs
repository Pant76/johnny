using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JonnyGallo.Models;
using JonnyGallo.ViewModels;
using Xamarin.Forms;

namespace JonnyGallo.Pages
{
    public class MockDishesViewModel
    {
        public List<Dish> Dishes => new List<Dish>()
        {
            new Dish() {Title = "test"}
        };
    }
    public partial class Dishes : ContentPage
    {
        protected DishesListViewModel ViewModel => BindingContext as DishesListViewModel;

        public Dishes()
        {
            //dishList = new ObservableCollection<Dish>();
            InitializeComponent();
            //BindingContext =new  MockDishesViewModel();
            //dishesListView.ItemsSource = ViewModel.Dishes;
            ////Note that veggies is an observable collection, so the ListView will update in real time as items are added
            //dishList.Add(new Dish() {Title = "Test", Description = "Descrizione"});
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.ExecuteLoadDishesCommand();
        }

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var dish = (Dish)e.Item;
            //Navigation.PushAsync(new DishDetail() { BindingContext = dish });
            Navigation.PushAsync(new DishDetail() { BindingContext = new DetailItem<Dish>(dish) {Author = "BY Johnny Gallo"} });

            // prevents the list from displaying the navigated item as selected when navigating back to the list
            ((ListView)sender).SelectedItem = null;

        }
    }
}

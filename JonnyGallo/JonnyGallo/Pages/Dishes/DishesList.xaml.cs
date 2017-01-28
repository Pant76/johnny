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
    public class veggieModel1
    {
        public string name { get; set; }
        public string comment { get; set; }
        public bool isReallyAVeggie { get; set; }
        public string image { get; set; }
    }
    public partial class DishesList : ContentPage
    {
        protected DishesListViewModel ViewModel => BindingContext as DishesListViewModel;
        private ObservableCollection<Dish> _dishes =new ObservableCollection<Dish>();
        private ObservableCollection<veggieModel1> veggies { get; set; }

        public DishesList()
        {
            InitializeComponent();
            veggies=new ObservableCollection<veggieModel1>();
            //ListView1.ItemsSource = veggies;
            
                _dishes.Add(new Dish() {Title = "11111111"});
            veggies.Add(new veggieModel1() { name = "tomato", comment = "actually a fruit", isReallyAVeggie = false, image = "tomato.png" });
            veggies.Add(new veggieModel1() { name = "pizza", comment = "no comment", isReallyAVeggie = false, image = "pizza.png" });
            veggies.Add(new veggieModel1() { name = "romaine lettuce", comment = "good in salads", isReallyAVeggie = true, image = "lettuce.png" });
            veggies.Add(new veggieModel1() { name = "zucchini", comment = "grows relatively easily", isReallyAVeggie = true, image = "zucchini.png" });

            BindingContext = veggies;

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //await ViewModel.ExecuteLoadDishesCommand();
        }
    }
}

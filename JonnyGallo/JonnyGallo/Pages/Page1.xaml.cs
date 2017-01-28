using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace JonnyGallo.Pages
{
    public class veggieModel
    {
        public string name { get; set; }
        public string comment { get; set; }
        public bool isReallyAVeggie { get; set; }
        public string image { get; set; }
    }
    public partial class Page1 : ContentPage
    {
        private ObservableCollection<veggieModel> veggies { get; set; }
        public Page1()
        {
            veggies = new ObservableCollection<veggieModel>();
            InitializeComponent();
            listView.ItemsSource = veggies;
            //Note that veggies is an observable collection, so the ListView will update in real time as items are added
            veggies.Add(new veggieModel() { name = "tomato", comment = "actually a fruit", isReallyAVeggie = false, image = "tomato.png" });
            veggies.Add(new veggieModel() { name = "pizza", comment = "no comment", isReallyAVeggie = false, image = "pizza.png" });
            veggies.Add(new veggieModel() { name = "romaine lettuce", comment = "good in salads", isReallyAVeggie = true, image = "lettuce.png" });
            veggies.Add(new veggieModel() { name = "zucchini", comment = "grows relatively easily", isReallyAVeggie = true, image = "zucchini.png" });

        }
    }
}

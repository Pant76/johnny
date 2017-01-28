using System.Reflection;
using System.Threading.Tasks;
using JonnyGallo.Pages.Categories;
using JonnyGallo.Util;
using JonnyGallo.ViewModels;
using Xamarin.Forms;

namespace JonnyGallo.Pages
{
    /// <summary>
    /// Splash Page that is used on Android only. iOS splash characteristics are NOT defined here, ub tn the iOS prject settings.
    /// </summary>
    public partial class SplashPage : ContentPage
    {
        bool _shouldDelayForSplash = true;

        public SplashPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //var assembly = typeof(EmbeddedImages).GetTypeInfo().Assembly;
            //foreach (var res in assembly.GetManifestResourceNames())
            //{
            //    System.Diagnostics.Debug.WriteLine("found resource: " + res);
            //}


            if (_shouldDelayForSplash)
                // delay for a few seconds on the splash screen
                await Task.Delay(1000);

            // create a new NavigationPage, with a new JonnyGalloanceListPage set as the Root
            var navPage = new NavigationPage(
                new RestaurantsList())
            {
                Title = "Ristoranti",
                BindingContext = new RestaurantsListViewModel(),
                BarBackgroundColor = Color.FromHex("#9b151a")
            };
            navPage.BarTextColor = Color.White;
            //// create a new NavigationPage, with a new JonnyGalloanceListPage set as the Root
            //var navPage = new NavigationPage(
            //    new Dishes())
            //{
            //    Title = "Piatti",
            //    BindingContext = new DishesListViewModel()
            //    //BarTextColor = Color.White
            //};


            // set the MainPage of the app to the navPage
            Application.Current.MainPage = navPage;

            _shouldDelayForSplash = false;

        }
    }
}


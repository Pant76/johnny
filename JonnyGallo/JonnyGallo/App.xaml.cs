using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FormsToolkit;
using JonnyGallo.Costants;
using JonnyGallo.Pages;
using JonnyGallo.Pages.Categories;
using JonnyGallo.Util;
using JonnyGallo.ViewModels;
using Xamarin.Forms;

namespace JonnyGallo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            SubscribeToDisplayAlertMessages();

            

            // The navigation logic startup needs to diverge per platform in order to meet the UX design requirements
            if (Device.OS == TargetPlatform.Android)
            {
                // if this is an Android device, set the MainPage to a new SplashPage
                MainPage = new SplashPage();
            }
            else
            {
                // create a new NavigationPage, with a new JonnyGalloanceListPage set as the Root
                //var navPage =
                //    new NavigationPage(
                //        new MainPage())
                //    {
                //        BindingContext = new MainMenuViewModel(),
                //        Title = "Test",
                //        BarTextColor = Color.White
                //    };

				            var navPage = new NavigationPage(new RestaurantsList())
				{
					Title = "Ristoranti",
					BindingContext = new RestaurantsListViewModel(),
					BarBackgroundColor = Color.FromHex("#9b151a")
				};

                // set the MainPage of the app to the navPage
                MainPage = navPage;
            }
           
        }

        public static Page GetMainPage()
        {
            return new NavigationPage(new JonnyGallo.MainPage());
        }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }


        /// <summary>
        /// Subscribes to messages for displaying alerts.
        /// </summary>
        static void SubscribeToDisplayAlertMessages()
        {
            MessagingService.Current.Subscribe<MessagingServiceAlert>(MessageKeys.DisplayAlert, async (service, info) => {
                var task = Current?.MainPage?.DisplayAlert(info.Title, info.Message, info.Cancel);
                if (task != null)
                {
                    await task;
                    info?.OnCompleted?.Invoke();
                }
            });

            MessagingService.Current.Subscribe<MessagingServiceQuestion>(MessageKeys.DisplayQuestion, async (service, info) => {
                var task = Current?.MainPage?.DisplayAlert(info.Title, info.Question, info.Positive, info.Negative);
                if (task != null)
                {
                    var result = await task;
                    info?.OnCompleted?.Invoke(result);
                }
            });
        }
    }
}

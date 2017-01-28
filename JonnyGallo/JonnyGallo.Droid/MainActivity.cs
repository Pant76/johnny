using System;
using System.Collections.Generic;
using JonnyGallo.Common.Droid;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using JonnyGallo.Abstractions;
using JonnyGallo.Data;
using JonnyGallo.Models;
using JonnyGallo.Util;
using Microsoft.Practices.ServiceLocation;
using ZXing.Mobile;

namespace JonnyGallo.Droid
{
    [Activity(Label = "JonnyGallo", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            RegisterDependencies();

            Settings.OnDataPartitionPhraseChanged += (sender, e) =>
            {
                UpdateDataSourceIfNecessary();
            };

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            MobileBarcodeScanner.Initialize(Application);
            LoadApplication(new App());
        }

        /// <summary>
        /// Updates the data source if necessary.
        /// </summary>
        void UpdateDataSourceIfNecessary()
        {
            var dataSource = ServiceLocator.Current.GetInstance<IDataSource<Dish>>();

            // Set the data source dependent on whether or not the data parition phrase is "UseLocalDataSource".
            // The local data source is mainly for use in TestCloud test runs, but the app can be used in local-only data mode if desired.

            // if the settings dictate that a local data source should be used, then register the local data provider and update the IoC container
            if (Settings.IsUsingLocalDataSource && !(dataSource is FilesystemOnlyJonnyGalloDataSource))
            {
                var builder = new ContainerBuilder();
                builder.RegisterInstance(_LazyFilesystemOnlyJonnyGalloanceDataSource.Value).As<IDataSource<Dish>>();
                builder.Update(_IoCContainer);
                return;
            }

            //// if the settings dictate that a local data souce should not be used, then register the remote data source and update the IoC container
            //if (!Settings.IsUsingLocalDataSource && !(dataSource is AzureAcquaintanceSource))
            //{
            //    var builder = new ContainerBuilder();
            //    builder.RegisterInstance(_LazyAzureAcquaintanceSource.Value).As<IDataSource<Acquaintance>>();
            //    builder.Update(_IoCContainer);
            //}
        }

        /// <summary>
        /// Registers dependencies with an IoC container.
        /// </summary>
        /// <remarks>
        /// Since some of our libraries are shared between the Forms and Native versions 
        /// of this app, we're using an IoC/DI framework to provide access across implementations.
        /// </remarks>
        void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(new EnvironmentService()).As<IEnvironmentService>();

            builder.RegisterInstance(new HttpClientHandlerFactory()).As<IHttpClientHandlerFactory>();

            builder.RegisterInstance(new DatastoreFolderPathProvider()).As<IDatastoreFolderPathProvider>();

            // Set the data source dependent on whether or not the data parition phrase is "UseLocalDataSource".
            // The local data source is mainly for use in TestCloud test runs, but the app can be used in local-only data mode if desired.
            if (Settings.IsUsingLocalDataSource)
            {
                builder.RegisterInstance(_LazyFilesystemOnlyJonnyGalloanceDataSource.Value).As<IWordPressDataSource<Dish>>();

            }
            else
            {
                builder.RegisterInstance(_LazyWordPressDataSourceDish.Value).As<IWordPressDataSource<Dish>>();
            }

            _IoCContainer = builder.Build();

            var csl = new AutofacServiceLocator(_IoCContainer);
            ServiceLocator.SetLocatorProvider(() => csl);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            global::ZXing.Net.Mobile.Forms.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        // we need lazy loaded instances of these two types hanging around because if the registration on IoC container changes at runtime, we want the same instances
        static Lazy<FilesystemOnlyJonnyGalloDataSource> _LazyFilesystemOnlyJonnyGalloanceDataSource = new Lazy<FilesystemOnlyJonnyGalloDataSource>(() => new FilesystemOnlyJonnyGalloDataSource());


        static Lazy<WordPressDataSource<Dish>> _LazyWordPressDataSourceDish = new Lazy<WordPressDataSource<Dish>>(() => new WordPressDataSource<Dish>("http://johnny.softme.it/wp-json/wp/v2/",
            "highgrove-dish", null
            ));

        private IContainer _IoCContainer;

        public static IEnumerable<Dish> MockDishes => new List<Dish>()
        {
            new Dish() {Title = "Dish1",Tags = new[]{CategorySettings.TypeAntipastiCat,CategorySettings.TypeRestPadova}},
            new Dish() {Title = "Dish2",Tags = new[]{CategorySettings.TypeAntipastiCat,CategorySettings.TypeRestPadova}},
            new Dish() {Title = "Dish2",Tags = new[]{CategorySettings.TypeAntipastiCat,CategorySettings.TypeRestPadova}}

        };

        //static Lazy<AzureJonnyGalloanceSource> _LazyAzureJonnyGalloanceSource = new Lazy<AzureJonnyGalloanceSource>(() => new AzureJonnyGalloanceSource());

    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using JonnyGallo.Common.iOS;
using JonnyGallo.Abstractions;
using JonnyGallo.Util;
using JonnyGallo.Data;
using JonnyGallo.Models;
using Microsoft.Practices.ServiceLocation;
using FFImageLoading.Forms.Touch;
using Xamarin.Forms.Maps;
using Xamarin;
using FFImageLoading.Transformations;

namespace JonnyGallo.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
			var workaround = typeof(UXDivers.Artina.Shared.CircleImage);
            

			RegisterDependencies();

			Settings.OnDataPartitionPhraseChanged += (sender, e) =>
			{
				UpdateDataSourceIfNecessary();
			};

#if ENABLE_TEST_CLOUD
			Xamarin.Calabash.Start();
#endif

			Forms.Init();

			FormsMaps.Init();

			CachedImageRenderer.Init();
			var ignore = new CircleTransformation();

			LoadApplication(new App());
			ConfigureTheming();

            return base.FinishedLaunching(app, options);
        }

		void ConfigureTheming()
		{
			UINavigationBar.Appearance.TintColor = UIColor.White;
			UINavigationBar.Appearance.BarTintColor = Color.FromHex("547799").ToUIColor();
			UINavigationBar.Appearance.TitleTextAttributes = new UIStringAttributes { ForegroundColor = UIColor.White };
			UIBarButtonItem.Appearance.SetTitleTextAttributes(new UITextAttributes { TextColor = UIColor.White }, UIControlState.Normal);
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


	}
}

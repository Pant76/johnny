using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JonnyGallo.Abstractions;
using JonnyGallo.Models;
using JonnyGallo.ViewModels.Base;
using Microsoft.Practices.ServiceLocation;
using Xamarin.Forms;

namespace JonnyGallo.ViewModels
{
    public class MainMenuViewModel:BaseNavigationViewModel
    {
        // this is just a utility service that we're using in this demo app to mitigate some limitations of the iOS simulator

        Command _LoadJonnyGalloancesCommand;

        Command _RefreshJonnyGalloancesCommand;

        Command _NewJonnyGalloanceCommand;

        Command _ShowSettingsCommand;

    

        public MainMenuViewModel()
        {
            Title = "Benvenuto";
            SetDataSource();
        }

        async void SetDataSource()
        {
            var _DataSource = ServiceLocator.Current.GetInstance<IDataSource<Dish>>();

            var res = await _DataSource.GetItems();
            var res1 = res.ToArray();
        }

    }
}

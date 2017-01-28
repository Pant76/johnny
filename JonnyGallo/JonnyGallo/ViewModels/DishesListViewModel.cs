using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JonnyGallo.Abstractions;
using JonnyGallo.Models;
using JonnyGallo.ViewModels.Base;
using Microsoft.Practices.ServiceLocation;
using MvvmHelpers;

namespace JonnyGallo.ViewModels
{
    public class DishesListViewModel : BaseCategorizableItemsListViewModel<Dish>
    {
        private IWordPressDataSource<Dish> _DataSource;
        private ObservableRangeCollection<Dish> _Dishes;

        public ObservableRangeCollection<Dish> Dishes
        {
            get { return _Dishes ?? (_Dishes = new ObservableRangeCollection<Dish>()); }
            set
            {
                _Dishes = value;
                OnPropertyChanged("Dishes");
            }
        }
        public DishesListViewModel(string title) : base(title)
        {
            SetDataSource();
        }


        public async Task ExecuteLoadDishesCommand()
        {
            await FetchDishes();
        }

        async Task FetchDishes()
        {
            IsBusy = true;
            
            Dishes = new ObservableRangeCollection<Dish>(await _DataSource.GetItems());

            //// ensuring that this flag is reset
            //Settings.ClearImageCacheIsRequested = false;

            IsBusy = false;
        }

        void SetDataSource()
        {
            _DataSource = ServiceLocator.Current.GetInstance<IWordPressDataSource<Dish>>();
        }


    }
}

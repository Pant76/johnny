using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JonnyGallo.Abstractions;
using JonnyGallo.Models;
using Microsoft.Practices.ServiceLocation;
using MvvmHelpers;

namespace JonnyGallo.ViewModels.Base
{
    public class BaseCategorizableItemsListViewModel<T> : BaseNavigationViewModel where T : IObservableEntityData
    {
        private readonly string _categoryType;
        private readonly Category _parentCategory;
        private IWordPressDataSource<T> _dataSource;
        private ObservableRangeCollection<Category> _categories;
        private ObservableRangeCollection<T> _items;

        internal BaseCategorizableItemsListViewModel(string title,string categoryType=null, Category parentCategory=null)
        {
            
            _categoryType = categoryType;
            _parentCategory = parentCategory;
            Title = title;
            SetDataSource();
        }


        public ObservableRangeCollection<Category> Categories
        {
            get { return _categories ?? (_categories = new ObservableRangeCollection<Category>()); }
            set
            {
                _categories = value;
                OnPropertyChanged("Categories");
            }
        }
        public ObservableRangeCollection<T> Items
        {
            get { return _items ?? (_items = new ObservableRangeCollection<T>()); }
            set
            {
                _items = value;
                OnPropertyChanged("Items");
            }
        }


        public string ChildOfCategory { get; set; }

        //public async Task ExecuteLoadCategoriesCommand()
        //{
        //   throw new NotImplementedException();
        //}

        public async Task ExecuteLoadCategoriesCommand(string overrideImage=null)
        {
            if (_categoryType == null) throw new ArgumentNullException(nameof(_categoryType));

            IsBusy = true;
            var childOfTag = _parentCategory?.FullTag;
            var categories = await _dataSource.GetTags(_categoryType, childOfTag);
            Categories = new ObservableRangeCollection<Category>(categories.Select(c => new Category()
            {
                Title = c.Replace(_categoryType, ""),
                CategoryType = _categoryType,
                FullTag=c,
                ImgUrl = overrideImage ?? c.Replace(_categoryType, "")+".jpg"
            }));

            IsBusy = false;
        }

        public async Task ExecuteLoadItemsCommand(string catType, string childOf)
        {
            IsBusy = true;

            Items = new ObservableRangeCollection<T>(await _dataSource.GetItems());

            //// ensuring that this flag is reset
            //Settings.ClearImageCacheIsRequested = false;

            IsBusy = false;
        }

        async Task FetchCategories(string catType)
        {
            IsBusy = true;
            var categories = await _dataSource.GetTags(catType, ChildOfCategory);
            Categories = new ObservableRangeCollection<Category>(categories.Select(c => new Category()
            {
                Title = c.Replace(catType,""),
                CategoryType = catType
            }));

            IsBusy = false;
        }

        void SetDataSource()
        {
            _dataSource = ServiceLocator.Current.GetInstance<IWordPressDataSource<T>>();
        }

    }
}

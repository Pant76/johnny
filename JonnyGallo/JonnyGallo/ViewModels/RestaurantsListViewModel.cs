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
    public class RestaurantsListViewModel : BaseCategorizableItemsListViewModel<Dish>
    {

        public RestaurantsListViewModel() : base("Ristoranti", CategorySettings.TypeRestaurant)
        {


        }
        public async Task ExecuteLoadCategoriesCommand(string overrideImage = null)
        {

            Categories = new ObservableRangeCollection<Category>(new List<Category>()
            {
                new Category()
                {
                    Title = "Albignasego",
                    ImgUrl = "albignasego.jpg",
                    Text = "Descrizione ristorante",
                    FullTag = "ristorante_albignasego"
                },
                new Category()
                {
                    Title = "Padova",
                    ImgUrl = "padova.jpg",
                    Text = "Descrizione ristorante",
                    FullTag = "ristorante_padova"
                },
                new Category()
                {
                    Title = "Bassano",
                    ImgUrl = "bassano.jpg",
                    Text = "Descrizione ristorante",
                    FullTag = "ristorante_bassano"
                },
                new Category()
                {
                    Title = "Castelfranco",
                    ImgUrl = "castelfranco.jpg",
                    Text = "Descrizione ristorante",
                    FullTag = "ristorante_castelfranco"
                },
                 new Category()
                {
                    Title = "Roma",
                    ImgUrl = "roma.jpg",
                    Text = "Descrizione ristorante",
                    FullTag = "ristorante_roma"
                }
            });

            IsBusy = false;
        }


    }
}

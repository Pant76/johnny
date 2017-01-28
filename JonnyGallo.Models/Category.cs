using System;
namespace JonnyGallo.Models
{
    public interface ICategory
    {
        string Title { get; set; }
        string Text { get; set; }
        string CategoryType { get; set; }
    }

    public static class CategorySettings
    {
        public static string TypeMenuCat => "menu_";
        public static string TypeRestaurant => "ristorante_";


        public static string TypeRestPadova => "ristorante_padova";

        public static string TypeAntipastiCat => "menu_antipasti";
        public static string TypePiattiiCat => "menu_piatti";
    }
    public class Category : ObservableEntityData, ICategory
    {
        
        public Category()
        {

        }

        public string Title
        {
            get;
            set;
        }
        public string Text
        {
            get;
            set;
        }
        public string CategoryType
        {
            get;
            set;
        }

        public string ImgUrl { get; set; }
        public string FullTag { get; set; }
    }
}

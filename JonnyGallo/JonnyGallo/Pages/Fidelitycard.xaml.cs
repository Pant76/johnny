using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace JonnyGallo.Pages
{
    public partial class Fidelitycard : ContentPage
    {
        public Fidelitycard(string code)
        {
            InitializeComponent();
            InitBarcode(code);
        }

        private void InitBarcode(string code)
        {
            var barcode = new ZXingBarcodeImageView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BarcodeFormat = ZXing.BarcodeFormat.CODE_128,
                BarcodeOptions =
                {
                    Width = 800,
                    Height = 600,
                    Margin = 10
                },
            };
            //https://components.xamarin.com/gettingstarted/zxing.net.mobile.forms
            
            barcode.BarcodeValue = "ZXing.Net.Mobile";
            barcodeContainer.Children.Add(barcode);
            barcodeValue.Text = code;
        }
    }
}

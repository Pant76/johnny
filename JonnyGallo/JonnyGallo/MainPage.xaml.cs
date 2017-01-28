using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JonnyGallo.Pages;
using JonnyGallo.Util;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace JonnyGallo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            // Display names of embedded resources
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            foreach (var res in assembly.GetManifestResourceNames())
            {
                System.Diagnostics.Debug.WriteLine(">>> " + res);
            }


        }
        ZXingBarcodeImageView barcode;

        private void Button_OnClicked(object sender, EventArgs e)
        {
           

            //var img = BarcodeUtility.GetBarcode("123456789012345678");
            //barcodeImg.Source = ImageSource.FromStream(() => new MemoryStream(img));

            ////barcodeImg.Source = ImageSource.FromResource("JonnyGallo.resources.img.test.mnm.JPG");
            ////barcodeImg.Source = ImageSource.FromUri(new Uri("https://xamarin.com/content/images/pages/forms/example-app.png"));
        }

        private byte[] GetBytesFromEmbedded()
        {
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            using (var stream = assembly.GetManifestResourceStream("JonnyGallo.resources.img.test.mnm.JPG"))
            {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                // TODO: use the buffer that was read
                return buffer;
            }
            
        }

        async void GoToFidelity_OnClicked(object sender, EventArgs e)
        {
            
            await Navigation.PushAsync(new Fidelitycard("test") );
        }
    }
}

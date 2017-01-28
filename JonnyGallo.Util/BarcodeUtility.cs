using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;

namespace JonnyGallo.Util
{
    public static class BarcodeUtility
    {
        public static byte[] GetBarcode(string content)
        {
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions
                {
                    Width = 75,
                    Height = 25,
                    Margin = 30
                }
            };
            
            return writer.Write(content);
        }
    }
}

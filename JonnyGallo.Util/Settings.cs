using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace JonnyGallo.Util
{
    public static class Settings
    {
		private const string HockeyAppIdKey = "HockeyAppId_key";
		private static readonly string HockeyAppIdDefault = "11111111222222223333333344444444"; // This is just a placeholder value. Replace with your real HockeyApp App ID.

		private static string _dataPartitionPhrase = "UseWordPressDataSource";//"UseLocalDataSource";
        private static ISettings AppSettings => CrossSettings.Current;

        public static string DataPartitionPhrase
        {
            get { return _dataPartitionPhrase; }
            set { _dataPartitionPhrase = value; }
        }

        public static bool IsUsingLocalDataSource => DataPartitionPhrase == "UseLocalDataSource";

   

        public static event EventHandler OnDataPartitionPhraseChanged;

        /// <summary>
        /// Raises the data parition phrase changed event.
        /// </summary>
        /// <param name="e">E.</param>
        static void RaiseDataParitionPhraseChangedEvent(EventArgs e)
        {
            var handler = OnDataPartitionPhraseChanged;

            if (handler != null)
                handler(null, e);
        }
		public static string HockeyAppId
		{
			get { return AppSettings.GetValueOrDefault<string>(HockeyAppIdKey, HockeyAppIdDefault); }
			set { AppSettings.AddOrUpdateValue<string>(HockeyAppIdKey, value); }
		}
    }
}

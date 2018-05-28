using Android.Content;
using Android.Content.PM;
using Android.Preferences;
using PreferencesPoc.Droid;
using Xamarin.Forms;
using Application = Android.App.Application;

[assembly: Dependency(typeof(SharedPreferencesProvider))]
namespace PreferencesPoc.Droid
{
    public class SharedPreferencesProvider : ISharedPreferencesProvider
    {
        private readonly ISharedPreferences yieldMobilePreferences;
        private readonly ISharedPreferences xpMobilePreferences;
        private const string xpMobileApplicationId = "whatever.applicationid.you.want";

        public SharedPreferencesProvider()
        {
            var yieldMobileContext = Application.Context.ApplicationContext;
            yieldMobilePreferences = PreferenceManager.GetDefaultSharedPreferences(yieldMobileContext);

            try
            {
                var xpMobileContext =
                    Application.Context.CreatePackageContext(xpMobileApplicationId, PackageContextFlags.IgnoreSecurity);
                xpMobilePreferences = PreferenceManager.GetDefaultSharedPreferences(xpMobileContext);
            }
            catch (PackageManager.NameNotFoundException)
            {
            }
        }

        private static string ComposeKey(string nameSpace, string key) => $"{nameSpace}.{key}";

        private static string GetFromAppsPreferences(ISharedPreferences preferences, string key, string defaultValue) 
            => preferences == null ? string.Empty : preferences.GetString(key, defaultValue);

        public string Get(string nameSpace, string key, string defaultValue)
        {
            var composedKey = ComposeKey(nameSpace, key);
            return GetFromAppsPreferences(yieldMobilePreferences, composedKey, defaultValue);
        }

        public string GetFromOtherApp(string nameSpace, string key, string defaultValue)
        {
            var composedKey = ComposeKey(nameSpace, key);
            return GetFromAppsPreferences(xpMobilePreferences, composedKey, defaultValue);
        }

        public void Set(string nameSpace, string key, string value)
        {
            var composedKey = ComposeKey(nameSpace, key);

            using (var editor = yieldMobilePreferences.Edit())
            {
                editor.PutString(composedKey, value);
                editor.Commit();
            }
        }

        public void Remove(string nameSpace, string key)
        {
            var composedKey = ComposeKey(nameSpace, key);

            using (var editor = yieldMobilePreferences.Edit())
            {
                editor.Remove(composedKey);
                editor.Commit();
            }
        }
    }
}
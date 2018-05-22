using Android.Content;
using Android.Preferences;
using PreferencesPoc.Droid;
using Xamarin.Forms;
using Application = Android.App.Application;

[assembly: Dependency(typeof(SharedPreferencesProvider))]
namespace PreferencesPoc.Droid
{
    public class SharedPreferencesProvider : ISharedPreferencesProvider
    {
        private readonly ISharedPreferences preferences;

        public SharedPreferencesProvider()
        {
            var otherAppContext = Application.Context
                .CreatePackageContext("whatever.packagename.you.want", PackageContextFlags.IgnoreSecurity);

            preferences = PreferenceManager.GetDefaultSharedPreferences(otherAppContext);
        }

        private static string ComposeKey(string nameSpace, string key) => $"{nameSpace}.{key}";

        public void Set(string nameSpace, string key, string value)
        {
            var composedKey = ComposeKey(nameSpace, key);

            using (var editor = preferences.Edit())
            {
                editor.PutString(composedKey, value);
                editor.Commit();
            }
        }

        public string Get(string nameSpace, string key, string defaultValue)
        {
            var composedKey = ComposeKey(nameSpace, key);
            return preferences.GetString(composedKey, defaultValue);
        }

        public void Remove(string nameSpace, string key)
        {
            var composedKey = ComposeKey(nameSpace, key);

            using (var editor = preferences.Edit())
            {
                editor.Remove(composedKey);
                editor.Commit();
            }
        }
    }
}
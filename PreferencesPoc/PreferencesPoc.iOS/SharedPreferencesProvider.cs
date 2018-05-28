
using Foundation;
using PreferencesPoc.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(SharedPreferencesProvider))]
namespace PreferencesPoc.iOS
{
    public class SharedPreferencesProvider : ISharedPreferencesProvider
    {
        private NSUserDefaults userDefaults;

        private NSUserDefaults GetUserDefaults(string nameSpace)
            => userDefaults ?? new NSUserDefaults(nameSpace, NSUserDefaultsType.SuiteName);

        public string Get(string nameSpace, string key, string defaultValue)
        {
            userDefaults = GetUserDefaults(nameSpace);
            return userDefaults.StringForKey(key);
        }

        public string GetFromOtherApp(string nameSpace, string key, string defaultValue)
            => string.Empty;

        public void Set(string nameSpace, string key, string value)
        {
            userDefaults = GetUserDefaults(nameSpace);
            userDefaults.SetString(value, key);
        }

        public void Remove(string nameSpace, string key)
        {
            userDefaults = GetUserDefaults(nameSpace);
            userDefaults.RemoveObject(key);
        }
    }
}
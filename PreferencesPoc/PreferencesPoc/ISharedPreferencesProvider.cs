namespace PreferencesPoc
{
    public interface ISharedPreferencesProvider
    {
        string Get(string nameSpace, string key, string defaultValue);
        string GetFromOtherApp(string nameSpace, string key, string defaultValue);
        void Set(string nameSpace, string key, string value);
        void Remove(string nameSpace, string key);
    }
}

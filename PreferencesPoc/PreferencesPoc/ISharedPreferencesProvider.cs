namespace PreferencesPoc
{
    public interface ISharedPreferencesProvider
    {
        void Set(string nameSpace, string key, string value);
        string Get(string nameSpace, string key, string defaultValue);
        void Remove(string nameSpace, string key);
    }
}

using System;
using Xamarin.Forms;

namespace PreferencesPoc
{
    public partial class MainPage : ContentPage
    {
        private ISharedPreferencesProvider preferences;
        const string nameSpace = "whatever.namespace.you.want";
        const string key = "SharedTokenData";

        public MainPage()
        {
            InitializeComponent();

            preferences = DependencyService.Get<ISharedPreferencesProvider>();
        }

        private void Get(string nameSpace, string key)
        {
            var sharedInfo = preferences.Get(nameSpace, key, "0");
            labelGet.Text = $"Shared Info: {sharedInfo}";
        }

        public void OnGet(object sender, EventArgs e)
        {
            var composedKey = $"{key}.{entryGet.Text}";
            Get(nameSpace, composedKey);
        }

        public void OnSet(object sender, EventArgs e)
        {
            var composedKey = $"{key}.{entrySet.Text}";
            preferences.Set(nameSpace, composedKey, "654321");
            Get(nameSpace, composedKey);
        }
    }
}

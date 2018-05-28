using System;
using Xamarin.Forms;

namespace PreferencesPoc
{
    public partial class MainPage : ContentPage
    {
        private DevicePreferences preferences;

        public MainPage()
        {
            InitializeComponent();

            preferences = new DevicePreferences(DependencyService.Get<ISharedPreferencesProvider>(),
                DependencyService.Get<IJsonConverter<CustomerOverview>>());
        }

        private void Get(long customerId)
        {
            var sharedInfo = preferences.Get(customerId);
            labelGet.Text = $"Shared Info: {sharedInfo}";
        }

        public void OnGet(object sender, EventArgs e)
        {
            Get(Convert.ToInt64(entryGet.Text));
        }

        public void OnSet(object sender, EventArgs e)
        {
            var accountNumber = Convert.ToInt64(entrySet.Text);

            var account = new CustomerOverview
            {
                Account = accountNumber,
                DeviceId = 10000
            };

            preferences.Set(account);
            Get(accountNumber);
        }
    }
}

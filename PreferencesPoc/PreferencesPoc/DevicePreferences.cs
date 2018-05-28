using System.Collections.Generic;
using System.Linq;

namespace PreferencesPoc
{
    public class DevicePreferences
    {
        const string nameSpace = "whatever.shareduserid.you.want";
        const string key = "SharedTokenData";
        private readonly ISharedPreferencesProvider preferences;
        private readonly IJsonConverter<CustomerOverview> jsonConverter;

        public DevicePreferences(ISharedPreferencesProvider preferences, IJsonConverter<CustomerOverview> jsonConverter)
        {
            this.preferences = preferences;
            this.jsonConverter = jsonConverter;
        }

        public virtual long Get(long customerId)
        {
            var account = GetAccount(GetAccounts(), customerId);

            if (account == null)
                account = GetAccount(GetAccounts(true), customerId);

            return account?.DeviceId ?? 0;
        }

        public virtual void Set(CustomerOverview account)
        {
            var accounts = GetAccounts().ToList();
            var savedAccount = GetAccount(accounts, account.Account);

            if (savedAccount != null)
                accounts.Remove(savedAccount);

            accounts.Add(account);
            SetAccounts(accounts);
        }

        public virtual void Remove(long customerId)
        {
            var accounts = GetAccounts().ToList();

            var account = GetAccount(accounts, customerId);

            if (account == null) return;

            accounts.Remove(account);

            if (accounts.Any())
                SetAccounts(accounts);
            else
                preferences.Remove(nameSpace, key);
        }

        private IEnumerable<CustomerOverview> GetAccounts(bool getFromOtherApp = false)
        {
            var value = getFromOtherApp
                ? preferences.GetFromOtherApp(nameSpace, key, null)
                : preferences.Get(nameSpace, key, null);

            return value != null
                ? jsonConverter.Deserialize(value)
                : new List<CustomerOverview>();
        }

        private static CustomerOverview GetAccount(IEnumerable<CustomerOverview> accounts, long customerId)
            => accounts.FirstOrDefault(x => x.Account == customerId);

        private void SetAccounts(IEnumerable<CustomerOverview> accounts)
        {
            var accountsToBeSaved = jsonConverter.Serialize(accounts);
            preferences.Remove(nameSpace, key);
            preferences.Set(nameSpace, key, accountsToBeSaved);
        }
    }
}

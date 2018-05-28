namespace PreferencesPoc
{
    public class CustomerOverview
    {
        public long Account { get; set; }
        public long DeviceId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Hash { get; set; }

        public CustomerOverview()
        {

        }

        public CustomerOverview(long account, long deviceId, string name, string phone, string hash)
        {
            Account = account;
            DeviceId = deviceId;
            Name = name;
            Phone = phone;
            Hash = hash;
        }
    }
}

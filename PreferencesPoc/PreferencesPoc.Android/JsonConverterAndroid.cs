using Newtonsoft.Json;
using System.Collections.Generic;
using PreferencesPoc.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(JsonConverterAndroid))]
namespace PreferencesPoc.Droid
{
    public class JsonConverterAndroid : IJsonConverter<CustomerOverview>
    {
        public string Serialize(IEnumerable<CustomerOverview> value)
            => JsonConvert.SerializeObject(value);

        public IEnumerable<CustomerOverview> Deserialize(string value)
            => JsonConvert.DeserializeObject<IEnumerable<CustomerOverview>>(value);
    }
}
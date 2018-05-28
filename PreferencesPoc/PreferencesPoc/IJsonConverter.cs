using System.Collections.Generic;

namespace PreferencesPoc
{
    public interface IJsonConverter<T> where T : class
    {
        string Serialize(IEnumerable<T> value);
        IEnumerable<T> Deserialize(string value);
    }
}

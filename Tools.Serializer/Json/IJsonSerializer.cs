using System.IO;

namespace Tools.Serializer.Json
{
    public interface IJsonSerializer<T>
    {
        Stream Serialize(T value);
        T Deserialize(Stream stream);
    }
}

using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace Tools.Helper.Json
{
    public static class JsonHelper
    {
        public static Stream SerializeToStream(object value)
        {
            var ms = new MemoryStream();

            using (var sw = new StreamWriter(ms, Encoding.UTF8, 1024, true))
            {
                JsonSerializer ser = new JsonSerializer();

                ser.Serialize(sw, value);
                sw.Flush();
                ms.Position = 0; 
            }

            return ms;
        }

        public static T DeserializeFromStream<T>(Stream stream)
        {
            using (var sr = new StreamReader(stream))
            {
                using (var jr = new JsonTextReader(sr))
                {
                    return new JsonSerializer().Deserialize<T>(jr);
                }
            }
        }

        public static T DeserializeFromFile<T>(string path)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
        }
    }
}

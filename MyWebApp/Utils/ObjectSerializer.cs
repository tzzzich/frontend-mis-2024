using System.Runtime.Serialization.Formatters.Binary;

namespace MyWebApp.Utils
{
    public static class ObjectSerializer
    {
        public static byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static T Deserialize<T>(byte[] param)
        {
            using (MemoryStream ms = new MemoryStream(param))
            {
                BinaryFormatter br = new BinaryFormatter();
                return (T)br.Deserialize(ms);
            }
        }
    }
}

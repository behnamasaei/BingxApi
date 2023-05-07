using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace BingxApi
{
    public class BingxTools
    {
        public static string ReadGzip(byte[] receivedBytes)
        {
            // Decompress the received bytes
            using (MemoryStream compressedStream = new MemoryStream(receivedBytes))
            using (GZipStream decompressionStream = new GZipStream(compressedStream, CompressionMode.Decompress))
            using (MemoryStream decompressedStream = new MemoryStream())
            {
                decompressionStream.CopyTo(decompressedStream);
                byte[] decompressedBytes = decompressedStream.ToArray();

                string decompressedMessage = Encoding.UTF8.GetString(decompressedBytes);
                return decompressedMessage;
            }
        }

        public static List<string[]> ConvertJTokenToList(JToken jToken)
        {
            List<string[]> resultList = new List<string[]>();

            if (jToken is JArray jArray)
            {
                foreach (JArray innerArray in jArray)
                {
                    string[] subArray = innerArray.ToObject<string[]>();
                    resultList.Add(subArray);
                }
            }

            return resultList;
        }
    }
}

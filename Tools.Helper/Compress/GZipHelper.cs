using System.IO;
using System.IO.Compression;

namespace Tools.Helper.Compress
{
    public static class GZipHelper
    {
        public static Stream Compress(Stream source)
        {
            var ms = new MemoryStream();

            using (var gzip = new GZipStream(ms, CompressionMode.Compress, true))
            {
                source.CopyTo(gzip);
            }

            ms.Position = 0;

            return ms;
        }

        public static Stream Decompress(Stream source)
        {
            var ms = new MemoryStream();

            using (var gzip = new GZipStream(source, CompressionMode.Decompress, true))
            {
                gzip.CopyTo(ms);
            }

            ms.Position = 0;

            return ms;
        }
    }
}

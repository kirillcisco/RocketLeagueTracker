using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Common
{
    public class ImageManager
    {
        private static ImageManager _instance;

        private readonly string SourceFolder = Constants.SaveLocation + "Cache";
        private Dictionary<string, byte[]> _cache = new Dictionary<string, byte[]>();

        public static ImageManager Instance()
        {
            if (_instance == null)
            {
                _instance = new ImageManager();
            }

            return _instance;
        }

        public byte[] GetImageFromUri(string url)
        {
            return Task.Run(() => GetImageFromUriAsync(url).GetAwaiter().GetResult()).Result;
        }

        public async Task<byte[]> GetImageFromUriAsync(string url)
        {
            var segments = url.Split('/');
            string fileName = segments.Last();
            var thumbnailFullPath = SourceFolder + "\\" + fileName;

            if (!System.IO.Directory.Exists(SourceFolder))
                System.IO.Directory.CreateDirectory(SourceFolder);

            if (_cache.ContainsKey(fileName))
            {
                return _cache[fileName];
            }

            if(System.IO.File.Exists(thumbnailFullPath))
            {
                var data = System.IO.File.ReadAllBytes(thumbnailFullPath);
                _cache.Add(fileName, data);
                return _cache[fileName];
            }

            //Download file 
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsByteArrayAsync();
                    var sourceImage = thumbnailFullPath + ".src";
                    System.IO.File.WriteAllBytes(sourceImage, data);

                    using (Image image = Image.Load(sourceImage))
                    {
                        image.Mutate(x => x
                             .Resize(50, 50));

                        image.Save(thumbnailFullPath);
                    }

                    var bytes = System.IO.File.ReadAllBytes(thumbnailFullPath);
                    
                    _cache.Add(fileName, data);
                    return _cache[fileName];
                }
            }

            return null;
        }


    }
}

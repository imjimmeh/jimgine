using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Newtonsoft.Json;

namespace Jimgine.Core.Content
{
    public static class ContentService
    {
        public static ContentManager contentManager;  

        public static T LoadContent<T>(string path)
        {
            return contentManager.Load<T>(path);
        }

        public static T LoadJsonFile<T>(string path)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText("Content\\" + path));
        }
    }
}

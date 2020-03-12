using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jimgine.Core.Models.Levels
{
    [Serializable]
    public class LevelData
    {
        public LevelCharacterInformation[] Characters { get; set; }
        public LevelLayerInformation[] Layers { get; set; }

        [JsonProperty("NextLevel")]
        public string NextLevelFileName { get; set; }
    }
}

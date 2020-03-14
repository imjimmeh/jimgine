using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jimgine.Core.Models.Content.Levels
{
    public class LevelTerrainContainer
    {
        public LayerContainer[] Layers;
    }

    public class LayerContainer
    {
        public string Name;

        [JsonProperty("TileInfo")]
        public TileInfo[,] Tiles;
    }

    public struct TileInfo
    {
        [JsonProperty("Sprite")]
        public int SpriteNumber;

        [JsonProperty("IsWalkable")]
        public bool IsWalkable;
    }
}

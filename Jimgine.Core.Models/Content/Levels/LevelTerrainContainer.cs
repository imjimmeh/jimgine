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

        //TODO:
        //change this to two arrays; onne of ints, one of bools
        //file size is lmao for what it is atm
        [JsonProperty("TileInfo")]
        public TileInfo?[,] Tiles;
    }

    public struct TileInfo
    {
        [JsonProperty("Sprite")]
        public int SpriteNumber;

        [JsonProperty("IsWalkable")]
        public bool IsWalkable;
    }
}

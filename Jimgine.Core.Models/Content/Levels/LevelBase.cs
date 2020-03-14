using Jimgine.Core.Models.Levels;
using Jimgine.Core.Models.World.Characters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jimgine.Core.Models.Content.Levels
{

  public class LevelBase
  {
    [JsonProperty("Characters")]
    public string CharactersJsonPath { get; set; }

    [JsonProperty("Terrain")]
    public string TerrainJsonPath { get; set; }

    [JsonProperty("NextLevel")]
    public string NextLevelBaseJsonPath { get; set; }

    [JsonProperty("Sprite")]
    public string SpriteContainerJsonPath { get;set ;}
  }
}

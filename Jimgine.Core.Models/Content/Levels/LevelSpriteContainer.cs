using Jimgine.Core.Models.Graphics.Sprites;
using Newtonsoft.Json;

namespace Jimgine.Core.Models.Content.Levels
{
    public class LevelSpriteContainer
    {

        [JsonProperty("Sprites")]
        public Sprites SpriteData;

        public class Sprites
        {
            [JsonProperty("Level")]
            public SpriteData[] Level;

            [JsonProperty("Characters")]
            public SpriteData[] Characters;
        }
    }
}
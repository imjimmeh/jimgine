using Jimgine.Core.Content;
using Jimgine.Core.Models.Content.Levels;
using Jimgine.Core.Models.Levels;
using Jimgine.Core.Models.World.Characters;
using Jimgine.Core.Models.World.Characters.Players;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jimgine.Core.Manager.State.Levels
{
    public static class LevelFactory
    {
        static string _levelBaseJsonPath;
        static string GetPath(string fileName) => _levelBaseJsonPath + fileName; 

        public static Level GetLevel(string levelBaseJsonPath, string initialFileName)
        {

            _levelBaseJsonPath = levelBaseJsonPath;
            var levelBaseInfo = ContentService.LoadJsonFile<LevelBase>(GetPath(initialFileName));
            var spriteContainer = ContentService.LoadJsonFile<LevelSpriteContainer>(GetPath(levelBaseInfo.SpriteContainerJsonPath));
            var characterContainer = ContentService.LoadJsonFile<LevelData>(GetPath(levelBaseInfo.CharactersJsonPath));

            var level = new Level(characters: LoadCharacters(characterContainer.Characters, out Player player),
                layers: GetLayers(spriteContainer, levelBaseInfo.TerrainJsonPath));

            level.Player = player;

            level.SpriteNames = spriteContainer.SpriteData.Characters.Select(c => c.TexturePath).Concat(spriteContainer.SpriteData.Level.Select(c => c.TexturePath)).ToList();
            return level;
        }

        private static Character[] LoadCharacters(LevelCharacterInformation[] characters, out Player player)
        {
            var convertedCharacters = new Character[characters.Length];
            int count = 0;
            player = null;

            for (var x = 0; x < characters.Length; x++)
            {
                if (characters[x].IsPlayer)
                {
                    player = ContentService.LoadJsonFile<Player>(characters[x].File);
                    continue;
                }

                convertedCharacters[count] = LoadCharacter(characters[x]);
                count++;
            }

            return convertedCharacters;
        }

        private static Character LoadCharacter(LevelCharacterInformation character)
        {
            return ContentService.LoadJsonFile<Character>(character.File);
        }

        static Layer[] GetLayers(LevelSpriteContainer spriteInfo, string terrainPath)
        {
            var terrain = ContentService.LoadJsonFile<LevelTerrainContainer>(GetPath(terrainPath));

            var layers = new Layer[terrain.Layers.Length];
            for (var x = 0; x < terrain.Layers.Length; x++)
            {
                if (terrain.Layers[x].Tiles == null)
                    return null;

                layers[x] = new Layer();
                layers[x].Name = terrain.Layers[x].Name;
                layers[x].Tiles = new Tile[terrain.Layers[x].Tiles.GetLength(0), terrain.Layers[x].Tiles.GetLength(1)];
                for (var y = 0; y < terrain.Layers[x].Tiles.GetLength(0); y++)
                {
                    for(var z = 0; z < terrain.Layers[x].Tiles.GetLength(1); z++)
                    {
                        layers[x].Tiles[y, z] = ConvertSprite(ref terrain.Layers[x].Tiles[y, z], ref spriteInfo);
                    }
                }
            }

            return layers;
        }

        private static Tile ConvertSprite(ref TileInfo tileInfo, ref LevelSpriteContainer spriteInfo)
        {
            return new Tile()
            {
                Image = spriteInfo.SpriteData.Level[tileInfo.SpriteNumber],
                Walkable = tileInfo.IsWalkable
            };
        }
    }
}

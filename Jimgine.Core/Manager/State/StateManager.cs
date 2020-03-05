using Jimgine.Core.Content;
using Jimgine.Core.Input;
using Jimgine.Core.Manager.Players;
using Jimgine.Core.Models.Commands;
using Jimgine.Core.Models.Input;
using Jimgine.Core.Models.Levels;
using Jimgine.Core.Models.World.Characters;
using Jimgine.Core.Models.World.Characters.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace Jimgine.Core.Manager.State
{
    public class StateManager : IGameService
    {
        Level level;
        PlayerManager playerManager;
        InputService inputService;
        Player player;

        public Player Player { get => player; }

        public StateManager(InputService inputService)
        {
            this.inputService = inputService;

            playerManager = new PlayerManager();
        }

        public void Initialise()
        {
            inputService.AddInput(new KeyboardInputContainer(Keys.Left, new ActionCommand(delegate () { playerManager.SetMovingLeft(); })));
            inputService.AddInput(new KeyboardInputContainer(Keys.Right, new ActionCommand(delegate () { playerManager.SetMovingRight(); })));
            inputService.AddInput(new KeyboardInputContainer(Keys.Up, new ActionCommand(delegate () { playerManager.SetMovingUp(); })));
            inputService.AddInput(new KeyboardInputContainer(Keys.Down, new ActionCommand(delegate () { playerManager.SetMovingDown(); })));
            inputService.AddInput(new KeyboardInputContainer(Keys.None, new ActionCommand(delegate () { playerManager.SetNotMoving(); })));
        }

        public void LoadContent()
        {
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
            playerManager.Update(gameTime);
        }

        internal void LoadLevel(LevelData levelData)
        {
            if (level != null)
                level.Dispose();

            level = new Level();
            level.Characters = new Character[levelData.Characters.Length];
            for (var x = 0; x < level.Characters.Length; x++)
            {
                if (!levelData.Characters[x].IsPlayer)
                {

                    level.Characters[x] = ContentService.LoadJsonFile<Character>(levelData.Characters[x].File);
                    level.Characters[x].MaxSpeed = 5f;
                }
                else
                { 
                    player = ContentService.LoadJsonFile<Player>(levelData.Characters[x].File);
                    playerManager.SetPlayer(player);
                    player.Direction = new Vector3();
                }
            }

            Initialise();
        }

        internal IEnumerable<string> GetFilesToLoad()
        {
            return level.Characters.Where(c => c != null).Select(y => y.spriteData).SelectMany(x => x.Values).Select(x => x.TexturePath).Concat(player.spriteData.Values.Select(y => y.TexturePath));
        }

        internal IEnumerable<Character> GetCharacters()
        {
            for (var x = 0; x < level.Characters.Length; x++)
                yield return level.Characters[x];
        }
    }
}

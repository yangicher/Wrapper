using Assets.Wrapper.Scripts.Model;
using strange.extensions.command.impl;

namespace Assets.Wrapper.Scripts.Controller.Commands
{
    public class GameStateChangeCommand : Command
    {
        [Inject]
        public GameState GameState { get; set; }

        [Inject]
        public IGameModel GameModel { get; set; }

        public override void Execute()
        {
            GameModel.GameState = GameState;
        }
    }
}

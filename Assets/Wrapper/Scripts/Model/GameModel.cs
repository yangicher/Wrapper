using Assets.Wrapper.Scripts.Controller;
using UnityEngine;

namespace Assets.Wrapper.Scripts.Model
{
    public class GameModel : IGameModel
    {
        private GameState _gameState;

        [Inject]
        public CommonSignal.GameStateChangedSignal GameStateChangedSignal { get; set; }

        public GameState GameState
        {
            get
            {
                return _gameState;
            }
            set
            {
                if (_gameState != value)
                {
                    _gameState = value;

                    Debug.LogError("GM.GameState: " + _gameState);

                    switch (_gameState)
                    {
                        case GameState.Init:
                            //doResetModel();
                            break;
                        case GameState.GameStart:
                            //MODEL DOES NOTHING
                            break;
                        case GameState.GameEnd:
                            //MODEL DOES NOTHING
                            break;
                        case GameState.Menu:
                            //doRoundStart();
                            break;
                        case GameState.Null:
                            //MODEL DOES NOTHING
                            break;
                    }

                    GameStateChangedSignal.Dispatch(_gameState);
                }
            }
        }
    }
}

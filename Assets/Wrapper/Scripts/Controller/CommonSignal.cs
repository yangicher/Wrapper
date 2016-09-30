using strange.extensions.signal.impl;
using Assets.Wrapper.Scripts.Model;

namespace Assets.Wrapper.Scripts.Controller
{
    public class CommonSignal
    {
        public class GameStateChangeSignal : Signal<GameState> { }
        public class GameStateChangedSignal : Signal<GameState> { }

        public class StartSignal : Signal { }

        public class StartGameSignal : Signal { }
        public class StopGameSignal : Signal { }
        public class ChangeScoreSignal : Signal { }
    }
}

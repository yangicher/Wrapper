using strange.extensions.signal.impl;

namespace Assets.Wrapper.Scripts.Controller
{
    public class CommonSignal 
    {
        public class StartGameSignal : Signal { }
        public class StopGameSignal : Signal { }
        public class ChangeScoreSignal : Signal { }
    }
}

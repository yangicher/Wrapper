using strange.extensions.context.impl;
using UnityEngine;

namespace Assets.Wrapper.Scripts
{
    public class WrapperRoot : ContextView 
    {
        void Awake()
        {
            context = new WrapperContext(this, true);
            context.Start();
        }
    }
}

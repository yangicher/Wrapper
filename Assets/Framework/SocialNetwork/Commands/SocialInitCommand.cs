using Assets.Framework.Logger;
using Assets.Framework.SocialNetwork.Interfaces;
using strange.extensions.command.impl;
using strange.extensions.dispatcher.eventdispatcher.impl;

namespace Assets.Framework.SocialNetwork.Commands
{
    public class SocialInitCommand : Command
    {
        [Inject]
        public ISocialServiceApi Social { get; set; }

        public override void Execute()
        {
			var appId = (data as TmEvent).data as string;
			LogService.Log (this, LogType.Authentication, "Social init: {0}", appId);
            Social.Init(appId);
        }
    }
}
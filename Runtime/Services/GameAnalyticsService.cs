using UnityEngine;

using GameAnalyticsSDK;

using Backend.Services.Interfaces;

#if ENABLE_LOGS
using TalusFramework.Runtime.Utility.Logging;
#endif

namespace Backend.Services
{
    public class GameAnalyticsService : IAnalyticService, IGameAnalyticsATTListener
    {
        public void Register()
        {
#if ENABLE_LOGS
            TLog.Log("Register to Game Analytics!", null, LogType.Log);
#endif

            switch (Application.platform)
            {
                case RuntimePlatform.IPhonePlayer:
                    GameAnalytics.RequestTrackingAuthorization(this);
                    break;
                default:
                    GameAnalytics.Initialize();
                    break;
            }
        }

        public void PushProgressionEvent(Progression progression)
        {
            switch (progression.Type)
            {
                case ProgressionType.Start:
                    GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, Time.timeSinceLevelLoad.ToString());
                    break;
                case ProgressionType.Failure:
                    GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, Time.timeSinceLevelLoad.ToString());
                    break;
                case ProgressionType.Success:
                    GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, Time.timeSinceLevelLoad.ToString());
                    break;
            }
        }


        public void Unregister()
        {
#if ENABLE_LOGS
            TLog.Log("Unregister Game Analytics!", LogType.Warning);
#endif
            GameAnalytics.EndSession();
        }

        public void GameAnalyticsATTListenerNotDetermined() => GameAnalytics.Initialize();
        public void GameAnalyticsATTListenerRestricted() => GameAnalytics.Initialize();
        public void GameAnalyticsATTListenerDenied() => GameAnalytics.Initialize();
        public void GameAnalyticsATTListenerAuthorized() => GameAnalytics.Initialize();
    }
}
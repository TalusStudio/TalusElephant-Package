using System.Collections.Generic;
using Backend.Services;
using UnityEngine;
using Backend.Services.Interfaces;

namespace Backend
{
    [CreateAssetMenu(fileName = "New Backend Manager", menuName = "Backend/Backend Manager", order = 0)]
    public class BackendManagerSO : ScriptableObject
    {
        private static List<IAnalyticService> _InitialServices = new List<IAnalyticService>()
        {
            new FacebookService(),
            new ElephantService()
        };

        // TIP!
        // Only BeforeSceneLoad and AfterSceneLoad execute in the editor.
        // AfterAssembliesLoaded and BeforeSplashScreen do not execute in the IDE.
        // So you cannot test them very easily, nor depend on them for typical debugging.
        // If you intend to use something for a one time execution, its then Before and After SceneLoad will work
        // but make sure the state will not be harmed by repeat calls or that only one scene will be in use in your app.
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void RegisterAnalyticServices()
        {
#if ENABLE_LOGS
            Debug.Log("Backend Services registering...");
#endif
            foreach (IAnalyticService service in _InitialServices)
            {
                service.Register();
            }
        }

        public void UnregisterAnalyticServices()
        {
#if ENABLE_LOGS
            Debug.Log("Backend Services unregistering...");
#endif
            foreach (IAnalyticService service in _InitialServices)
            {
                service.Unregister();
            }

            _InitialServices.Clear();
            _InitialServices = null;
        }

        public void PushProgressionEvent(Progression progression)
        {
            foreach (IAnalyticService service in _InitialServices)
            {
                service.PushProgressionEvent(progression);
            }
        }
    }
}
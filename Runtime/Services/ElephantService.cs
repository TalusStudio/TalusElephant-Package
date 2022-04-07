using UnityEngine;
using UnityEngine.SceneManagement;

using ElephantSDK;

using Backend.Services.Interfaces;

namespace Backend.Services
{
    public class ElephantService : IAnalyticService
    {
        private static Params EventParameter
        {
            get
            {
                Params p = Params.New();
                p.Set("SceneIndex", SceneManager.GetActiveScene().buildIndex);
                p.Set("SceneName", SceneManager.GetActiveScene().name);
                p.Set("TimeSinceLevelLoad", Time.timeSinceLevelLoad);

                return p;
            }
        }

        public void Register()
        {
            Debug.Log("[Backend] Elephant registering...");
        }

        public void PushProgressionEvent(Progression progression)
        {
            Elephant.Event(progression.ToString(), SceneManager.GetActiveScene().buildIndex, EventParameter);
        }

        public void Unregister()
        {
            Debug.Log("[Backend] Elephant unregistering...");
        }

    }
}
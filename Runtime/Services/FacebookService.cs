using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Facebook.Unity;

using Backend.Services.Interfaces;

namespace Backend.Services
{
    public class FacebookService : IAnalyticService
    {
        private static Dictionary<string, object> EventParameter
        {
            get
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    ["SceneIndex"] = SceneManager.GetActiveScene().buildIndex,
                    ["SceneName"] = SceneManager.GetActiveScene().name,
                    ["TimeSinceLevelLoad"] = Time.timeSinceLevelLoad
                };

                return parameters;
            }
        }

        public void Register()
        {
            Debug.Log("[Backend] Facebook registering...");

            // Initialize FacebookSDK
            if (!FB.IsInitialized)
            {
                FB.Init(delegate
                {
                    if (FB.IsInitialized)
                    {
                        FB.ActivateApp();
                    }
                    else
                    {
                        Debug.Log("[Backend] Failed to Initialize the Facebook SDK!");
                    }
                }, isGameShown => { Time.timeScale = !isGameShown ? 0 : 1; });
            }
            else
            {
                // Already initialized, signal an app activation App Event
                FB.ActivateApp();
            }

        }

        public void PushProgressionEvent(Progression progression)
        {
            if (!FB.IsInitialized)
            {
                Debug.Log("[Backend] Facebook SDK not initialized!");
                return;
            }

            FB.LogAppEvent(progression.ToString(), SceneManager.GetActiveScene().buildIndex, EventParameter);
        }

        public void Unregister()
        {
            Debug.Log("[Backend] Facebook unregistering...");
            FB.LogOut();
        }

    }
}
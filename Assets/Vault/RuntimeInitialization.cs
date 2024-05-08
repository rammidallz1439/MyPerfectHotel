using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vault
{

    public class RuntimeInitialization
    {
        [RuntimeInitializeOnLoadMethod]
        private static void OnProjectInitialize()
        {
            var controller = Object.FindObjectOfType<ContextController>();

            if (controller == null)
            {
                var obj = Resources.Load<ContextController>("ProjectController");

                if (obj)
                {
                    controller = Object.Instantiate(obj);
                    Object.DontDestroyOnLoad(controller.gameObject);
                }
                else
                {
                    Debug.Log("[CONTEXT CONTROLLER] : ProjectController Not found");
                }
            }
            else
            {
                Object.DontDestroyOnLoad(controller.gameObject);
            }
        }
    }
}

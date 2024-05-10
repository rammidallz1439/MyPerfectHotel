using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vault;

namespace Game
{
    public class Player : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {

            EventManager.Instance.TriggerEvent(new CollectMoneyEvent(other.gameObject));
        }
        private void OnTriggerStay(Collider other)
        {
            EventManager.Instance.TriggerEvent(new OnPlayerCollisionEvent(other.gameObject));
            EventManager.Instance.TriggerEvent(new OnPlayerTriggedEvent(other.gameObject));
            EventManager.Instance.TriggerEvent(new CleanRoomEvent(other.gameObject));
            
        }
        private void OnTriggerExit(Collider other)
        {
            EventManager.Instance.TriggerEvent(new OnPlayerExitCollider(other.gameObject));

        }
    }
}


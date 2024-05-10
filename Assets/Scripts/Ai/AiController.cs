using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vault;

namespace Game
{
    public class AiController : AiHandler,IObserver
    {
        public void OnEnable()
        {
           
        }

        public void OnNotify()
        {
            manager  = Object.FindObjectOfType<GameManager>();
           
        }

        public void OnRelease()
        {
        }

        public void OnStart()
        {
            EventManager.Instance.TriggerEvent(new SpawnAiCharactersEvent());

        }

        public void RegisterListeners()
        {
            EventManager.Instance.AddListener<SpawnAiCharactersEvent>(SpawnCustomerHandler);
            EventManager.Instance.AddListener<GoToRoomEvent>(CustomerToRoomEvent);
            EventManager.Instance.AddListener<RoomFilledEvent>(RoomFilledeventHandler);
            EventManager.Instance.AddListener<RoomEmptiedEvent>(RoomEmptiedEventHanler);
            EventManager.Instance.AddListener<VacateRoomEvent>(VacateRoomEventHandler);
            EventManager.Instance.AddListener<CustomerRequeuEvent>(OnCustomerRequeue);

        }

        public void RemoveListeners()
        {
            EventManager.Instance.RemoveListener<SpawnAiCharactersEvent>(SpawnCustomerHandler);
            EventManager.Instance.RemoveListener<GoToRoomEvent>(CustomerToRoomEvent);
            EventManager.Instance.RemoveListener<RoomFilledEvent>(RoomFilledeventHandler);
            EventManager.Instance.RemoveListener<RoomEmptiedEvent>(RoomEmptiedEventHanler);
            EventManager.Instance.RemoveListener<VacateRoomEvent>(VacateRoomEventHandler);
            EventManager.Instance.AddListener<CustomerRequeuEvent>(OnCustomerRequeue);

        }
    }
}


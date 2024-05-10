using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vault;

namespace Game
{
    public class PlayerController : PlayerHandler, IObserver,ITickable
    {
        public void OnEnable()
        {
         
        }

        public void OnNotify()
        {
            manager = Object.FindObjectOfType<GameManager>();    
        }

        public void OnRelease()
        {

        }

        public void OnStart()
        {
            EventManager.Instance.TriggerEvent(new PlaceEarnedMoneyEvent());
        }

        public void RegisterListeners()
        {
            EventManager.Instance.AddListener<MovePlayerEvent>(MovePlayerEventHandler);
            EventManager.Instance.AddListener<OnPlayerCollisionEvent>(PlayerCollisionEvent);
            EventManager.Instance.AddListener<OnPlayerExitCollider>(PlayerExitcollisionHandler);
            EventManager.Instance.AddListener<OnPlayerTriggedEvent>(OnPlayerTriggered);
            EventManager.Instance.AddListener<SpawnMoneyEvent>(RewardMoneyEvent);
            EventManager.Instance.AddListener<PlaceEarnedMoneyEvent>(MoneyEarnedEventHandler);
        }

        public void RemoveListeners()
        {
            EventManager.Instance.RemoveListener<MovePlayerEvent>(MovePlayerEventHandler);
            EventManager.Instance.RemoveListener<OnPlayerCollisionEvent>(PlayerCollisionEvent);
            EventManager.Instance.RemoveListener<OnPlayerExitCollider>(PlayerExitcollisionHandler);
            EventManager.Instance.RemoveListener<OnPlayerTriggedEvent>(OnPlayerTriggered);
            EventManager.Instance.RemoveListener<SpawnMoneyEvent>(RewardMoneyEvent);
            EventManager.Instance.RemoveListener<PlaceEarnedMoneyEvent>(MoneyEarnedEventHandler);


        }

        public void Tick()
        {
            EventManager.Instance.TriggerEvent(new MovePlayerEvent());
         
        }
    }
}


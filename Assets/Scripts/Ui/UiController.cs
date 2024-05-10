using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vault;

namespace Game.Ui
{
    public class UiController : UiHandler,IObserver
    {
        public void OnEnable()
        {
        }

        public void OnNotify()
        {
            Manager = Object.FindObjectOfType<GameManager>();
        }

        public void OnRelease()
        {
        }

        public void OnStart()
        {
            EventManager.Instance.TriggerEvent(new StartUiImplimentation());
        }

        public void RegisterListeners()
        {
            EventManager.Instance.AddListener<FillKeySilderEvent>(FillKeySliderEventHandler);
            EventManager.Instance.AddListener<CollectMoneyEvent>(CollectMoneyEventHandler);
            EventManager.Instance.AddListener<StartUiImplimentation>(StartUi);
            EventManager.Instance.AddListener<CleanRoomEvent>(CleanRoomEventHandler);
        }

        public void RemoveListeners()
        {
            EventManager.Instance.RemoveListener<FillKeySilderEvent>(FillKeySliderEventHandler);
            EventManager.Instance.RemoveListener<CollectMoneyEvent>(CollectMoneyEventHandler);
            EventManager.Instance.RemoveListener<StartUiImplimentation>(StartUi);
            EventManager.Instance.RemoveListener<CleanRoomEvent>(CleanRoomEventHandler);




        }
    }
}


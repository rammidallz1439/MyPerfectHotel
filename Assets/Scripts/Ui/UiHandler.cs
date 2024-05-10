using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vault;


namespace Game.Ui
{

    public class UiHandler
    {
        protected GameManager Manager;
        #region handler methods

        protected void StartUi(StartUiImplimentation e)
        {
            ImplimentUI();
        }
        protected void FillKeySliderEventHandler(FillKeySilderEvent e)
        {
            FillSlider(e.KeySlider, e.RoomAvailable, e.WaitingCompleted);
        }
        protected void CollectMoneyEventHandler(CollectMoneyEvent e)
        {
            CollectRewardOnTrigger(e.MoneyObject);
        }

        protected void CleanRoomEventHandler(CleanRoomEvent e)
        {
            if(!Manager.TriggeredCleaner)
            {
                if (e.Collision.gameObject.tag == "CleanTrigger")
                {
                    Manager.TriggeredCleaner = true;
                    CleanSlider parent = e.Collision.transform.parent.GetComponent<CleanSlider>();
                    parent.transform.GetComponent<Slider>().DOValue(1f, 1f).OnComplete(() => {

                        Room room = Manager.Rooms[parent.RoomNo];
                        room.CleanedCount--;
                        DataManager.Instance.SaveData(room.CleanedCount, GameConstants.RoomCleanedCount);

                        if (room.CleanedCount == 0)
                        {
                            room.isFilled = false;
                            DataManager.Instance.SaveData(room.isFilled, GameConstants.RoomFilled);
                            Manager.Waiting = true;
                        }

                        parent.gameObject.SetActive(false);
                    }).WaitForCompletion();


                }
            }
           

        }
        #endregion

        #region Methods
        void FillSlider(Slider slider, bool avaialble, Action<bool> callBack)
        {
            if (slider != null)
            {
                if (!avaialble)
                {

                    slider.DOValue(1, 2f).OnComplete(() =>
                    {
                        slider.value = 0;
                        callBack.Invoke(true);
                        MEC.Timing.CallDelayed(0.1f, () =>
                        {

                            EventManager.Instance.TriggerEvent(new SpawnMoneyEvent());

                        });
                    }).WaitForCompletion();

                }
            }
        }

        void CollectRewardOnTrigger(GameObject obj)
        {
            if (obj.CompareTag("Money"))
            {
                obj.transform.GetComponent<BoxCollider>().enabled = false;
                Manager.CollectedMoneyCount++;
                DataManager.Instance.SaveData<int>(Manager.CollectedMoneyCount, GameConstants.CollectedMoneyCount);
                obj.transform.DOMoveY(5, 0.2f).OnComplete(() =>
                {
                    obj.transform.DOMove(Manager.Player.transform.position, 0.5f).OnComplete(() =>
                    {
                        Manager.CollectedMoney.text = "Cash: " + Manager.CollectedMoneyCount.ToString();
                        Manager.Moneys.Remove(obj);
                        MonoHelper.instance.DestroyObject(obj);
                      
                    }).WaitForCompletion();
                }).WaitForCompletion();

                if (!Manager.RoomEntered)
                {
                    Manager.MoneyEarned--;
                    DataManager.Instance.SaveData<int>(Manager.MoneyEarned, GameConstants.MoneySaved);
                }
               
            }

        }

        void ImplimentUI()
        {
            Manager.CollectedMoneyCount = DataManager.Instance.Load<int>(GameConstants.CollectedMoneyCount);

            Manager.CollectedMoney.text = "Cash: " + Manager.CollectedMoneyCount.ToString();
            
            foreach (Room item in Manager.Rooms)
            {
                item.isFilled = DataManager.Instance.Load<bool>(GameConstants.RoomFilled); 
                if(item.isFilled)
                {
                    item.CleanedCount = DataManager.Instance.Load<int>(GameConstants.RoomCleanedCount);
                    if(item.CleanedCount <= 0)
                    {
                        item.isFilled = false;
                    }
                    for (int i = 0; i < item.CleanedCount; i++)
                    {
                        item.CleanSliders[i].gameObject.SetActive(true);
                    }

                }
            }
        }
        #endregion
    }

}


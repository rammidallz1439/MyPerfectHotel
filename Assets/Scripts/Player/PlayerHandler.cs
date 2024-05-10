using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using Vault;

namespace Game
{
    public class PlayerHandler
    {
        protected GameManager manager;

        #region handler methods
        protected void MovePlayerEventHandler(MovePlayerEvent e)
        {
            /*#if UNITY_EDITOR
                        PlayerMovement();
            #else
            PlayerTouchMovement();
            #endif*/
            PlayerTouchMovement();
        }

        protected void PlayerCollisionEvent(OnPlayerCollisionEvent e)
        {

            RoatateCameraOnCollison(e.CollidedObject);


        }
        protected void PlayerExitcollisionHandler(OnPlayerExitCollider e)
        {
            OnPlayerExitCollider(e.CollidedObject);
        }
        protected void OnPlayerTriggered(OnPlayerTriggedEvent e)
        {

            if (manager.Waiting)
            {
                Debug.Log("Collided");
                if (e.TriggedObject.CompareTag("KeyTrigger"))
                {
                    EventManager.Instance.TriggerEvent(new FillKeySilderEvent(manager.KeySilder, RetunRoomAvailablity(),
                 (sucess) =>
                 {
                     manager.Waiting = true;
                     EventManager.Instance.TriggerEvent(new GoToRoomEvent());
                 }));
                    manager.Waiting = false;
                }
            }


        }

        protected void RewardMoneyEvent(SpawnMoneyEvent e)
        {
            MoneyPlaceMent();
        }

        protected void MoneyEarnedEventHandler(PlaceEarnedMoneyEvent e)
        {
            manager.MoneyEarned = DataManager.Instance.Load<int>(GameConstants.MoneySaved);
            StartMoneyPlacement();
        }

        #endregion


        #region methods
        void PlayerMovement()
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 mousePos = manager.mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, manager.mainCamera.transform.position.y));

                Vector3 direction = mousePos - manager.Player.position;
                direction.y = 0f;
                manager.Player.Translate(direction.normalized * manager.moveSpeed * Time.deltaTime, Space.World);
                RotateTowards(mousePos);
            }
        }
        void RotateTowards(Vector3 targetPosition)
        {
            Vector3 direction = targetPosition - manager.Player.position;
            direction.y = 0f;
            Quaternion rotation = Quaternion.LookRotation(direction);
            manager.Player.rotation = Quaternion.Slerp(manager.Player.rotation, rotation, manager.rotationSpeed * Time.deltaTime);
        }



        void PlayerTouchMovement()
        {
            if (manager.IsJoystick)
            {
                var movementDirection = new Vector3(-manager.joystick.Direction.x, 0.0f, -manager.joystick.Direction.y).normalized;
                manager.characterController.SimpleMove(movementDirection * manager.moveSpeed);
                if(movementDirection != null)
                {
                    manager.targetDirection = Vector3.RotateTowards(manager.characterController.transform.forward, movementDirection, manager.rotationSpeed * Time.deltaTime, 0.0f);
                    manager.characterController.transform.rotation = Quaternion.LookRotation(manager.targetDirection);
                }
               
                
            }

        }

        void RoatateCameraOnCollison(GameObject other)
        {

            if (other.CompareTag("LeftDoor"))
            {
                manager.RoomEntered = true;
                manager.mainCamera.transform.DORotateQuaternion(Quaternion.Euler(60f, 160f, 0f), 0.5f);
            }
            else if (other.CompareTag("RightDoor"))
            {
                manager.RoomEntered = true;
                manager.mainCamera.transform.DORotateQuaternion(Quaternion.Euler(60f, 200f, 0f), 0.5f);
            }


        }

        void OnPlayerExitCollider(GameObject obj)
        {
            if (obj.CompareTag("RightDoor") || obj.CompareTag("LeftDoor"))
            {
                manager.mainCamera.transform.DORotateQuaternion(Quaternion.Euler(50f, 180f, 0f), 0.5f);
                manager.RoomEntered = false;

            }
            if (obj.CompareTag("CleanTrigger"))
            {
                manager.TriggeredCleaner = false;
            }

        }

        bool RetunRoomAvailablity()
        {
            manager.room = manager.Rooms.Find(x => x.isFilled == false);
            if (manager.room == null)
            {
                return true;
            }

            return manager.room.isFilled;
        }


        void MoneyPlaceMent()
        {

            GameObject obj = MonoHelper.instance.InstantiateObject(manager.Money, manager.CustomerSpot.position, Quaternion.identity);
            manager.Moneys.Add(obj);


            if (manager.Moneys.Count <= 1)
            {
                float x = manager.MoneySpot.transform.position.x + 1;
                float y = manager.MoneySpot.transform.position.y;
                float z = manager.MoneySpot.transform.position.z;
                obj.transform.DOMove(new Vector3(x, y, z), 0.3f);
                obj.transform.GetComponent<BoxCollider>().enabled = true;

            }
            else
            {

                float x = manager.Moneys[manager.MoneyEarned - 1].transform.position.x + 1;
                float y = manager.MoneySpot.transform.position.y;
                float z = 0;
                if (manager.Moneys.Count <= 4)
                {
                    z = manager.MoneySpot.transform.position.z;
                }
                else
                {
                    x = manager.Moneys[manager.MoneyEarned - 4].transform.position.x;
                    z = manager.Moneys[manager.MoneyEarned - 4].transform.position.z + 1.5f;

                }
                obj.transform.DOMove(new Vector3(x, y, z), 0.3f);
                obj.transform.GetComponent<BoxCollider>().enabled = true;

            }
            manager.MoneyEarned++;
            DataManager.Instance.SaveData<int>(manager.MoneyEarned, GameConstants.MoneySaved);


        }


        void StartMoneyPlacement()
        {
            GameObject obj = null;

            for (int j = 0; j < manager.MoneyEarned; j++)
            {
                if (j == 0)
                {
                    obj = MonoHelper.instance.InstantiateObject(manager.Money, manager.MoneySpot.position, Quaternion.identity);
                    manager.Moneys.Add(obj);
                    obj.GetComponent<BoxCollider>().enabled = true;
                }
                else
                {
                    obj = MonoHelper.instance.InstantiateObject(manager.Money);
                    manager.Moneys.Add(obj);
                    obj.GetComponent<BoxCollider>().enabled = true;
                    float x = manager.Moneys[j - 1].transform.position.x + 1;
                    float y = manager.MoneySpot.transform.position.y;
                    float z = 0;
                    if (manager.Moneys.Count <= 4)
                    {
                        z = manager.MoneySpot.transform.position.z;
                    }
                    else
                    {
                        x = manager.Moneys[j - 4].transform.position.x;
                        z = manager.Moneys[j - 4].transform.position.z + 1.5f;
                    }
                    obj.transform.position = new Vector3(x, y, z);
                }
            }

        }
    }
    #endregion
}




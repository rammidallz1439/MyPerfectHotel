using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Vault;

namespace Game
{
    public class AiHandler
    {
        protected GameManager manager;
    
        #region handler methods
        protected void SpawnCustomerHandler(SpawnAiCharactersEvent e)
        {

            CreateCustomers();
        }

        protected void CustomerToRoomEvent(GoToRoomEvent e)
        {
            GoRoom();
           
        }

        protected void RoomFilledeventHandler(RoomFilledEvent e)
        {
            if (e.collision.CompareTag("LeftDoor") || e.collision.CompareTag("RightDoor"))
            {
               room.isFilled = true;
            }

        }

        protected void RoomEmptiedEventHanler(RoomEmptiedEvent e)
        {
            RoomEmpited(e.collision, e.CurrentCustomer);
        }

        protected void VacateRoomEventHandler(VacateRoomEvent e)
        {
            if (e.Collision.CompareTag("LeftDoor") || e.Collision.CompareTag("RightDoor"))
            {
                MEC.Timing.CallDelayed(Random.Range(7,11), () =>
                {
                    VacateRoom(e.CustomerInRoom);
                });
            }
        }

        protected void OnCustomerRequeue(CustomerRequeuEvent e)
        {
            if (e.Collision.CompareTag("Requeue"))
            {
                NavMeshAgent agent = e.CustomerTriggered.GetComponent<NavMeshAgent>();
                agent.isStopped = true;
                
                e.CustomerTriggered.transform.position = new Vector3(manager.Customers.ToArray()[manager.Customers.Count - 1].transform.position.x,
                  manager.Customers.ToArray()[manager.Customers.Count - 1].transform.position.y,
                  manager.Customers.ToArray()[manager.Customers.Count - 1].transform.position.z + 2
                  );
                e.CustomerTriggered.transform.DORotate(new Vector3(0f, 180f, 0f), 0.2f, RotateMode.Fast);
                manager.Customers.Enqueue(e.CustomerTriggered);
            }
        }
        #endregion


        #region methods

        void CreateCustomers()
        {
            GameObject previousObj = null;


            for (int i = 0; i < manager.SpawnCount; i++)
            {
                GameObject spawnObj = PoolManager.Instance.GetPooledObject("Customer");

                if (i == 0)
                {
                    spawnObj.transform.position = manager.CustomerSpot.position;
                }
                else
                {
                    spawnObj.transform.position = previousObj.transform.position + new Vector3(0f, 0f, 2f);
                }
                spawnObj.name = spawnObj.name + i;
                manager.Customers.Enqueue(spawnObj.transform.GetComponent<Customer>());
                previousObj = spawnObj;
            }
        }
        Room room;
        void GoRoom()
        {

            Customer customer = manager.Customers.Dequeue();
            if (customer != null)
            {
                NavMeshAgent agent = customer.transform.GetComponent<NavMeshAgent>();
                room = manager.Rooms.Find(x => x.isFilled == false);
                agent.SetDestination(room.transform.position);
                room.isFilled = true;
                DataManager.Instance.SaveData(room.isFilled, GameConstants.RoomFilled);
                customer.RoomTaken = room.RoomNumber;
                MEC.Timing.CallDelayed(0.6f, () =>
                {

                    foreach (Customer item in manager.Customers)
                    {
                        item.transform.DOMoveZ(item.transform.position.z - 2f, 0.5f);
                    }
                });

            }

        }

        void VacateRoom(Customer cus)
        {
           
            NavMeshAgent agent = cus.transform.GetComponent<NavMeshAgent>();
          //  Transform pos = manager.Customers.ToArray()[manager.Customers.Count - 1].transform;
            agent.SetDestination(manager.ReQueuePos.position);
        }


        void RoomEmpited(GameObject collision,Customer CurrentCustomer)
        {
            if (collision.CompareTag("LeftDoor") || collision.CompareTag("RightDoor"))
            {
                foreach (Slider item in manager.Rooms[CurrentCustomer.RoomTaken].CleanSliders)
                {
                    item.gameObject.SetActive(true);
                    manager.Rooms[CurrentCustomer.RoomTaken].CleanedCount++;
                    DataManager.Instance.SaveData(manager.Rooms[CurrentCustomer.RoomTaken].CleanedCount, GameConstants.RoomCleanedCount);
                }
                foreach (Transform item in manager.Rooms[CurrentCustomer.RoomTaken].MoneyPoses)
                {
                    GameObject money = MonoHelper.instance.InstantiateObject(manager.Money, item.position, Quaternion.identity);
                    money.transform.GetComponent<BoxCollider>().enabled = true;
                }
           
              
            }
        }
        #endregion

    }
}


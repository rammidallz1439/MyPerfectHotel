using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vault;

namespace Game
{
    public class AiManager : MonoBehaviour
    {
        public Queue<Customer> Customers = new Queue<Customer>();
        public Transform CustomerSpawnPoint;
        public int SpawnCount;
        public List<Room> Rooms;

    }

}

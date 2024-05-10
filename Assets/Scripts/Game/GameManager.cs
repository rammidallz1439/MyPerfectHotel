using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [Header("PlayerMovement")]
        public Transform Player;
        public float moveSpeed = 5f;
        public float rotationSpeed = 5f;
        public Camera mainCamera;
        public bool RoomEntered;
        public Vector3 targetDirection ;
        public Vector2 lastInputPosition ;
        public VariableJoystick joystick ;
        public CharacterController characterController;
        public Canvas canvas;


        [Header("UI")]
        public Slider KeySilder;
        public TMP_Text CollectedMoney;


        [Header("bool")]
        public bool RoomIsAvailable;
        public bool PlayerOnKeyTrigger;
        public bool Waiting = true;
        public bool TriggeredCleaner;
        public bool IsJoystick = true;
   

        [Header("Ai")]
        public int SpawnCount;
        public List<Room> Rooms;
        public Room room;
        public Queue<Customer> Customers = new Queue<Customer>();
        public Transform CustomerSpot;
        public Transform ReQueuePos;

        [Header("Rewards")]
        public Transform MoneySpot;
        public GameObject Money;
        public List<GameObject> Moneys;

        [Header("Save Data")]
        public int MoneyEarned;
        public int CollectedMoneyCount;
        


    }
}


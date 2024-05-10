using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vault;

public class Customer : MonoBehaviour
{
    public int RoomTaken;

    private void OnTriggerEnter(Collider other)
    {
        EventManager.Instance.TriggerEvent(new CustomerRequeuEvent(other.gameObject,this));
    }
    private void OnTriggerStay(Collider other)
    {
        EventManager.Instance.TriggerEvent(new RoomFilledEvent(other.gameObject));

        EventManager.Instance.TriggerEvent(new VacateRoomEvent(this,other.gameObject));

    }

    private void OnTriggerExit(Collider other)
    {
        EventManager.Instance.TriggerEvent(new RoomEmptiedEvent(other.gameObject, this));

    }
}

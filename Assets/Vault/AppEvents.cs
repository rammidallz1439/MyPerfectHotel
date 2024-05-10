
using System;
using UnityEngine;
using UnityEngine.UI;
using Vault;

public class AppEvents
{
  
}
public struct SpawnAiCharactersEvent : GameEvent
{

}

public struct MovePlayerEvent : GameEvent
{

}

public struct OnPlayerCollisionEvent : GameEvent
{
    public GameObject CollidedObject;

    public OnPlayerCollisionEvent(GameObject collidedObject)
    {
        CollidedObject = collidedObject;

    }
}
public struct OnPlayerExitCollider : GameEvent
{
    public GameObject CollidedObject;

    public OnPlayerExitCollider(GameObject collidedObject)
    {
        CollidedObject = collidedObject;
    }
}
public struct OnPlayerTriggedEvent : GameEvent
{
    public GameObject TriggedObject;

    public OnPlayerTriggedEvent(GameObject triggedObject)
    {
        TriggedObject = triggedObject;
    }
}

public struct FillKeySilderEvent : GameEvent
{
    public Slider KeySlider;
    public bool RoomAvailable;
    public Action<bool> WaitingCompleted;

    public FillKeySilderEvent(Slider keySlider, bool roomAvailable, Action<bool> waitingCompleted)
    {
        KeySlider = keySlider;
        RoomAvailable = roomAvailable;
        WaitingCompleted = waitingCompleted;
    }
}

public struct GoToRoomEvent : GameEvent
{

}

public struct RoomFilledEvent : GameEvent
{
    public GameObject collision;


    public RoomFilledEvent(GameObject collision)
    {
        this.collision = collision;
 
    }
}

public struct RoomEmptiedEvent : GameEvent
{
    public GameObject collision;
    public Customer CurrentCustomer;

    public RoomEmptiedEvent(GameObject collision, Customer currentCustomer)
    {
        this.collision = collision;
        CurrentCustomer = currentCustomer;
    }
}

public struct VacateRoomEvent : GameEvent
{
    public Customer CustomerInRoom;
    public GameObject Collision;

    public VacateRoomEvent(Customer customerInRoom, GameObject collision)
    {
        CustomerInRoom = customerInRoom;
        Collision = collision;
    }
}

public struct SpawnMoneyEvent : GameEvent
{

}
public struct PlaceEarnedMoneyEvent : GameEvent
{

}
public struct CollectMoneyEvent : GameEvent
{
    public GameObject MoneyObject;

    public CollectMoneyEvent(GameObject moneyObject)
    {
        MoneyObject = moneyObject;
    }
}

public struct StartUiImplimentation : GameEvent
{

}
public struct CleanRoomEvent : GameEvent
{
    public GameObject Collision;

    public CleanRoomEvent(GameObject collision)
    {
        Collision = collision;
    }
}
public struct CustomerRequeuEvent : GameEvent
{
    public GameObject Collision;
    public Customer CustomerTriggered;

    public CustomerRequeuEvent(GameObject collision, Customer customerTriggered)
    {
        Collision = collision;
        CustomerTriggered = customerTriggered;
    }
}
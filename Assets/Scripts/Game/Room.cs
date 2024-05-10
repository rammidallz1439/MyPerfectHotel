using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public bool isFilled;
    public int RoomNumber;
    public List<Transform> MoneyPoses;
    public List<Slider> CleanSliders;
    public int CleanedCount;
}

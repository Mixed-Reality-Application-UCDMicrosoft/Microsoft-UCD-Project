using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeRoom : MonoBehaviour
{
    public GameObject[] Rooms;
    int roomId = 0;
    string[] subs;
    string roomName = " ";

    public void Next()
    {
        Rooms[roomId].SetActive(false);

        roomId++;
        if (roomId == Rooms.Length)
            roomId = 0;

        Rooms[roomId].SetActive(true);


        roomName = Rooms[roomId].name; 
        subs = roomName.Split('_');

        TMP_Text nameText = GetComponent<TMP_Text>();
        nameText.text = subs[0];
        //Debug.Log(subs[0]);
    }

    public void Prev()
    {
        Rooms[roomId].SetActive(false);

        roomId--;
        if (roomId == -1)
            roomId = Rooms.Length-1;

        Rooms[roomId].SetActive(true);


        roomName = Rooms[roomId].name;
        subs = roomName.Split('_');

        TMP_Text nameText = GetComponent<TMP_Text>();
        nameText.text = subs[0];
        //Debug.Log(Rooms[roomId]);
    }

}

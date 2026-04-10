using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class MovingRequest
{
    public int id, x, y;
}

public class SentData : MonoBehaviour
{
    public List<MovingRequest> data = new List<MovingRequest>();

    public void AddRequest(int _id, int _x, int _y)
    {
        data.Add(new MovingRequest { id = _id, x = _x, y = _y }) ;
    }

    public void Clear()
    {
        data.Clear();
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }
}

using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class IntStringPair
{
    public int id;
    public string move;
}

public class SentData : MonoBehaviour
{
    public List<IntStringPair> data = new List<IntStringPair>();

    public void AddPair(int _id, string _move)
    {
        data.Add(new IntStringPair { id = _id, move = _move });
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

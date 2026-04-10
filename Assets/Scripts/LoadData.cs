using System;
using System.Collections.Generic;
using UnityEngine;

public class LoadData : MonoBehaviour
{
    [Serializable]
    public class MovingUpdate
    {
        public int id, x, y;
    }

    public List<MovingUpdate> data = new List<MovingUpdate>();
    //public List<IntStringPair> data = new List<IntStringPair>();

    public void Clear()
    {
        data.Clear();
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }
}
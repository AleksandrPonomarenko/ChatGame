using System.Collections.Generic;
using UnityEngine;

public class LoadData : MonoBehaviour
{
    public List<IntStringPair> data = new List<IntStringPair>();

    public void Clear()
    {
        data.Clear();
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }
}
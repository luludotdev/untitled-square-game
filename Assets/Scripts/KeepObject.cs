using System;
using UnityEngine;

public class KeepObject : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this);
    }
}

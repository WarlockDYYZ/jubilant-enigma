using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISystemController : MonoBehaviour
{
    public static UISystemController instance { get; private set; }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}

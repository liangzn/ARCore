using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base manager with singleton.
/// </summary>
public class BaseManager<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));

                if (instance == null)
                {
                    Debug.LogError(string.Format("An instance of {0} is needed in the scene, but there is none.", typeof(T)));
                }
            }

            return instance;
        }
    }
}
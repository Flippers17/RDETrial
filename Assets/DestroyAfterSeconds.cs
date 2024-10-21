using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    [SerializeField]
    private float _seconds = 3f;

    [SerializeField]
    private bool _destroyOnAwake = true;

    private void Awake()
    {
        if(_destroyOnAwake)
            Destroy(gameObject, _seconds);
    }


}

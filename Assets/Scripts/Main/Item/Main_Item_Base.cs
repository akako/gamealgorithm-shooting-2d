using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
abstract public class Main_Item_Base : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Affect();
        Destroy(gameObject);
    }

    abstract public void Affect();
}

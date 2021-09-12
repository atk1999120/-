using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider hit)
    {
       if(hit.CompareTag("player"))
        {
            GameObject image_object = GameObject.Find("Image")
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCameraControl : MonoBehaviour
{
    public GameObject cameraobject;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Vector3 newPosition = this.transform.position + new Vector3(6f, -0.5f, -10f);
            cameraobject.transform.position = newPosition;
        }
    }
}

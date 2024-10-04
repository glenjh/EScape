using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class recordplayerinteraction : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject playerObject;
    public Player player;
    public scenefade scene;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerObject = other.gameObject;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (playerObject != null)
        {
            playerObject = null;
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (playerObject != null)
            {
                
                scene.FadeTo("Inventory");
            }
        }
    }
}

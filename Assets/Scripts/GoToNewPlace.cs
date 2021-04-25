using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNewPlace : MonoBehaviour
{
    public string newPlaceName = "New Scene Name Here!!!";
    public bool needsClick = false;

    public string uuid;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (!needsClick )
            {
                FindObjectOfType<PlayerController>().nextUuid = uuid;
                SceneManager.LoadScene(newPlaceName);
            }
            
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name =="Player")
        {
            if (needsClick && Input.GetMouseButtonDown(0))
            {
                FindObjectOfType<PlayerController>().nextUuid = uuid;
                SceneManager.LoadScene(newPlaceName);
            }
        }
    }
}

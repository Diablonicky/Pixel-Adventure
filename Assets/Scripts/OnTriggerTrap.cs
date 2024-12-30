using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerTrap : MonoBehaviour
{
    public GameObject trapGround;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(trapGround != null)
            {
                Destroy(trapGround);
            }
            
        } 
    }
}

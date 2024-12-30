using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlatform : MonoBehaviour
{
    [SerializeField] private GameObject rotatePlatform;
    [SerializeField] private float delay = 1f;
    private bool hasRotated = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(rotatePlatform != null && !hasRotated)
            {
               StartCoroutine(RotateWithDelay());
            }
        }
    }

    private IEnumerator RotateWithDelay()
    {
        hasRotated = true;
        yield return new WaitForSeconds(delay);
        rotatePlatform.transform.Rotate(0,0,90);
    }
}

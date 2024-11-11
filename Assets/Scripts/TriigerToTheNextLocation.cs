using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriigerToTheNextLocation : MonoBehaviour
{
    public GameObject[] objectsHide;
    public GameObject[] objectsActive;
    public string TagPlayer;

    private BoxCollider2D boxCollider;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(TagPlayer))
        {
            foreach (GameObject obj in objectsHide)
            {
                obj.SetActive(false);
            }

            foreach (GameObject obj in objectsActive)
            {
                obj.SetActive(true);
            }
        }
    }
}

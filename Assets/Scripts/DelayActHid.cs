using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayActHid : MonoBehaviour
{
    public GameObject[] objectsHide;
    public GameObject[] objectsActive;
    public string TagPlayer;
    public float delayToActivate;
    public float delayToHide;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(TagPlayer))
        {
            StartCoroutine(ActivateObjects());
            StartCoroutine(HideObjectsWithDelay());
        }
    }

    IEnumerator ActivateObjects()
    {
        yield return new WaitForSeconds(delayToActivate);

        foreach (GameObject obj in objectsActive)
        {
            obj.SetActive(true);
        }
    }

    IEnumerator HideObjectsWithDelay()
    {
        yield return new WaitForSeconds(delayToHide);

        foreach (GameObject obj in objectsHide)
        {
            obj.SetActive(false);
        }
    }
}

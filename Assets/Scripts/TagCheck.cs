using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagCheck : MonoBehaviour
{
    [Header("Tag check")]
    public string targetTag;
    public int requiredCount = 3;
    public GameObject objectToActivate;

    void Update()
    {
        int activeCount = CountActiveObjectsWithTag(targetTag);

        if (activeCount >= requiredCount)
        {
            objectToActivate.SetActive(true);
        }
        else
        {
            objectToActivate.SetActive(false);
        }
    }

    private int CountActiveObjectsWithTag(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        int count = 0;

        foreach (GameObject obj in objects)
        {
            if (obj.activeInHierarchy)
            {
                count++;
            }
        }

        return count;
    }
}

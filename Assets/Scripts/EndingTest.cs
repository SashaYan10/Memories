using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingTest : MonoBehaviour
{
    [Header("Objects for check")]
    public GameObject[] objectsToCheck;
    public GameObject pastObject;
    public GameObject futureObject;

    private HashSet<GameObject> countedObjects = new HashSet<GameObject>();

    void Update()
    {
        int currentPastCount = ObjectCounter.Instance.pastCount;
        int currentFutureCount = ObjectCounter.Instance.futureCount;

        foreach (GameObject obj in objectsToCheck)
        {
            if (obj.activeInHierarchy && !countedObjects.Contains(obj))
            {
                if (obj.CompareTag("Past1") || obj.CompareTag("Past2") || obj.CompareTag("Past3") || obj.CompareTag("ExtraP"))
                {
                    ObjectCounter.Instance.AddPastCount();
                    countedObjects.Add(obj);
                }
                else if (obj.CompareTag("Future1") || obj.CompareTag("Future2") || obj.CompareTag("Future3") || obj.CompareTag("ExtraF"))
                {
                    ObjectCounter.Instance.AddFutureCount();
                    countedObjects.Add(obj);
                }
            }
        }

        if (currentPastCount + currentFutureCount >= 9)
        {
            if (currentPastCount > currentFutureCount)
            {
                pastObject.SetActive(true);
                futureObject.SetActive(false);
            }
            else if (currentFutureCount > currentPastCount)
            {
                futureObject.SetActive(true);
                pastObject.SetActive(false);
            }
        }
    }
}
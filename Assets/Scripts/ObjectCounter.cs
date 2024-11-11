using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectCounter : MonoBehaviour
{
    public static ObjectCounter Instance;

    public int pastCount = 0;
    public int futureCount = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            ResetCounts();
        }
    }

    public void ResetCounts()
    {
        pastCount = 0;
        futureCount = 0;
    }

    public void AddPastCount()
    {
        pastCount++;
    }

    public void AddFutureCount()
    {
        futureCount++;
    }
}

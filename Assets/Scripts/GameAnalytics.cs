using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameAnalytics : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameAnalytics Instance;
    private string sessionID;
    private int highestLevelReached;
    private Dictionary<int, int> levelAttempts = new Dictionary<int, int>(); 
    void Awake()
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

    void Start()
    {
        sessionID = Guid.NewGuid().ToString();
        Debug.Log("Session ID: " + sessionID);
    }

    public void LevelStarted(int level)
    {
        if (!levelAttempts.ContainsKey(level))
        {
            levelAttempts[level] = 0;
        }
        levelAttempts[level]++;
        Debug.Log($"Level {level} started. Attempts: {levelAttempts[level]}");

        if (level > highestLevelReached)
        {
            highestLevelReached = level;
        }
    }

    public void LevelCompleted(int level)
    {
        Debug.Log($"Level {level} completed in {levelAttempts[level]} attempts.");
    }

    public void EndSession()
    {
        Debug.Log($"Session {sessionID} ended. Highest Level Reached: {highestLevelReached}");
        foreach (var entry in levelAttempts)
        {
            Debug.Log($"Level {entry.Key}: {entry.Value} attempts.");
        }
        // Here you would send data to a server or save it locally
    }
}

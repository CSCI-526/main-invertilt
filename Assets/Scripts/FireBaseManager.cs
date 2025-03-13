using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance;
    // private string databaseURL = "https://invertilt-default-rtdb.firebaseio.com"; // Replace with your Firebase URL
    private string databaseURL = "https://invertilt-default-rtdb.firebaseio.com/game_sessions.json";

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
            return;
        }

        // Ensure FirebaseManager is initialized
        if (FirebaseManager.Instance == null)
        {
            Debug.LogError("🚨 FirebaseManager is not initialized yet!");
        }
        else{
            Debug.Log("FirebaseManager is initialized!");
        }
    }
    public void SendAnalyticsData(string sessionID, int highestLevel, Dictionary<int, int> levelAttempts)
    {
        PlayerSession data = new PlayerSession(sessionID, highestLevel, levelAttempts);
        Debug.Log("Sending data to Firebase:  " + data.levelAttempts);
        string jsonData = JsonUtility.ToJson(data);

        StartCoroutine(SendDataToFirebase(jsonData));
    }

    private IEnumerator SendDataToFirebase(string jsonData)
    {
        using (UnityWebRequest request = new UnityWebRequest(databaseURL, "POST"))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Data sent successfully: " + request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error sending data: " + request.error);
            }
        }
    }
}

// [Serializable]
// public class PlayerSession
// {
//     public string sessionID;
//     public int highestLevel;
//     public List<KeyValuePair<int, int>> levelAttempts;  // Changed from Dictionary to List

//     public PlayerSession(string id, int level, Dictionary<int, int> attempts)
//     {
//         sessionID = id;
//         highestLevel = level;
//         levelAttempts = new List<KeyValuePair<int, int>>(attempts); // Convert Dictionary to List
//     }
// }

[Serializable]
public class PlayerSession
{
    public string sessionID;
    public int highestLevel;
    public DictionaryWrapper levelAttempts;  // Use a wrapper class

    public PlayerSession(string id, int level, Dictionary<int, int> attempts)
    {
        sessionID = id;
        highestLevel = level;
        levelAttempts = new DictionaryWrapper(attempts); // Convert Dictionary
    }
}

[Serializable]
public class DictionaryWrapper
{
    public List<DictionaryEntry> entries = new List<DictionaryEntry>();

    public DictionaryWrapper(Dictionary<int, int> dictionary)
    {
        foreach (var kvp in dictionary)
        {
            entries.Add(new DictionaryEntry(kvp.Key, kvp.Value));
        }
    }
}

[Serializable]
public class DictionaryEntry
{
    public int key;
    public int value;

    public DictionaryEntry(int key, int value)
    {
        this.key = key;
        this.value = value;
    }
}
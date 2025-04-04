using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlashingText : MonoBehaviour
{
    public float displayDuration = 3f; // Duration the text stays visible
    public float fadeSpeed = 2f;       // Speed of fade in/out
    private TextMeshProUGUI text;
    private CanvasGroup canvasGroup;

    // Dictionary to store messages for each scene
    private readonly Dictionary<string, string> sceneMessages = new Dictionary<string, string>
    {
        {"Tut-1", "Tutorial"},
        {"Tut-2", "Tutorial"},
        {"Tut-3", "Tutorial"},
        {"Play-1", "Try Yourself"},
        {"Play-2", "Try Yourself"},
        {"Play-3", "Try Yourself"},
        { "Level-1", "'G' for gravity \n'A' & 'D' for spin" },
        { "Level-2", "Navigate with wind!" },
        { "Level-3", "Motion on yellow sandpaper!" },
        { "Level-4", "Think before you move!" },
        { "Level-5", "Portals & Black hole!" }
    };

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        // Ensure the object has a CanvasGroup for fade effects
        canvasGroup = text.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = text.gameObject.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0; // Ensure text starts invisible

        string sceneName = SceneManager.GetActiveScene().name; // Get current scene name

        if (sceneMessages.ContainsKey(sceneName))
        {
            text.text = sceneMessages[sceneName]; // Assign the corresponding message
        }
        else
        {
            text.text = "Get Ready!"; // Default message if scene name not found
        }

        StartCoroutine(ShowAndFadeText()); // Start flashing effect
    }

    IEnumerator ShowAndFadeText()
    {
        // Fade In
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * fadeSpeed;
            canvasGroup.alpha = Mathf.Lerp(0, 1, t);
            yield return null;
        }

        yield return new WaitForSeconds(displayDuration); // Keep text visible

        // Fade Out
        t = 1;
        while (t > 0)
        {
            t -= Time.deltaTime * fadeSpeed;
            canvasGroup.alpha = Mathf.Lerp(0, 1, t);
            yield return null;
        }

        gameObject.SetActive(false); // Disable after fading out
    }
}

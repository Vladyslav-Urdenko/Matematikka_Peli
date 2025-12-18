using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TypeWriterScript : MonoBehaviour
{
    [Header("Text Settings")]
    public TextMeshProUGUI textUI;

    [TextArea(3, 10)]
    public string mainText;

    [TextArea(1, 3)]
    public string warningText = "Hei, älä nuku!!";

    public float letterDelay = 0.05f;

    [Header("Inactivity Settings")]
    public float warningAfterSeconds = 10f;
    public float hideAfterSeconds = 15f;

    private float inactivityTimer;
    private Coroutine typingCoroutine;
    private bool warningShown;

    void Start()
    {
        StartTyping(mainText);
    }

    void Update()
    {
        // NEW Input System detection
        if ((Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame) ||
            (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame) ||
            (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame))
        {
            inactivityTimer = 0f;
            warningShown = false;

            // Restart main text if player interacts
            StartTyping(mainText);
        }

        inactivityTimer += Time.deltaTime;

        // Show warning text after 10 seconds
        if (inactivityTimer >= warningAfterSeconds && !warningShown)
        {
            warningShown = true;
            StartTyping(warningText);

            // Return to main text after warning finishes
            StartCoroutine(ReturnToMainText());
        }

        // Hide everything after 15 seconds
        if (inactivityTimer >= hideAfterSeconds)
        {
            HideText();
        }
    }

    void StartTyping(string textToType)
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        textUI.gameObject.SetActive(true);
        textUI.text = "";

        typingCoroutine = StartCoroutine(TypeText(textToType));
    }

    IEnumerator TypeText(string textToType)
    {
        foreach (char c in textToType)
        {
            textUI.text += c;
            yield return new WaitForSeconds(letterDelay);
        }
    }

    IEnumerator ReturnToMainText()
    {
        // Wait until warning text is fully shown + short pause
        yield return new WaitForSeconds(warningText.Length * letterDelay + 1f);

        if (inactivityTimer < hideAfterSeconds)
        {
            StartTyping(mainText);
        }
    }

    void HideText()
    {
        textUI.gameObject.SetActive(false);
    }
}

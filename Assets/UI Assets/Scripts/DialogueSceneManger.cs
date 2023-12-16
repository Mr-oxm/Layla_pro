using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueSceneManger : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    public float delayBeforeNextSentence;
    private bool isTyping = false;
    public bool FinshedTyping = false;

    public float delayBeforeStart=20;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("startAction", delayBeforeStart);
    }

    void startAction(){
        StartCoroutine(StartDialogue());
    }
    IEnumerator StartDialogue()
    {
        foreach (string sentence in sentences)
        {
            yield return StartCoroutine(TypeSentence(sentence));
            yield return new WaitForSeconds(delayBeforeNextSentence);
            textDisplay.text = ""; // Clear the text before typing the next sentence
        }

        // Do whatever you want when the dialogue is finished
        FinshedTyping = true;
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        foreach (char letter in sentence.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }
}
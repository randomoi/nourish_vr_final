using UnityEngine;

// START - The code was created using documentation and additional external research materials. Please review the referenced links.

// handles issues with pop questions overlap
public class QuestionSequenceManager : MonoBehaviour
{
    public static QuestionSequenceManager Instance;

    public GameObject questionOneModal;
    public GameObject questionTwoModal;

    private GameObject activeQuestion = null;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // handles question displaying
    public void DisplayQuestion(GameObject questionCanvas)
    {
        if (activeQuestion != null)
        {
            activeQuestion.SetActive(false); // hides active question
        }
        activeQuestion = questionCanvas;
        activeQuestion.SetActive(true); // displays new question
    }

    // handles hiding of question
    public void HideActiveQuestion()
    {
        if (activeQuestion != null)
        {
            activeQuestion.SetActive(false);
            activeQuestion = null;
        }
    }

    // checks if there is an active question
    public bool IsQuestionActive()
    {
        return activeQuestion != null;
    }
}

// END - The code was created using documentation and additional external research materials. Please review the referenced links.

// References:
//https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
//https://docs.unity3d.com/ScriptReference/Object.Destroy.html
//https://forum.unity.com/threads/instance-what-does-it-mean.570511/
//https://community.gamedev.tv/t/difference-between-keyword-this-and-gameobject/67951


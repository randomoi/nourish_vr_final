using UnityEngine;
using UnityEngine.UI;
using TMPro;

// START - The code was created using documentation and additional external research materials. Please review the referenced links.

public class QuestionOnePopupMessage : MonoBehaviour
{
    public GameObject questionOne;
    public Button yesButton;
    public Button noButton;
    public TextMeshProUGUI questionText;

    public GameObject questionTwo;
    public DisplayGoInteractMessage displayGoInteractMessage; 


    void Start()
    {
        yesButton.onClick.AddListener(OnYesButtonClicked);
        noButton.onClick.AddListener(OnNoButtonClicked);
        questionOne.SetActive(false); // hides question one
    }

    public void DisplayModal()
    {
        if (!QuestionSequenceManager.Instance.IsQuestionActive())
        {
            QuestionSequenceManager.Instance.DisplayQuestion(questionOne);
        }
    
    }

    // handles when yes is clicked (shows second question)
    private void OnYesButtonClicked()
    {
        questionOne.SetActive(false);
        QuestionSequenceManager.Instance.HideActiveQuestion();
        questionTwo.SetActive(true);

    }
    // handles when no is clicked (shows message to interact with food items)
    private void OnNoButtonClicked()
    {
        questionOne.SetActive(false);
        QuestionSequenceManager.Instance.HideActiveQuestion();

        if (displayGoInteractMessage != null)
            displayGoInteractMessage.ShowGoInteractMessage(); // calls DisplayGoInteractMessage to show message
        else
            Debug.LogError("DisplayGoInteractMessage is not assigned in QuestionOnePopupMessage");
    }

}


// END - The code was created using documentation and additional external research materials. Please review the referenced links.

// References:
//https://docs.unity3d.com/Packages/com.unity.ugui@2.0/api/TMPro.TextMeshProUGUI.html
//https://docs.unity3d.com/2018.2/Documentation/ScriptReference/UI.Button.html
//https://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
//https://docs.unity3d.com/2018.3/Documentation/ScriptReference/UI.Button-onClick.html
//https://docs.unity3d.com/ScriptReference/GameObject.SetActive.html
//https://forum.unity.com/threads/instance-what-does-it-mean.570511/
//https://docs.unity3d.com/ScriptReference/Debug.LogError.html
//https://discussions.unity.com/t/button-onclick-addlistener-not-working-solved/222213
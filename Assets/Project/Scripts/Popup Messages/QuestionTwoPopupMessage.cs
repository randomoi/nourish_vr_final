using UnityEngine;
using UnityEngine.UI;
using TMPro;

// START - The code was created using documentation and additional external research materials. Please review the referenced links.

public class QuestionTwoPopupMessage : MonoBehaviour
{
    // to be assigned in Unity inspector
    public GameObject questionTwo;
    public Button yesButton;
    public Button noButton;
    public TextMeshProUGUI questionText;

    public BreathingExerciseController breathingExercise;
    public GameObject goInteract;

    public DisplayGoInteractMessage displayGoInteractMessage; 
    private Transform cameraTransform;

    // handles hiding modal, cam and event listeners on start
    void Start()
    {
        cameraTransform = Camera.main.transform;
        yesButton.onClick.AddListener(OnYesButtonClicked);
        noButton.onClick.AddListener(OnNoButtonClicked);
        questionTwo.SetActive(false); // hides modal 
    }

    // handles showing of modal
    public void ShowModal()
    {
        if (!QuestionSequenceManager.Instance.IsQuestionActive())
        {
            PanelPositionInFrontOfCamera(questionTwo); 
            questionTwo.SetActive(true);
            QuestionSequenceManager.Instance.DisplayQuestion(questionTwo);
        }

    }

    // handles yes button clicks
    private void OnYesButtonClicked()
    {
        questionTwo.SetActive(false);
        QuestionSequenceManager.Instance.HideActiveQuestion();
        if (breathingExercise != null)
            breathingExercise.StartBreathingExercise();
    }

    // handles no button clicks
    private void OnNoButtonClicked()
    {
        questionTwo.SetActive(false);
        QuestionSequenceManager.Instance.HideActiveQuestion();
        if (displayGoInteractMessage != null)
            displayGoInteractMessage.ShowGoInteractMessage();
        else
            Debug.LogError("Not assigned");
    }

    // handles panel position
    private void PanelPositionInFrontOfCamera(GameObject panel)
    {
        if (cameraTransform == null) return;

        float modalDistanceToCamera = 2.0f; // distance to camera
        float offsetRight = 0.5f; // right offset
        float offsetHeight = 0.15f; // height offset

        Vector3 cameraPosition = cameraTransform.position;
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0; // parallel to floor
        cameraForward.Normalize();

        Vector3 cameraRight = cameraTransform.right;
        Vector3 moveRightward = cameraRight * offsetRight; // moves to right
        Vector3 moveUpward = Vector3.up * offsetHeight; // moves up

        Vector3 updatedPosition = cameraPosition + cameraForward * modalDistanceToCamera + moveRightward + moveUpward;
        panel.transform.position = updatedPosition;

        panel.transform.LookAt(cameraPosition);
        panel.transform.rotation = Quaternion.LookRotation(panel.transform.position - cameraPosition, Vector3.up);
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
//https://docs.unity3d.com/ScriptReference/Transform-position.html
//https://forum.unity.com/threads/how-to-make-object-match-position-with-camera.918788/
//https://forum.unity.com/threads/c-change-camera-target-object-problem.151368/
//https://discussions.unity.com/t/my-camera-stays-in-the-same-position-regardless-of-any-variables-i-change/65034
//https://forum.unity.com/threads/camera-get-position-vector3-solved.59051/
//https://docs.unity3d.com/ScriptReference/Transform.LookAt.html
//https://discussions.unity.com/t/question-regarding-to-transform-lookat/251234
//https://stackoverflow.com/questions/59039958/how-can-a-get-the-camera-to-move-smoothly-to-a-new-vector3-position-on-the-click
//https://discussions.unity.com/t/how-do-i-make-the-camera-follow-a-vector3-3d/223403
//https://discourse.threejs.org/t/camera-transform-from-unity-to-three/18903
//https://docs.unity3d.com/ScriptReference/Camera.WorldToScreenPoint.html
//https://docs.unity3d.com/ScriptReference/Camera.ViewportToWorldPoint.html
//https://stackoverflow.com/questions/75984333/bring-gameobject-to-the-position-of-camera
//https://docs.unity3d.com/ScriptReference/Quaternion-eulerAngles.html
//https://discussions.unity.com/t/how-does-one-use-quaternion-euler-x-y-z-to-rotate-on-y-axis/106681
//https://forum.unity.com/threads/quaternions-and-particle-collisions.1084151/
//https://docs.unity3d.com/ScriptReference/Quaternion.Euler.html
//https://forum.unity.com/threads/convert-position-from-one-cameras-perspective-to-another.1387248/
//https://docs.unity3d.com/ScriptReference/Quaternion.html
//https://docs.unity3d.com/ScriptReference/Quaternion.LookRotation.html
//https://forum.unity.com/threads/how-to-make-object-match-position-with-camera.918788/
//https://docs.unity3d.com/ScriptReference/Vector3.html
//https://docs.unity3d.com/ScriptReference/Transform.IsChildOf.html
//https://docs.unity3d.com/ScriptReference/Transform.SetParent.html
//https://docs.unity3d.com/ScriptReference/Quaternion.Euler.html
//https://docs.unity3d.com/ScriptReference/Vector3.Normalize.html
//https://docs.unity3d.com/ScriptReference/Transform.LookAt.html

using System.Collections;
using UnityEngine;
using TMPro;

public class KPNmanager : MonoBehaviour
{
    public GameObject rockMesh;
    public GameObject paperMesh;
    public GameObject scissorsMesh;

    public GameObject leftHand;
    public GameObject rightHand;

    public TextMeshProUGUI winText;
    public TextMeshProUGUI lossText;
    public TextMeshProUGUI drawText;

    public float initialSpeed = 1f;
    public float secondSpeed = 0.5f;
    public float thirdSpeed = 0.25f;
    public float finalSpeed = 0.1f;

    private float currentSpeed;
    private string currentGesture;
    private string playerGesture;
    private int gestureCount = 0;
    private bool isPlaying = false;
    private Coroutine rotationCoroutine;

    void Start()
    {
        InitializeGame();
    }

    void Update()
    {
        // Check if thumbs-up gesture is detected
        if (!isPlaying && (playerGesture == "ThumbsUpLeft" || playerGesture == "ThumbsUpRight"))
        {
            Debug.Log("Thumbs-Up Gesture Detected: Restarting Game");
            RestartGame();
        }
    }

    void InitializeGame()
    {
        DeactivateAllGestureMeshes();
        ResetResultText();
        currentSpeed = initialSpeed;
        gestureCount = 0;
        isPlaying = true;
        rotationCoroutine = StartCoroutine(RotateHandMeshes());
    }

    void RestartGame()
    {
        Debug.Log("RestartGame method triggered.");
        if (rotationCoroutine != null)
        {
            StopCoroutine(rotationCoroutine);
        }

        playerGesture = null;
        currentGesture = null;
        currentSpeed = initialSpeed;
        gestureCount = 0;
        isPlaying = true;

        ResetResultText();
        DeactivateAllGestureMeshes();
        rotationCoroutine = StartCoroutine(RotateHandMeshes());
    }

    IEnumerator RotateHandMeshes()
    {
        string[] gestures = { "Rock", "Paper", "Scissors" };

        while (isPlaying)
        {
            foreach (string gesture in gestures)
            {
                DeactivateAllGestureMeshes();

                if (gesture == "Rock") rockMesh.SetActive(true);
                if (gesture == "Paper") paperMesh.SetActive(true);
                if (gesture == "Scissors") scissorsMesh.SetActive(true);

                currentGesture = gesture;
                yield return new WaitForSeconds(currentSpeed);
            }
        }
    }

    public void UpdatePlayerGesture(string gesture)
    {
        Debug.Log("Player Gesture Detected in Script: " + gesture);
        playerGesture = gesture;

        if (!isPlaying) return;

        gestureCount++;

        if (gestureCount == 1)
        {
            currentSpeed = secondSpeed;
        }
        else if (gestureCount == 2)
        {
            currentSpeed = thirdSpeed;
        }
        else if (gestureCount == 3)
        {
            currentSpeed = finalSpeed;
        }
        else if (gestureCount == 4)
        {
            isPlaying = false;
            StopCoroutine(rotationCoroutine);
            DetermineWinner();
        }
    }

    void DetermineWinner()
    {
        ResetResultText();

        if (playerGesture == currentGesture)
        {
            drawText.gameObject.SetActive(true);
        }
        else if (playerGesture == "Rock" && currentGesture == "Scissors" ||
                 playerGesture == "Paper" && currentGesture == "Rock" ||
                 playerGesture == "Scissors" && currentGesture == "Paper")
        {
            winText.gameObject.SetActive(true);
        }
        else
        {
            lossText.gameObject.SetActive(true);
        }
    }

    void DeactivateAllGestureMeshes()
    {
        rockMesh.SetActive(false);
        paperMesh.SetActive(false);
        scissorsMesh.SetActive(false);
    }

    void ResetResultText()
    {
        winText.gameObject.SetActive(false);
        lossText.gameObject.SetActive(false);
        drawText.gameObject.SetActive(false);
    }
}

using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using Oculus.Interaction;
using UnityEngine;

public class KeypadColorSetter : MonoBehaviour
{
    public Transform parentObject; // Assign the parent object (Keypad Variant) in the Inspector
    public Color startColor = Color.blue;
    public Color endColor = Color.cyan;
        public float fixedHue = 0.5f; // Fixed hue (e.g., 0.5 = cyan)
    public float fixedSaturation = 1.0f; // Fixed saturation
    public int colorPoolSize = 6; // Number of colors in the pool
        public Color[] colorPool; // Pool of colors to assign (must be at least 6)
            private List<ButtonData> buttonsData = new List<ButtonData>();
                private int currentClickIndex = 0;

    [SerializeField]
    private EventReference fmodEventReference; // Replace string with EventReference

    private EventInstance eventInstance;
    public StudioGlobalParameterTrigger studioParameterTrigger;

    void Start()
    {
        studioParameterTrigger = GetComponent<StudioGlobalParameterTrigger>();
        // Create an instance of the FMOD event
        eventInstance = RuntimeManager.CreateInstance(fmodEventReference);
        eventInstance.start(); // Start the event
    }

    public void SetMITransition(float value)
    {
        // Set the MI_TRANSITION parameter
//        eventInstance.setParameterByName("MI_TRANSITION", value);
FMODUnity.RuntimeManager.StudioSystem.setParameterByName("MI_TRANSITION", value);
    }

    private void OnDestroy()
    {
        // Stop and release the event instance
        eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        eventInstance.release();
    }

    // Function to shuffle the color array
    private Color[] ShuffleColors(Color[] colors)
    {
        Color[] shuffled = (Color[])colors.Clone(); // Clone to avoid modifying the original pool
        for (int i = shuffled.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Color temp = shuffled[i];
            shuffled[i] = shuffled[randomIndex];
            shuffled[randomIndex] = temp;
        }
        return shuffled;
    }

        // Generate a pool of colors with the same hue but different brightness
    private Color[] GenerateColorPool(float hue, float saturation, int poolSize)
    {
        Color[] colors = new Color[poolSize];
        float step = 1f / (poolSize - 1); // Divide value range into equal steps

        for (int i = 0; i < poolSize; i++)
        {
            float value = i * step; // Gradually increase brightness
            colors[i] = Color.HSVToRGB(hue, saturation, value);
        }

        return colors;
    }

    // Function to generate a random value with steps
    private float GetSteppedRandomValue(float stepSize)
    {
        int steps = Mathf.RoundToInt(1f / stepSize); // Calculate the number of steps
        int randomStep = Random.Range(0, steps + 1); // Randomize within the step range
        return randomStep * stepSize; // Scale back to 0-1 range
    }
        void OnButtonClicked(Transform clickedButton)
    {
        if (currentClickIndex >= buttonsData.Count)
        {
            Debug.Log("All buttons have been clicked!");
            return;
        }

        // Check if the clicked button is the correct one in the sequence
        if (clickedButton == buttonsData[currentClickIndex].ButtonTransform)
        {
            Debug.Log("Correct button clicked!");
            currentClickIndex++;
        }
        else
        {
            Debug.Log("Wrong button clicked! Restarting sequence.");
            currentClickIndex = 0; // Reset the sequence
            Awake();
        }

        // Check if all buttons are clicked in the correct order
        if (currentClickIndex == buttonsData.Count)
        {
            Debug.Log("Sequence completed successfully!");
            SetMITransition(1);
            gameObject.SetActive(false);
        }
    }
    void Awake()
    {
        buttonsData.Clear();
                float randomHue = Random.Range(0f, 1f);

        Color[] colorPool = GenerateColorPool(randomHue, fixedSaturation, colorPoolSize);

        Color[] shuffledColors = ShuffleColors(colorPool);

        parentObject = transform;
        if (parentObject == null)
        {
            Debug.LogError("Parent object is not assigned!");
            return;
        }

        // Get all child KeypadButton objects dynamically
        var buttons = parentObject.GetComponentsInChildren<Transform>(true);
        int buttonCount = 0;

        // Count valid KeypadButtons
        foreach (var button in buttons)
        {
            if (button.name.Contains("KeypadButton"))
            {
                buttonCount++;
            }
        }

        int currentIndex = 0;
        foreach (var button in buttons)
        {
                              // Store the button and its brightness
            if (button.name.Contains("KeypadButton"))
            {
                // Calculate the gradient color
                float t = (float)currentIndex / (buttonCount - 1);
                //Color buttonColor = Color.Lerp(startColor, endColor, t);
                                // Generate a color with fixed hue but randomized saturation and/or value
                float fixedHue = 0.5f; // Fixed hue value (0 = red, 0.5 = cyan, etc.)
                //float saturation = false ? Random.Range(0.5f, 1f) : 1f; // Saturation range
                //float value = true? GetSteppedRandomValue(0.1f) : 1f; // Brightness range
                //Color buttonColor = Color.HSVToRGB(fixedHue, saturation, value);


                //Color assignedColor = shuffledColors[currentIndex];

                Color buttonColor = shuffledColors[currentIndex];

                // Find the ButtonVisual child and set its color
                Transform visuals = button.Find("Visuals/ButtonVisual");
                Debug.Log("color set");
                if (visuals != null)
                {
                Debug.Log("color set 2");

                var colorVisual = visuals.GetComponent<InteractableColorVisual>();
                var roundedBoxProperties = visuals.GetComponent<RoundedBoxProperties>();
                if (colorVisual != null)
                {
                Debug.Log("color set 3");
                    //colorVisual.ColoeS= buttonColor;
                    colorVisual.InjectOptionalNormalColorState(new InteractableColorVisual.ColorState() {Color = buttonColor});
                    roundedBoxProperties.Color = buttonColor;
                    roundedBoxProperties.BorderColor = buttonColor;
                    roundedBoxProperties.BorderOuterRadius = roundedBoxProperties.BorderOuterRadius;

                }

                    var renderer = visuals.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                 //       renderer.material.color = buttonColor;
                    }
                            // Create a MaterialPropertyBlock
//                     var targetRenderer = visuals.GetComponent<Renderer>();
//                     MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
// Color colorToSet = Color.green;
//                     // Get the current property block from the renderer
//                     targetRenderer.GetPropertyBlock(propertyBlock);

//                     // Set the color property (make sure to use the correct property name)
//                     propertyBlock.SetColor("_Color", colorToSet);

//                     // Apply the property block back to the renderer
//                     targetRenderer.SetPropertyBlock(propertyBlock);
                }
                    float hue, saturation, value;
                    Color.RGBToHSV(buttonColor, out hue, out saturation, out value);

                    buttonsData.Add(new ButtonData
                    {
                        ButtonTransform = button,
                        Brightness = value
                    });

                    // Add a click listener to the button
                    var interactable = button.GetComponent<InteractableUnityEventWrapper>();
                    if (interactable != null)
                    {
                        interactable.WhenSelect.RemoveAllListeners();
                        interactable.WhenSelect.AddListener(() => OnButtonClicked(button));
                    }

                currentIndex++;
            }

            buttonsData.Sort((a, b) => a.Brightness.CompareTo(b.Brightness));
        }
    }
}

internal class ButtonData
{
        public Transform ButtonTransform;
        public float Brightness;}
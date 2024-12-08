using UnityEngine;

public class KeypadColorSetter : MonoBehaviour
{
    public Transform parentObject; // Assign the parent object (Keypad Variant) in the Inspector
    public Color startColor = Color.blue;
    public Color endColor = Color.cyan;

    void Start()
    {
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
            if (button.name.Contains("KeypadButton"))
            {
                // Calculate the gradient color
                float t = (float)currentIndex / (buttonCount - 1);
                Color buttonColor = Color.Lerp(startColor, endColor, t);

                // Find the ButtonVisual child and set its color
                Transform visuals = button.Find("Model/Visuals/ButtonVisual");
                if (visuals != null)
                {
                    var renderer = visuals.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.material.color = buttonColor;
                    }
                }

                currentIndex++;
            }
        }
    }
}

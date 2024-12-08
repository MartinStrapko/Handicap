using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using System.Collections;

public class SphereShellOpener : MonoBehaviour
{
    [SerializeField]
    private Transform[] shellParts; // �asti obalu

    [SerializeField]
    private float targetRotationY = 0f; // Absol�tna cie�ov� hodnota rot�cie po osi Y

    [SerializeField]
    private int rotationDirection = 1; // Smer rot�cie: 1 pre pozit�vny smer, -1 pre negat�vny smer

    [SerializeField]
    private float rotationSpeed = 50f; // R�chlos� otv�rania (stupne za sekundu)

    [SerializeField]
    private float delayBetweenParts = 0.5f; // Oneskorenie medzi za�iatkami otv�rania jednotliv�ch �ast�

    private void Start()
    {
        OpenShell(); // Spust� proces otv�rania
    }

    private void OpenShell()
    {
        StartCoroutine(OpenShellSequence());
    }

    private IEnumerator OpenShellSequence()
    {
        for (int i = 0; i < shellParts.Length; i++)
        {
            StartCoroutine(RotatePartSmoothly(shellParts[i]));
            yield return new WaitForSeconds(delayBetweenParts);
        }
    }

    private IEnumerator RotatePartSmoothly(Transform part)
    {
        float currentRotationY = NormalizeAngle(part.localEulerAngles.y); // Normalizujeme aktu�lny uhol
        float targetRotation = targetRotationY;

        // Uprav�me cie�ov� hodnotu pod�a smeru
        if (rotationDirection > 0 && currentRotationY > targetRotation)
        {
            targetRotation += 360f; // Pozit�vny smer
        }
        else if (rotationDirection < 0 && currentRotationY < targetRotation)
        {
            currentRotationY -= 360f; // Negat�vny smer
        }

        while (Mathf.Abs(currentRotationY - targetRotation) > 0.1f)
        {
            float step = rotationSpeed * Time.deltaTime * rotationDirection;
            currentRotationY = Mathf.MoveTowards(currentRotationY, targetRotation, Mathf.Abs(step));
            part.localEulerAngles = new Vector3(part.localEulerAngles.x, currentRotationY, part.localEulerAngles.z);
            yield return null;
        }

        // Presne nastav�me cie�ov� uhol
        part.localEulerAngles = new Vector3(part.localEulerAngles.x, targetRotation, part.localEulerAngles.z);
    }

    private float NormalizeAngle(float angle)
    {
        return (angle % 360 + 360) % 360; // Normalizuje uhol na rozsah 0� - 360�
    }
}

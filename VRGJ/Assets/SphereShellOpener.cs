using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class SphereShellOpener : MonoBehaviour
{
    [SerializeField]
    private Transform[] shellParts; // �asti obalu (napr�klad jednotliv� �asti gule)

    [SerializeField]
    private Vector3 targetRotation = new Vector3(0f, 0f, 20f); // Cie�ov� rot�cia pre v�etky �asti

    [SerializeField]
    private float rotationSpeed = 50f; // R�chlos� otv�rania (stupne za sekundu)

    [SerializeField]
    private float delayBetweenParts = 0.5f; // Oneskorenie medzi otv�ran�m jednotliv�ch �ast� (v sekund�ch)

    private void Start()
    {
        OpenShell(); // Spust� otv�ranie hne� po �tarte
    }

    private void OpenShell()
    {
        StartCoroutine(OpenShellSequence());
    }

    private IEnumerator OpenShellSequence()
    {
        foreach (var part in shellParts)
        {
            yield return RotatePartSmoothly(part);
            yield return new WaitForSeconds(delayBetweenParts);
        }
    }

    private IEnumerator RotatePartSmoothly(Transform part)
    {
        Quaternion initialRotation = part.localRotation;
        Quaternion finalRotation = Quaternion.Euler(targetRotation);

        while (Quaternion.Angle(part.localRotation, finalRotation) > 0.1f)
        {
            part.localRotation = Quaternion.RotateTowards(part.localRotation, finalRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        // Zaru��me presn� nastavenie na cie�ov� rot�ciu
        part.localRotation = finalRotation;
    }
}

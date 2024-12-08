using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class SphereShellOpener : MonoBehaviour
{
    [SerializeField]
    private Transform[] shellParts; // Èasti obalu (napríklad jednotlivé èasti gule)

    [SerializeField]
    private Vector3 targetRotation = new Vector3(0f, 0f, 20f); // Cie¾ová rotácia pre všetky èasti

    [SerializeField]
    private float rotationSpeed = 50f; // Rýchlos otvárania (stupne za sekundu)

    [SerializeField]
    private float delayBetweenParts = 0.5f; // Oneskorenie medzi otváraním jednotlivých èastí (v sekundách)

    private void Start()
    {
        OpenShell(); // Spustí otváranie hneï po štarte
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

        // Zaruèíme presné nastavenie na cie¾ovú rotáciu
        part.localRotation = finalRotation;
    }
}

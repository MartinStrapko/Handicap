using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class PropellerRotation : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 100f; // R˝chlosù rot·cie v stupÚoch za sekundu

    [SerializeField]
    private Vector3 rotationAxis = Vector3.forward; // Predvolen· os rot·cie (Z os)

    void Update()
    {
        // Rot·cia vrtule okolo öpecifikovanej osi
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime, Space.Self);
    }
}


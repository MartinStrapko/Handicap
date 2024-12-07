using Oculus.Interaction;
using UnityEngine;

public class SphereRotation : MonoBehaviour
{
    private Rigidbody sphereRigidbody;
        private Grabbable grabbable;


    void Start()
    {
        sphereRigidbody = GetComponent<Rigidbody>();
                grabbable = GetComponent<Grabbable>();

        // Wire up the grab events
//        grabbable.+= OnGrabStart;
 //       grabbable.WhenReleased += OnGrabEnd;
    }

    public void OnGrabStart()
    {
        // Apply initial rotation based on hand movement
  //      sphereRigidbody.AddTorque(handMovement * 10f, ForceMode.VelocityChange);
    }

    public void OnGrabEnd()
    {
        // Let the sphere continue to rotate smoothly after release
   //     sphereRigidbody.angularVelocity *= 0.95f; // Fine-tune for inertia
    }
}
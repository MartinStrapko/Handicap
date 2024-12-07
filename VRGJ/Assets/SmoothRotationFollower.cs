using System.Collections;
using System.Linq;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class SmoothRotationFollower : MonoBehaviour
{
    public Transform target; // The object you rotate with hand interaction
    public Transform follower; // The object that smoothly follows the rotation
    public float smoothSpeed = 5f; // How quickly the follower catches up
    private  bool isInteracting;
    [SerializeField]
    public Grabbable _grabbable;
        public HandGrabInteractable _handGrabInteractable;


    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

void Start()
{
    _handGrabInteractable.WhenSelectingInteractorAdded.Action += OnGrabbed; 
    _handGrabInteractable.WhenSelectingInteractorRemoved.Action += OnReleased; 
}
    private void OnGrabbed(IInteractor interactor)
    {
        //Debug.Log($"Object grabbed by {interactor.gameObject.name}");
        Debug.Log($"Object grabbed by");
        // Your grab logic here
    }

    private void OnReleased(IInteractor interactor)
    {

        //target.rotation = follower.rotation;
//        target.rotation = Quaternion.identity;
        target.localRotation = follower.localRotation;
        Debug.Log($"Object released");
                StartCoroutine(AlignRotationAfterDelay(0.1f)); // 1 second delay

        //Debug.Log($"Object released by {interactor.gameObject.name}");
        // Your release logic here
    }
        private IEnumerator AlignRotationAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Align the grabbable object's rotation with the outer object's rotation
        //transform.rotation = follower.rotation;
        target.localRotation = follower.localRotation;
    }
    void Update()
    {
        //bool isGrabbed = _handGrabInteractable.isAny();
        //Debug.Log("is grabbed" + isGrabbed );
        //Debug.Log("is grabbed" + _handGrabInteractable.Interactors);
        //_grabbable.Whe
        bool isGrabbed = _handGrabInteractable.State == InteractableState.Select;

        //Debug.Log("is grabbed" + isGrabbed );

        if (isGrabbed && target != null && follower != null)
        {
            // Interpolate the follower's rotation to match the target's rotation
            follower.rotation = Quaternion.Slerp(
                follower.rotation, 
                target.rotation, 
                Time.deltaTime * smoothSpeed
            );
        }
    }
        // Call this when interaction starts
    public void StartInteraction()
    {
        isInteracting = true;
    }

    // Call this when interaction ends
    public void EndInteraction()
    {
        isInteracting = false;
    }
}

using FMODUnity;
using Oculus.Interaction;
using UnityEngine;

public class SnapInteractableEvents : MonoBehaviour
{
    private SnapInteractable snapInteractable;
    public Material referenceMaterial;
    public int matId;
    public SnapManager snapManager;
public StudioEventEmitter eventEmitter;

    private void Awake()
    {
        snapInteractable = GetComponent<SnapInteractable>();
        eventEmitter = GetComponent<StudioEventEmitter>();

        if (snapInteractable != null)
        {
            // snapInteractable.selectEntered.AddListener(OnSnapSelected);
            // snapInteractable.selectExited.AddListener(OnSnapDeselected);
            // snapInteractable.hoverEntered.AddListener(OnHoverStarted);
            // snapInteractable.hoverExited.AddListener(OnHoverEnded);
            snapInteractable.WhenSelectingInteractorAdded.Action += OnInteractorAdded;
        }
    }

    private void OnInteractorAdded(IInteractorView interactor)
    {
        eventEmitter.Play();
        var x = interactor as SnapInteractor;
        var go = x.Rigidbody.gameObject as GameObject;
        var sphere = go.transform.Find("Visuals/Sphere");
        Debug.Log("Snap selected by" + x);
        Debug.Log("Snap selected by" + go);
        Debug.Log("Snap selected by" + sphere);
        Debug.Log("Snap selected by" + sphere.GetComponent<Renderer>().material.color);

        Debug.Log("Snap selected by" + (sphere.GetComponent<Renderer>().material.color == referenceMaterial.color));

        if(sphere.GetComponent<Renderer>().material.color == referenceMaterial.color)
        {
            switch (matId)
            {
                case 0:
                snapManager.finalBoolYellow = true;

                break;
                
                case 1:

                snapManager.finalBoolRed = true;
                break;

                case 2:
                snapManager.finalBoolGreen = true;

                break;
                case 3:
                snapManager.finalBoolBlue = true;

                break;
            }
            snapManager.ChechAll();

        }
    }
}

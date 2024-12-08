using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class SetAudioParam : MonoBehaviour
{
        [SerializeField]
    private EventReference fmodEventReference; // Replace string with EventReference

    private EventInstance eventInstance;
    // Start is called before the first frame update
    public StudioGlobalParameterTrigger studioGlobalParameterTrigger;
    private FMOD.Studio.EventInstance instance;
        private FMOD.Studio.System fmodSystem;


public FMODUnity.EventReference fmodEvent;
    void Start()

    {       
         //       RuntimeManager.GetSystem(out fmodSystem);


        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance.start();
        //studioGlobalParameterTrigger.Value ++;
         //eventInstance = RuntimeManager.CreateInstance(fmodEventReference);
        //eventInstance.start(); // Start the event
       //eventInstance.setParameterByName("MI_TRANSITION", 2);  
       SetAudioParam2();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAudioParam2(){
        //jinstance.setParameterByName("ParameterName
        instance.getParameterByName("MI_TRANSITION", out float x);
        Debug.Log("param" + x);
        x++;
        //instance.setParameterByName("MI_TRANSITION", x);
//FMODUnity.RuntimeManager.StudioSystem.setParameterByName("MI_TRANSITION", x);
    }
}

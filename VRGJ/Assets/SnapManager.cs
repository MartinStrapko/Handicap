using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapManager : MonoBehaviour
{
    public bool finalBoolRed;
    public bool finalBoolYellow;
    public bool finalBoolGreen;
    public bool finalBoolBlue;
    // Start is called before the first frame update

    public void ChechAll() {
        if(finalBoolBlue && finalBoolGreen && finalBoolRed && finalBoolYellow){

FMODUnity.RuntimeManager.StudioSystem.setParameterByName("MI_TRANSITION", 2);
            gameObject.SetActive(false);
        }

    }
}

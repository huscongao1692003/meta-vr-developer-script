using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassthroughtToggle : MonoBehaviour
{
   
    private Button button;
    public OVRPassthroughLayer passthroughLayer;
    bool isPassthroughEnabled;



    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(TogglePassthrough);
    }

    // Function to toggle between passthrough and VR mode
    void TogglePassthrough()
    {

        if(passthroughLayer.enabled == false)
        {
            isPassthroughEnabled = false;
        }
        else
        {
            isPassthroughEnabled = true;
        }

        // Toggle passthrough mode
        if (isPassthroughEnabled)
        {
            passthroughLayer.enabled = false;
        }
        else
        {
            passthroughLayer.enabled = true;
        }
    }
}
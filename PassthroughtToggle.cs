using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassthroughtToggle : MonoBehaviour
{
   
    private Button button;

   
    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(TogglePassthrough);
    }

    // Function to toggle between passthrough and VR mode
    void TogglePassthrough()
    {
        // Check the current state of passthrough mode
        bool isPassthroughEnabled = OVRManager.instance.virtualGreenScreenType != OVRManager.VirtualGreenScreenType.Off;

        // Toggle passthrough mode
        if (isPassthroughEnabled)
        {
            // Disable passthrough mode
            OVRManager.instance.virtualGreenScreenType = OVRManager.VirtualGreenScreenType.Off;
        }
        else
        {
            // Enable passthrough mode
            OVRManager.instance.virtualGreenScreenType = OVRManager.VirtualGreenScreenType.OuterBoundary;
        }
    }
}
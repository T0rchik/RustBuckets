using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class MonitorControl : MonoBehaviour
{
        public HoverButton minimap;
        public HoverButton missileLauncher;
        public GameObject monitor;

        Canvas[] screens;
    // Start is called before the first frame update
    void Start()
    {
        screens = monitor.GetComponentsInChildren<Canvas>();
        screens[0].enabled = false;
        screens[1].enabled = false;

        minimap.onButtonDown.AddListener(Minimap);
        missileLauncher.onButtonDown.AddListener(MissileLauncher);
    }

    public void Minimap(Hand hand)
    {
        if(screens[1].enabled)
        {
            screens[1].enabled = false;
        }

        screens[0].enabled = true;

    }

    public void MissileLauncher(Hand hand)
    {
        if(screens[0].enabled)
        {
            screens[0].enabled = false;
        }

        screens[1].enabled = true;

    }
}

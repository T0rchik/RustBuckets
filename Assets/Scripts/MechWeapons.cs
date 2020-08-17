using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;


public class MechWeapons : MonoBehaviour
{
    //Weapon[] weapons;     //for later when multiple weapons are finished
//    public Weapon gun;
    public Cannon gun;

    public Hand hand;
    public SteamVR_Action_Boolean fireAction;
    public SteamVR_Action_Boolean altFireAction;

    public Canvas weaponMonitor;
    Text[] textBoxes;


    // Start is called before the first frame update
    void Start()
    {
       textBoxes = weaponMonitor.GetComponentsInChildren<Text>(); 
       textBoxes[0].text = gun.name;
       textBoxes[1].text = gun.AmmoToString();
    }

    // Update is called once per frame
    void Update()
    {
        gun.UpdateTTS();

        if(SteamVR_Actions.default_FireWeapon[SteamVR_Input_Sources.RightHand].state && gun.TTS() <= 0f)
       {
           Debug.Log("Firing!!!!");
           gun.Fire();
       }

        if(SteamVR_Actions.default_AltFire[SteamVR_Input_Sources.LeftHand].state)
        {
            gun.AltFire();
        }

       textBoxes[1].text = gun.AmmoToString();
    }


    // For later
    //void changeWeapon(int weapon)
}

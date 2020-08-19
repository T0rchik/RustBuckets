using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;


public class MechWeapons : MonoBehaviour
{
    public Weapon[] weapons;     //for later when multiple weapons are finished
    private Weapon gun;
    private int weaponIdx;
    //public GatlingGun gun;

    public Hand hand;
    public SteamVR_Action_Boolean fireAction;
    public SteamVR_Action_Boolean altFireAction;

    public Canvas weaponMonitor;
    Text[] textBoxes;


    // Start is called before the first frame update
    void Start()
    {
        //weapons = new Weapon[] {cannon, gatlingGun}; 
        weaponIdx = 0;
        gun = weapons[weaponIdx];
        textBoxes = weaponMonitor.GetComponentsInChildren<Text>(); 
        textBoxes[0].text = gun.name;
        textBoxes[1].text = gun.AmmoToString();
    }

    // Update is called once per frame
    void Update()
    {
        gun.UpdateTTS();

        if(SteamVR_Actions.default_NextWeapon[SteamVR_Input_Sources.LeftHand].state)
        {
            nextWeapon();
            textBoxes[0].text = gun.name;
        }

        if(SteamVR_Actions.default_PrevWeapon[SteamVR_Input_Sources.LeftHand].state)
        {
            prevWeapon();
            textBoxes[0].text = gun.name;
        }
        

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


    void nextWeapon()
    {
        weaponIdx++;
        if(weaponIdx >= weapons.Length)
        {
            weaponIdx = 0;
        }
        gun = weapons[weaponIdx];
    }

    void prevWeapon()
    {
        weaponIdx--;
        if(weaponIdx < 0)
        {
            weaponIdx = weapons.Length - 1;
        }
        gun = weapons[weaponIdx];
    }
}

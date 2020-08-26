using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;


public class MechWeapons : MonoBehaviour
{
    public Weapon[] weapons;     //for later when multiple weapons are finished
    private int weaponIdx;
    Weapon monitorGun;

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
        monitorGun = weapons[weaponIdx];
        textBoxes = weaponMonitor.GetComponentsInChildren<Text>(); 
        textBoxes[0].text = monitorGun.name;
        textBoxes[1].text = monitorGun.AmmoToString();
    }

    // Update is called once per frame
    void Update()
    {
        weapons[0].UpdateTTS();
        weapons[1].UpdateTTS();

        if(SteamVR_Actions.default_NextWeapon[SteamVR_Input_Sources.LeftHand].state)
        {
            nextWeapon();
            textBoxes[0].text = monitorGun.name;
        }

        if(SteamVR_Actions.default_PrevWeapon[SteamVR_Input_Sources.LeftHand].state)
        {
            prevWeapon();
            textBoxes[0].text = monitorGun.name;
        }

        if(SteamVR_Actions.default_FireWeapon[SteamVR_Input_Sources.RightHand].state && weapons[0].TTS() <= 0f)
       {
           Debug.Log("Firing " + weapons[0].name + "!");
           weapons[0].Fire();
       }

        if(SteamVR_Actions.default_AltFire[SteamVR_Input_Sources.LeftHand].state && weapons[1].TTS() <= 0f)
        {
           Debug.Log("Firing " + weapons[1].name + "!");
           weapons[1].Fire(); 
        }

       textBoxes[1].text = monitorGun.AmmoToString();
    }


    void nextWeapon()
    {
        weaponIdx++;
        if(weaponIdx >= weapons.Length)
        {
            weaponIdx = 0;
        }
        monitorGun = weapons[weaponIdx];
    }

    void prevWeapon()
    {
        weaponIdx--;
        if(weaponIdx < 0)
        {
            weaponIdx = weapons.Length - 1;
        }
        monitorGun = weapons[weaponIdx];
    }
}

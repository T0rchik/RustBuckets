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

    public Canvas[] weaponMonitors;
    Text[] textBoxes;


    // Start is called before the first frame update
    void Start()
    {
        updateWeaponMonitors();
    }

    // Update is called once per frame
    void Update()
    {
        weapons[0].UpdateTTS();
        weapons[1].UpdateTTS();


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

//       textBoxes[1].text = monitorGun.AmmoToString();
        updateWeaponMonitors();
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

    void updateWeaponMonitors()
    {
        textBoxes = weaponMonitors[0].GetComponentsInChildren<Text>(); 
        textBoxes[0].text = weapons[0].name;
        textBoxes[1].text = weapons[0].AmmoToString();

        textBoxes = weaponMonitors[1].GetComponentsInChildren<Text>(); 
        textBoxes[0].text = weapons[1].name;
        textBoxes[1].text = weapons[1].AmmoToString();
    }
}

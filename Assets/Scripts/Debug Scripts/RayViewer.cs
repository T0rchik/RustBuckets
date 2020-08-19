using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayViewer : MonoBehaviour
{
    public float weaponRange = 50f;
    private Camera HMD_Cam;
    public Camera main;
    public GameObject Mech;
    void Start()
    {
       HMD_Cam = Mech.GetComponentInChildren<Camera>();
    }
    void Update()
    {
       Vector3 lineOrigin = HMD_Cam.ViewportToWorldPoint(new Vector3 (0.5f, 0.5f, 0));
       //Vector3 lineOrigin = main.ViewportToWorldPoint(new Vector3 (0.5f, 0.5f, 0));
       //Vector3 lineOrigin = Mech.transform.TransformDirection(Vector3.forward);
       Debug.DrawRay(lineOrigin, HMD_Cam.transform.forward * weaponRange, Color.green); 
    }
}

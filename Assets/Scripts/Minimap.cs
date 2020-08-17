using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// based on minimap as done in this tutorial: https://www.youtube.com/watch?v=28JTTXqMvOU
public class Minimap : MonoBehaviour
{
    public Transform mech;

    void LateUpdate()
    {
        Vector3 newPosition = mech.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        transform.rotation = Quaternion.Euler(90f, mech.eulerAngles.y, 0f);
    }
}

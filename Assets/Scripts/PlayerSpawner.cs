using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        Instantiate(playerObject, transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

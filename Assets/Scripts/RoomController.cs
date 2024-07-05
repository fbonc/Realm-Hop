using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{

    public Material skyMaterial;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") //We check for a tag so we know we aren't colliding with a wall. Can be removed if necessary, but should
            //just add a Player tag to the player game objects.
        {
            RenderSettings.skybox = skyMaterial;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

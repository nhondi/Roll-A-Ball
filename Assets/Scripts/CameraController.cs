using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position;
    }

    // Update is called once per frame
    //We modify the position of the camera (through its transform, directly accessible through the variable transform), by adding the offset to the position of the player.
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}

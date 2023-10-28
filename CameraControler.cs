using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    [SerializeField]private Transform player;


    // Update is called once per frame
    private void Update()
    {
        transform.position = new Vector3(player.position.x, 3, -15);
    }
}

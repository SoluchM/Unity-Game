using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    [SerializeField]private Transform player;

    // Update is called once per frame
    private void Update()
    {
        float hold = player.position.y;
        if (hold>3)
        {
            transform.position = new Vector3(player.position.x, player.position.y, -17);
        }
        else
        {
            transform.position = new Vector3(player.position.x, 3, -17);
        }

    }
}

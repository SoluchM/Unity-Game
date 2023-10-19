using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemcollector : MonoBehaviour
{
    private int pointcount = 0;

    [SerializeField] private Text point_text;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("points")) 
        {
            Destroy(collision.gameObject);
            pointcount++;
            point_text.text = "Truskawki: " + pointcount;
        }
    }
}

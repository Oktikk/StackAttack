using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollision : MonoBehaviour
{
    private PressDetector _pressDetector;

    private void Start()
    {
        _pressDetector = GetComponent<PressDetector>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_pressDetector.IsKeyPressed())
        {
            if (collision.gameObject.tag == "Box" && !collision.gameObject.GetComponent<BoxController>().isMoving)
            {
                if (Input.GetAxisRaw("Horizontal") > 0f)
                {
                    StartCoroutine(collision.gameObject.GetComponent<BoxController>().MoveBox("Horizontal", 1));
                }
                else if (Input.GetAxisRaw("Horizontal") < 0f)
                {
                    StartCoroutine(collision.gameObject.GetComponent<BoxController>().MoveBox("Horizontal", -1));
                }
            }
        }
    }
}

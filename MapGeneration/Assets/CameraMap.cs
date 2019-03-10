using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMap : MonoBehaviour {

    void Update()
    {
        Vector3 movement = new Vector3();
        if (Input.GetKey(KeyCode.UpArrow))
        {
            movement.y =+ 0.2F;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            movement.y =- 0.2F;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            movement.x = +0.2F;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movement.x = -0.2F;
        }

        this.gameObject.transform.Translate(movement);
    }
}

# Ejercicio 2

![2021-10-26 17-16-58](https://user-images.githubusercontent.com/72868069/138920038-7cd9679f-059b-419f-a12d-ee89282892e8.gif)

```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerAngel : MonoBehaviour
{
    public float speed = 0.1f;

    // Update is called once per frame
    void Update()
    {
        float xDirection = Input.GetAxis("Horizontal");
        float zDirection = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(xDirection, 0.0f, zDirection);
        transform.Translate(moveDirection * speed, Space.Self);
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0.0f, 80*Time.deltaTime, 0.0f);
        } else if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0.0f, -80*Time.deltaTime, 0.0f);
        } 
    }
}

```

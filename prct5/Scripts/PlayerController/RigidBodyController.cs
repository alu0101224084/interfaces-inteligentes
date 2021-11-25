using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyController : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
      PlayerMovement();
    }

    void PlayerMovement()
    {
      float x = Input.GetAxis("Horizontal");
      float z = Input.GetAxis("Vertical");
      Vector3 direction = transform.right * x + transform.forward * z;
      direction *= speed;
      direction.y = 0;
      rigid.velocity = direction;
    }
}
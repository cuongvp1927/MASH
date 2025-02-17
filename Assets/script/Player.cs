using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] float speed = 1;
    public Rigidbody2D rd;
    private Vector2 moveInput;

    // Start is called before the first frame update
    void Start()
    {
        rd = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        rd.velocity = moveInput * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("touch something");
    }


}

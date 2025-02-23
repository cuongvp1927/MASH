using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] float speed = 1;
    public Rigidbody2D rd;
    private Vector2 moveInput;
    private GameMaster GM;
    private float carrying_counter = 0;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log($"collided with {collision.gameObject.name}");

        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Objective_rescue"))
            {
                carrying_counter++;
                Destroy(collision.gameObject);
            }

            if (collision.gameObject.CompareTag("Objective_home"))
            {
                GameMaster.Instance.WinGame(carrying_counter);
            }

            if (collision.gameObject.CompareTag("obstacles"))
            {
                GameMaster.Instance.LoseGame();
            }
        }
    }


}

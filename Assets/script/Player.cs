using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // move
    [SerializeField] float speed = 1;
    public Rigidbody2D rd;
    private Vector2 moveInput;

    // game mechanic
    [SerializeField] float max_carry;
    private float carrying_counter = 0;
    
    // stuff
    [SerializeField] AudioClip hit_objective1;
    [SerializeField] AudioClip hit_objective2;

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
                SoundMaster.Instance.playmusic(hit_objective1,transform,1f);
                Vector3 pos = this.transform.position;
                //pos.x = pos.x + 5;
                //pos.y = pos.y + 5;
                GameMaster.Instance.SpamPopup($"Carrying {carrying_counter} monster", pos); 

                Destroy(collision.gameObject);
            }

            if (collision.gameObject.CompareTag("Objective_home"))
            {
                SoundMaster.Instance.playmusic(hit_objective2, transform, 1f);
                GameMaster.Instance.WinGame(carrying_counter);
            }

            if (collision.gameObject.CompareTag("obstacles"))
            {
                GameMaster.Instance.LoseGame();
            }
        }
    }


}

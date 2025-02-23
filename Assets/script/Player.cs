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
    [SerializeField] float max_carry = 3;
    private float carrying_counter = 0;
    
    // stuff
    [SerializeField] AudioClip hit_objective1;
    [SerializeField] AudioClip hit_objective2;
    [SerializeField] AudioClip lose;

    [SerializeField] GameObject moveField;

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

        IfOffScreen();
    }

    private void IfOffScreen()
    {
        Vector2 pos = this.transform.position;
        Vector2 max = moveField.GetComponent<SpriteRenderer>().bounds.max;
        Vector2 min = moveField.GetComponent<SpriteRenderer>().bounds.min;
        if (pos.x < min.x || pos.x > max.x || pos.y < min.y || pos.y > max.y)
        {
            GameMaster.Instance.CheaterDetected();
            Debug.Log("I already know you will try this!! WHA HA HA!!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log($"collided with {collision.gameObject.name}");

        if (collision != null)
        {

            if (collision.gameObject.CompareTag("Objective_rescue"))
            {
                if (carrying_counter < max_carry)
                {
                    carrying_counter++;
                    SoundMaster.Instance.playmusic(hit_objective1, transform, 1f);
                    Vector3 pos = this.transform.position;
                    //pos.x = pos.x + 5;
                    //pos.y = pos.y + 5;
                    GameMaster.Instance.SpamPopup($"Carrying {carrying_counter} monster", pos);

                    Destroy(collision.gameObject);
                }
                else
                {
                    Vector3 pos = this.transform.position;
                    GameMaster.Instance.SpamPopup($"I'm carrying too many", pos);

                }

            }

            if (collision.gameObject.CompareTag("Objective_home"))
            {
                SoundMaster.Instance.playmusic(hit_objective2, transform, 1f);
                Vector3 pos = this.transform.position;
                GameMaster.Instance.SpamPopup($"You saved the org children, yay!!!", pos);

                GameMaster.Instance.WinGame(carrying_counter);
                carrying_counter = 0;
            }

            if (collision.gameObject.CompareTag("obstacles"))
            {
                SoundMaster.Instance.playmusic(lose, transform, 1f);
                Vector3 pos = this.transform.position;
                GameMaster.Instance.SpamPopup($"Oh NOOOOOOOO!!!!!!!", pos);
                GameMaster.Instance.LoseGame();
            }
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;

    bool jump = false;

    private void Start()
    {
        //�R���|�[�l���g�̓ǂݍ���
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float horizontalKey = Input.GetAxis("Horizontal");
            //�ړ�����@�E
        if(horizontalKey > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
            //�ړ�����@��
        else if(horizontalKey < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
    public float flap = 1000f;

    private void Update()
    {
        if (Input.GetKeyDown("space") &&!jump)
        {
            rb.AddForce(Vector2.up * flap);
            jump = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        jump = false;
    }
}
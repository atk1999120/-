using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed; //���x
    public float gravity; //�d��
    public float jumpSpeed; //�W�����v���鑬�x
    public float jumpHeight; //��������
    public float jumpLimitTime; //�W�����v��������
    public GroundCheck ground; //�ڒn����
    public GroundCheck head; //���Ԃ�������
    public AnimationCurve dashCurve;
    public AnimationCurve jumpCurve;

    private Rigidbody2D rb;
    private bool isGround = false;
    private bool isJump = false;
    private bool isHead = false;
    private float jumpPos = 0.0f;
    private float dashTime, jumpTime;
    private float beforeKey;
    
    void Start()
    {
        //�R���|�[�l���g�̓ǂݍ���
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        //�ڒn����𓾂�
        isGround = ground.IsGround();
        isHead = head.IsGround();

        //�L�[���͂��ꂽ��s������
        float horizontalKey = Input.GetAxis("Horizontal");
        float xSpeed = 0.0f;
        float ySpeed = -gravity;
        float verticalKey = Input.GetAxis("Vertical");

        if (isGround)
        {
            if(verticalKey > 0)
            {
                ySpeed = jumpSpeed;
                jumpPos = transform.position.y; //�W�����v�����ʒu���L�^����
                isJump = true;
                jumpTime = 0.0f;
            }
            else
            {
                isJump = false;
            }
        }
        else if (isJump)
        {
            //������L�[�������Ă��邩
            bool pushUpKey = verticalKey > 0;
            //���݂̍�������ׂ鍂����艺��
            bool canHeight = jumpPos + jumpHeight > transform.position.y;
            //�W�����v���Ԃ������Ȃ肷���Ă��Ȃ���
            bool canTime = jumpLimitTime > jumpTime;

            if(pushUpKey && canHeight && canTime && !isHead)
            {
                ySpeed = jumpSpeed;
                jumpSpeed += Time.deltaTime;
            }
            else
            {
                isJump = false;
                jumpTime = 0.0f;
            }
        }
        
        //�ړ�����@�E
        if (horizontalKey > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            dashTime += Time.deltaTime;
            xSpeed = speed;
        }
        //�ړ�����@��
        else if (horizontalKey < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            dashTime += Time.deltaTime;
            xSpeed = -speed;
        }
        else
        {
            xSpeed = 0.0f;
            dashTime = 0.0f;
        }

        //�O��̓��͂���_�b�V���̔��]�𔻒f���đ��x��ς���
        if (horizontalKey > 0 && beforeKey < 0)
        {
            dashTime = 0.0f;
        }
        else if (horizontalKey < 0 && beforeKey > 0)
        {
            dashTime = 0.0f;
        }
        beforeKey = horizontalKey;

        //�A�j���[�V�����J�[�u�𑬓x�ɓK�p
        xSpeed *= dashCurve.Evaluate(dashTime);
        if (isJump)
        {
            ySpeed *= jumpCurve.Evaluate(jumpTime);
        }
        rb.velocity = new Vector2(xSpeed, ySpeed);
    }
}

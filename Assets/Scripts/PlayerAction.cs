using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float speed;

    Animator anim;
    Rigidbody2D rigid;
    float h;
    float v;
    bool isHorizonMove;
    Vector3 dirVec;
    GameObject scanObj;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //move value
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        //check button up/down
        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");
        //check horizontalMove
        if(hDown)
            isHorizonMove = true;
        else if(vDown)
            isHorizonMove = false;
        else if (hUp || vUp)
            isHorizonMove = h != 0;
        //animation
        if (anim.GetInteger("hAxisRaw") != h) {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if(anim.GetInteger("vAxisRaw") != v) {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else
            anim.SetBool("isChange", false);
        //direction
        if(vDown && v == 1)
            dirVec = Vector3.up;
        else if (vDown && v == -1)
            dirVec = Vector3.down;
        else if (hDown && h == -1)
            dirVec = Vector3.left;
        else if (hDown && h == 1)
            dirVec = Vector3.right;

        //scan
        if (Input.GetButtonDown("Jump") && scanObj != null) {
            Debug.Log(scanObj.name + "발견했다.");
        }
    }

    void FixedUpdate()
    {
        //move
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v); 
        rigid.velocity = moveVec * speed * Time.deltaTime;
        //ray
        Debug.DrawRay(rigid.position, dirVec, new Color(0,1,0));
        RaycastHit2D hit = Physics2D.Raycast(rigid.position, dirVec, 1f, LayerMask.GetMask("Object"));

        if(hit.collider != null) {
            scanObj = hit.collider.gameObject;
        }
        else scanObj = null;
    }
}

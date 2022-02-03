using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public float flyingSpeed;


    public Rigidbody2D rig;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInputs();
    }
 void CheckInputs()
    {
        Move();
        if (Input.GetKey(KeyCode.UpArrow))
        {
            FlyUp();
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            FlyDown();
        }
    }

    void Move()
    {
        float dir = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        if (dir > 0)
            transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        else if (dir < 0)
            transform.localScale = new Vector3(-0.2f, 0.2f, 0.2f);
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            FlyUp();
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            FlyDown();
        }

        rig.velocity = new Vector2(dir * moveSpeed, rig.velocity.y);
    }
   

    void FlyUp()
    {
        rig.AddForce(Vector2.up * flyingSpeed, ForceMode2D.Impulse);
    }

    void FlyDown()
    {
        rig.AddForce(Vector2.down * flyingSpeed, ForceMode2D.Impulse);
    }


}

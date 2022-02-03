using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Obstacle : MonoBehaviour
{

    public Vector3 moveDir;
    public float moveSpeed;
    public AudioSource playSound;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        transform.Rotate(Vector3.back * moveDir.x * (moveSpeed * 20) * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D asteroid)
    {
        if (asteroid.gameObject.CompareTag("Player"))
        {
            playSound.Play();
            
            GameManager.instance.removeLife();
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;

    [SerializeField]
    public float pushForce = 500;
    private float movement;
    [SerializeField]
    private float speed = 5f;
    public Vector3 respawnPoint;
    private GameManager gameManager;
 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        respawnPoint = transform.position;
        gameManager = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Horizontal input'u al
        movement = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        // Ýleri yönde sabit bir kuvvet uygula
        rb.AddForce(0, 0, pushForce * Time.fixedDeltaTime);

        // Yatay hareketi kontrol et
        rb.velocity = new Vector3(movement * speed, rb.velocity.y, rb.velocity.z);

        FallDecector();
    }

   public void OnCollisionEnter(Collision other)
    {
     if (other.collider.CompareTag ("Barier"))
        {
            gameManager.RespawnPlayer();
        }
    }


    private void FallDecector()
    {
        if (rb.position.y < -2f)
        {
            gameManager.RespawnPlayer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("EndTrigger"))
        {
            gameManager.LevelUp();
        }
    }
}

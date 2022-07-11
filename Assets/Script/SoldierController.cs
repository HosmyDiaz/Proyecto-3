using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoldierController : MonoBehaviour
{
    public float JumpForce;
    public float Speed;
    private Rigidbody2D r2d;
    private Animator Animator;
    float Horizontal; 
    private bool Ground = false;
    public GameObject BulletPrefab;
    private float CD;
    private bool isPaused = false;
    public Canvas gamePausedCanvas; //Canavas con la palabra pausa
    
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movimiento (A y D)
        Animator.SetBool("Jumping", false);
        Animator.SetBool("Brunch", false);
        Horizontal = Input.GetAxisRaw("Horizontal");
        if (Horizontal < 0.0f)
        {
            transform.localScale = new Vector3 (-1.0f, 1.0f, 1.0f);
            Animator.SetBool("Jumping", true);
        } 
        else if (Horizontal > 0.0f)
        {
            transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
            Animator.SetBool("Jumping", true);
        }
        Animator.SetBool("Running", Horizontal != 0.0f);

        //Para que el salto no sea infinito
        Debug.DrawRay(transform.position, Vector3.down * 0.17f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.17f))
        {
            Ground = true;
        }
        else Ground = false;
        if (Input.GetKeyDown(KeyCode.W) && Ground)
        {
            Animator.SetBool("Jumping", true);
            Jump();
        }

        //Para disparar
        if (Input.GetKey(KeyCode.K) && Time.time > CD + 0.2f)
        {
            Shoot();
            CD = Time.time;
        }

        if (Input.GetKey(KeyCode.S))
        {
            Animator.SetBool("Brunch", true);
            Horizontal = Input.GetAxisRaw("Horizontal")*0;
        }
        else
        {
            Animator.SetBool("Brunch", false);
        }


        if(Input.GetKeyDown(KeyCode.P))
        {
            if(isPaused == false)
            {
                Time.timeScale = 0;
                gamePausedCanvas.gameObject.SetActive(true);
                isPaused = true;
            }else{
                Time.timeScale = 1;
                gamePausedCanvas.gameObject.SetActive(false);
                isPaused = false;
            }
        } 
    }

    void Jump()
    {
        r2d.AddForce(Vector2.up * JumpForce);
    }

    void FixedUpdate()
    {
        r2d.velocity = new Vector2(Horizontal, r2d.velocity.y);

    }

    void Shoot()
    {
        Vector3 direccion;
        if(transform.localScale.x == 1.0f) 
        {
            direccion = Vector3.right;
        }
        else 
        {
            direccion = Vector3.left;
        }
        GameObject bullet =  Instantiate(BulletPrefab, transform.position + direccion * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletBehaviour>().SetDireccion(direccion);
    }

}

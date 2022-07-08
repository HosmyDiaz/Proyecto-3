using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private Rigidbody2D r2d;
    public float Speed;
    private Vector2 Direccion;
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
       r2d.velocity = Direccion * Speed;
    }

    public void SetDireccion(Vector2 direccion)
    {
        Direccion = direccion;
    }

    public void BulletDestruction()
    {
        Destroy(gameObject);
    }
}

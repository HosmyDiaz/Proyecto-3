using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemyController : MonoBehaviour
{

    private Vector2 currentPosition;
    private Vector2 soldierPosition;
    private Animator anim;
    [SerializeField] private float speed;
    private bool stop = false;
    public float distancia_Stop;
    public float distancia_Back;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = this.gameObject.transform.position;
        soldierPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        anim.SetBool("Run", false);
        float distancia = Vector2.Distance(currentPosition, soldierPosition);
        float moviento = speed * Time.deltaTime;
        if (distancia > distancia_Stop)
        {
            //Debug.Log("Detectado");
            anim.SetBool("Run", true);
            transform.position = Vector2.MoveTowards(transform.position, soldierPosition, moviento);
            if (distancia < distancia_Back)
            {
                anim.SetBool("Run", false);
            }
        }


        if(soldierPosition.x > currentPosition.x)
        {
            this.transform.localScale = new Vector2(1, 1);
        }
        else
        {
            this.transform.localScale = new Vector2(-1, 1);
        }

    }
}

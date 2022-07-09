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

        float distancia = Vector2.Distance(currentPosition, soldierPosition);
        float moviento = speed * Time.deltaTime;
        if (distancia < 4 /*&& stop == false*/)
        {
            //Debug.Log("Detectado");
            anim.SetTrigger("Run");
            transform.position = Vector2.MoveTowards(transform.position, soldierPosition, moviento);
        }
        if (distancia <= 2)
        {
            stop = true;
        }
    }
}

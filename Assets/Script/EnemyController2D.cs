using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyController2D : MonoBehaviour
{
    public float speed;
    public Vector3 target;
    public Transform[] waypoints;
    Vector3 velocity;
    Vector3 previousPosition;
    Animator enemyAnimator;
    bool flipped;

    public void Start()
    {
        Init();
    }

    public virtual void Update() {
        Movement();
    }

    public virtual void Init() {
        enemyAnimator = GetComponent<Animator>();
        target = waypoints[1].position;
    }

    public virtual IEnumerator SetTarget(Vector3 position) {
        yield return new WaitForSeconds(2f);
        target = position;
        FaceTowards(position - transform.position);
    }

    public virtual void FaceTowards(Vector3 direction) {
        if (direction.x < 0f)
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        else {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
    }

    public virtual void Movement() {
        velocity = ((transform.position - previousPosition) / Time.deltaTime);
        previousPosition = transform.position;

        if (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        }
        else {
            if (target == waypoints[0].position)
            {
                if (flipped)
                {
                    flipped = !flipped;
                    StartCoroutine("SetTarget", waypoints[1].position);
                }
            }
            else {
                if (!flipped) {
                    flipped = !flipped;
                    StartCoroutine("SetTarget", waypoints[0].position);
                }
            }
        }
    }

}

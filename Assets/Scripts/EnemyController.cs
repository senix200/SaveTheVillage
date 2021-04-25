using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float speed = 1;
    private Rigidbody2D _rigidbody;
    private bool walking = false;
    private bool attacking = false;
    private Animator _animator;
    private Transform thePlayer;

    public float timeBetweenSteps;
    private float timeBetweenStepsCounter;

    public float timeToMakeStep;
    private float timeToMakeStepCounter;


    public Vector2 directionToMove;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            walking = true;
            attacking = true;
            this.transform.position = Vector2.MoveTowards(this.transform.position, thePlayer.position, 1.15f * speed * Time.deltaTime);
            directionToMove = thePlayer.position - this.transform.position;
            _animator.SetFloat("Horizontal", directionToMove.x);
            _animator.SetFloat("Vertical", directionToMove.y);
            _animator.SetBool("Walking", walking);
            _animator.SetBool("Attacking", attacking);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            walking = false;
            attacking = false;
            _animator.SetBool("Attacking", attacking);


        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        thePlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        timeBetweenStepsCounter = timeBetweenSteps* UnityEngine.Random.Range(0.5f, 1.5f);
        timeToMakeStepCounter = timeToMakeStep * UnityEngine.Random.Range(0.5f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {

        if (walking)
        {
           
            timeToMakeStepCounter -= Time.deltaTime;
            _rigidbody.velocity = directionToMove * speed;
            if (timeToMakeStepCounter < 0)
            {
                walking = false;
                _animator.SetBool("Walking", walking);
                timeBetweenStepsCounter = timeBetweenSteps;
                _rigidbody.velocity = Vector2.zero;

            }
        }
        else
        {
            
            timeBetweenStepsCounter -= Time.deltaTime;
            
            if (timeBetweenStepsCounter < 0 )
            {
                
                timeToMakeStepCounter = timeToMakeStep;
                directionToMove = new Vector2(UnityEngine.Random.Range(-1, 2), UnityEngine.Random.Range(-1,2));
                _animator.SetFloat("Horizontal", directionToMove.x);
                _animator.SetFloat("Vertical", directionToMove.y);
                walking = true;
                _animator.SetBool("Walking", walking);

            }
        }

    }
  
}

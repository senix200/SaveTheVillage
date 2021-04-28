using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public bool isTalking;
    public float speed = 5.0f;
    private const string AXIS_H = "Horizontal", AXIS_V = "Vertical", LASTAXIS_H = "LastH", LASTAXIS_V = "LastV";    
    private Animator _animator;
    private bool walking = false;
    public UnityEngine.Vector2 lastMovement = UnityEngine.Vector2.zero;
    private Rigidbody2D _rigidbody;
    public static bool playerCreated;
    public string nextUuid;
    private bool attacking = false;
    public int arrows;

    public float attackTime;
    private float attackTimeCounter;


    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletForce = 100f;

    private bool attackingBow = false;


    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        PlayerCreated();
        isTalking = false;
    }
    private IEnumerator AttackCo()
    {
        _animator.SetBool("Attacking", true);
        Shoot();
        yield return null;
        _animator.SetBool("Attacking", false);
        yield return new WaitForSeconds(.33f);
        
    }

    private IEnumerator AttackBow()
    {
        Shoot();
        yield return null;
        _animator.SetBool("AttackingBow", false);
        yield return new WaitForSeconds(.33f);
        attackingBow = false;
        arrows--;

    }

    private IEnumerator AttackBowDown()
    {
        attackingBow = true;
        _animator.SetBool("AttackingBow", true);
        yield return new WaitForSeconds(.33f);
    }
    void Shoot()
    {
       
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation) as GameObject;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.up * bulletForce;
    }
    // Update is called once per frame
    void Update()
    {
        Movement();

        if (Input.GetButtonDown("Shoot") && arrows > 0)
        {
            StartCoroutine(AttackBowDown());
        }
        if (Input.GetButtonUp("Shoot") && arrows > 0)
        {
            StartCoroutine(AttackBow());
        }

    }

    public void Movement()
    {
        if (isTalking)
        {
            _rigidbody.velocity = UnityEngine.Vector2.zero;
            return;
        }

        walking = false;

        if (attacking)
        {
            attackTimeCounter -= Time.deltaTime;
            if (attackTimeCounter < 0)
            {
                attacking = false;
                _animator.SetBool("Attacking", false);
            }
        }else if (Input.GetButtonDown("Attack"))
        {
            attacking = true;
            attackTimeCounter = attackTime;
            _rigidbody.velocity = UnityEngine.Vector2.zero;
            _animator.SetBool("Attacking", true);
        }

        if (attackingBow)
        {

        }
        else
        {
            float ejeH = (Mathf.Abs(Input.GetAxisRaw(AXIS_H)) > 0.2f) ? Input.GetAxisRaw(AXIS_H) : 0;
            float ejeV = (Mathf.Abs(Input.GetAxisRaw(AXIS_V)) > 0.2f) ? Input.GetAxisRaw(AXIS_V) : 0;

            walking = (ejeH != 0 || ejeV != 0);

            if (ejeH == 0 && ejeV != 0) lastMovement = new UnityEngine.Vector2(0, ejeV);
            else if (ejeH != 0 && ejeV == 0) lastMovement = new UnityEngine.Vector2(ejeH, 0);
            if (walking)
                _rigidbody.velocity = new UnityEngine.Vector2(Input.GetAxisRaw(AXIS_H), Input.GetAxisRaw(AXIS_V)).normalized * this.speed;
        }
        

    }

    private void LateUpdate()
    {
        if (!walking)
        {
            _rigidbody.velocity = UnityEngine.Vector2.zero;
        }
        _animator.SetFloat(AXIS_H, Input.GetAxisRaw(AXIS_H));
        _animator.SetFloat(AXIS_V, Input.GetAxisRaw(AXIS_V));
        _animator.SetBool("Walking", walking);
        _animator.SetFloat(LASTAXIS_H, lastMovement.x);
        _animator.SetFloat(LASTAXIS_V, lastMovement.y);
    }

    public void PlayerCreated()
    {
        if (!playerCreated)
        {
            DontDestroyOnLoad(this.transform.gameObject);
            playerCreated = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

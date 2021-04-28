using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float timeToDestroy;
    public GameObject arrow;
    public int damage;
    public GameObject bloodAnim;
    public GameObject canvasDamage;
    private GameObject currentAnim;


    private void Update()
    {
        timeToDestroy -= Time.deltaTime;
        if (timeToDestroy < 0)
        {
            Destroy(arrow, 0.5f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {

            if (bloodAnim != null)
            {
                currentAnim = Instantiate(bloodAnim, arrow.transform.position, arrow.transform.rotation);
                Destroy(currentAnim, 0.5f);
            }

            var clone = (GameObject)Instantiate(canvasDamage, arrow.transform.position, Quaternion.Euler(Vector3.zero));

            clone.GetComponent<DamageNumber>().damagePoints = damage;
            collision.gameObject.GetComponent<HealthManager>().DamageCharacter(damage);
            Destroy(arrow);
        }
        
    }
   
}

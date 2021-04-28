using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{

    public int damage;

    public GameObject bloodAnim;
    public GameObject canvasDamage;
    private GameObject bloodPoint;
    //private GameObject currentAnim;

    private CharacterStats stats;
    public string weaponName;

    private void Start()
    {
        bloodPoint = transform.Find("BloodPoint").gameObject;
        stats = GameObject.Find("Player").GetComponent<CharacterStats>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {

            CharacterStats enemyStats = collision.gameObject.GetComponent<CharacterStats>();

            float plaFac = (1 + stats.strengthLevels[stats.level] / CharacterStats.MAX_STAT_VAL);
            float eneFac = (1 - enemyStats.defenseLevels[enemyStats.level] / CharacterStats.MAX_STAT_VAL);

            int totalDamage = (int)(damage * eneFac * plaFac);
            if (Random.Range(0, CharacterStats.MAX_STAT_VAL) < stats.accuracyLevels[stats.level])
            {
                if (Random.Range(0, CharacterStats.MAX_STAT_VAL) < enemyStats.luckLevels[enemyStats.level])
                {
                    totalDamage = 0;
                }
                else
                {
                    totalDamage *= 5;
                }
            }
            if (bloodAnim != null && bloodPoint != null)
            {
                Destroy(Instantiate(bloodAnim,
                            bloodPoint.transform.position,
                            bloodPoint.transform.rotation), 0.5f);
            }

            var clone = (GameObject)Instantiate(canvasDamage,
                                                 bloodPoint.transform.position,
                                                 Quaternion.Euler(Vector3.zero));

            clone.GetComponent<DamageNumber>().damagePoints = totalDamage;
            collision.gameObject.GetComponent<HealthManager>().DamageCharacter(totalDamage);
        }
    }
}

    
        


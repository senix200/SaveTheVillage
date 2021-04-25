﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToPlayer : MonoBehaviour
{
    public float timeToRevivePlayer;
    private float timeRevivalCounter;
    private bool playerReviving;
    private GameObject thePlayer;
    public int damage;
    public GameObject canvasDamage;

    private CharacterStats playerStats;
    private CharacterStats _stats;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {

            if (!collision.gameObject.GetComponent<HealthManager>().flashActive)
            {

                float strFac = 1 + _stats.strengthLevels[_stats.level] / CharacterStats.MAX_STAT_VAL;
                float plaFac = 1 - playerStats.defenseLevels[playerStats.level] / CharacterStats.MAX_STAT_VAL;

                int totalDamage = Mathf.
                                        Clamp((int)(damage * strFac * plaFac),
                                              1, CharacterStats.MAX_HEALTH);


                if (Random.Range(0, CharacterStats.MAX_STAT_VAL) < playerStats.luckLevels[playerStats.level])
                {
                    if (Random.Range(0, CharacterStats.MAX_STAT_VAL) > _stats.accuracyLevels[_stats.level])
                    {
                        totalDamage = 0;
                    }
                }

                var clone = (GameObject)Instantiate(canvasDamage,
                                               collision.gameObject.transform.position,
                                               Quaternion.Euler(Vector3.zero));
                clone.GetComponent<DamageNumber>().damagePoints = totalDamage;


                collision.gameObject.GetComponent<HealthManager>().DamageCharacter(totalDamage);
            }


        }
    }


    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<CharacterStats>();
        _stats = GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerReviving)
        {
            timeRevivalCounter -= Time.deltaTime;
            if (timeRevivalCounter < 0)
            {
                playerReviving = false;
                thePlayer.SetActive(true);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float timeToDestroy;
    // Start is called before the first frame update
    private void Update()
    {
        timeToDestroy -= Time.deltaTime;
        if (timeToDestroy<0)
        {
            Destroy(gameObject);
        }
    }

}

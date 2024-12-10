using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingSaw : EnemyController
{
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player=FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position=Vector3.MoveTowards(transform.position,player.transform.position,maxSpeed * Time.deltaTime);
    }
}

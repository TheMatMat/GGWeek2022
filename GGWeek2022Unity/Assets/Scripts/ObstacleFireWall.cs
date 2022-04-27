using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFireWall : Obstacle
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("FireWall: " + other.gameObject.name);
    }
}

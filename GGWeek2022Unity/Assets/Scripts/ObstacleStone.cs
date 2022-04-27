using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EntireGame
{
    public class ObstacleStone : Obstacle
    {

        // Start is called before the first frame update
        void Start()
        {
            gm = GameObject.Find("/GameManager").GetComponent<GameManager>();

            obstacleSpeed = gm.GeneralSpeed;
        }

        // Update is called once per frame
        void Update()
        {
            obstacleSpeed = gm.GeneralSpeed;

            //obstacleMove
            transform.position -= new Vector3(2.0f * obstacleSpeed * Time.deltaTime, 0, 0);
        }

        public override void CheckForDeath()
        {
            gm.GameOver();
        }

    }
}


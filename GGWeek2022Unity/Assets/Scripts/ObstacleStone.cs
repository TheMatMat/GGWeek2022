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
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
            gm = GameObject.Find("/GameManager").GetComponent<GameManager>();

            obstacleSpeed = gm.GeneralSpeed;

            sprites = this.gameObject.GetComponent<Obstacle>().sprites;
            this.gameObject.transform.Find("ObstacleSprite").GetComponent<SpriteRenderer>().sprite = sprites[3];
            Debug.Log("stone");
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


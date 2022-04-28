using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EntireGame
{
    public class ObstacleFireWall : Obstacle
    {
        public Sprite obstacleSprite;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
            gm = GameObject.Find("/GameManager").GetComponent<GameManager>();

            obstacleSpeed = gm.GeneralSpeed;

            sprites = this.gameObject.GetComponent<Obstacle>().sprites;
            this.gameObject.transform.Find("ObstacleSprite").GetComponent<SpriteRenderer>().sprite = sprites[1];
            this.gameObject.transform.Find("ObstacleSprite").GetComponent<SpriteRenderer>().flipX = true;

            ResizeCollider();

            //Debug.Log("fire wall");
        }

        // Update is called once per frame
        void Update()
        {
            obstacleSpeed = gm.GeneralSpeed;

            //obstacleMove
            transform.position -= new Vector3(2.0f * obstacleSpeed * 1.5f * Time.deltaTime, 0, 0);

            if(this.gameObject.transform.position.y < -10)
            {
                Destroy(this.gameObject);
            }
        }

        public override void CheckForDeath()
        {
            if(player.playerPower != Power.SHIELD)
            {
                gm.GameOver();
            }
        }


    }
}


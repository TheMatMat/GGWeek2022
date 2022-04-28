using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EntireGame
{
    public class ObstacleFireBall : Obstacle
    {

        public Vector3 targetPos, dir;
        

        // Start is called before the first frame update
        void Start()
        {

            player = GameObject.FindWithTag("Player").GetComponent<Player>();
            gm = GameObject.Find("/GameManager").GetComponent<GameManager>();

            obstacleSpeed = gm.GeneralSpeed;

            sprites = this.gameObject.GetComponent<Obstacle>().sprites;
            this.gameObject.transform.Find("ObstacleSprite").GetComponent<SpriteRenderer>().sprite = sprites[2];
            //Debug.Log("fire ball");


            targetPos = player.gameObject.transform.position;
            dir = (targetPos - this.gameObject.transform.position).normalized;

            //transform.rotation = Quaternion.LookRotation(targetPos - this.gameObject.transform.position, Vector3.forward);

            ResizeCollider();
        }

        // Update is called once per frame
        void Update()
        {
            obstacleSpeed = gm.GeneralSpeed;

            //obstacleMove

            //transform.position -= new Vector3(2.0f * obstacleSpeed * Time.deltaTime, 0, 0);
            transform.position += dir * 10.0f * Time.deltaTime;
        }

        public override void CheckForDeath()
        {
            gm.GameOver();
            Destroy(this.gameObject);
        }
    }
}


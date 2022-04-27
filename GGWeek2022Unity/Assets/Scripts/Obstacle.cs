using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EntireGame
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField]
        public float obstacleSpeed;


        public GameObject obstacle;
        public Player player;
        public GameManager gm;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
            gm = GameObject.Find("/GameManager").GetComponent<GameManager>();

            obstacleSpeed = gm.GeneralSpeed;
        }

        // Update is called once per frame
        void Update()
        {
            
            //check and update speed
            obstacleSpeed = gm.GeneralSpeed;
        }

        public void OnTriggerEnter2D(Collider2D other)
        {

            if (other != null)
            {

                if (other.gameObject.name == "Player")
                {
                    //Debug.Log("check death");
                    CheckForDeath();
                }
            }
            
        }

        public virtual void CheckForDeath()
        {

        }

    }
}


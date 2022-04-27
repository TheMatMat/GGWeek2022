using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EntireGame
{

    public class Player : MonoBehaviour
    {
        [Header("-- Power --")]
        public Power playerPower = Power.NONE;

        [Header("-- Jump and Dash --")]
        public float jumpHeight = 4.0f;
        public float dashLength = 4.0f;
        public float dashTime = 0.05f;
        public float returnTime = 0.5f;
        public float currentDashTime, currentReturnTime;

        public AnimationCurve smooth;

        [SerializeField]
        private bool isJumping = false;
        [SerializeField]
        private bool isDashing = false;
        [SerializeField]
        private bool wasDashing = false;

        private Vector2 jumpForce;
        private float originPosX, dashPosX;

        [Header("-- Obstacles --")]
        [SerializeField]
        public List<Obstacle> obstaclesInRange;

        // Start is called before the first frame update
        void Start()
        {
            jumpForce = new Vector2(0, jumpHeight);
        }

        // Update is called once per frame
        void Update()
        {
            //activate power + proceed action
            if (Input.GetKeyDown(KeyCode.A))
            {
                playerPower = Power.SHIELD;
                PlayerShield();
                Debug.Log(playerPower);
            }
            else if (Input.GetKeyDown(KeyCode.Z) && !wasDashing)
            {
                // dash reset
                originPosX = transform.position.x;
                dashPosX = originPosX + dashLength;

                currentDashTime = 0;
                currentReturnTime = 0;

                playerPower = Power.DASH;
                Debug.Log(originPosX + " - " + dashPosX);

                // dash starts
                isDashing = true;

                Invoke("ResetPower", dashTime + returnTime);
            }
            else if (Input.GetKeyDown(KeyCode.E) && playerPower != Power.JUMP)
            {
                isJumping = true;
                playerPower = Power.JUMP;
                Debug.Log(playerPower);
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                playerPower = Power.ATTACK;
                PlayerAttack();
                Debug.Log(playerPower);
            }

            //deactivate power
            if (Input.GetKeyUp(KeyCode.A))
            {
                ResetPower();
            }   
            else if (Input.GetKeyUp(KeyCode.R))
            {

                ResetPower();
            }

            if(isJumping)
            {
                GetComponent<Rigidbody2D>().AddForce(jumpForce, ForceMode2D.Impulse);
                isJumping = false;
            }

            //dash
            if (isDashing && !wasDashing)
            {
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                // increment time
                currentDashTime += Time.deltaTime;

                // a value between 0 and 1
                float perc = Mathf.Clamp01(currentDashTime / dashTime);

                // updating position
                float newPosX = Mathf.Lerp(originPosX, dashPosX, smooth.Evaluate(perc));
                transform.position = new Vector3(newPosX, transform.position.y, transform.position.z);

                if (currentDashTime >= dashTime)
                {
                    // dash finished
                    wasDashing = true;
                    isDashing = false;
                    //transform.position = new Vector3(dashPosX, transform.position.y, transform.position.z);
                }
            }
            //return after dash
            if(!isDashing && wasDashing)
            {
                // increment time
                currentReturnTime += Time.deltaTime;

                // a value between 0 and 1
                float perc = Mathf.Clamp01(currentReturnTime / returnTime);

                // updating position
                float newPosX = Mathf.Lerp(dashPosX, originPosX, smooth.Evaluate(perc));
                transform.position = new Vector3(newPosX, transform.position.y, transform.position.z);

                if (currentReturnTime >= returnTime)
                {
                    // dash finished
                    wasDashing = false;
                    //transform.position = new Vector3(originPosX, transform.position.y, transform.position.z);
                }
            }
        }

        private void PlayerShield()
        {
            
        }
        private void PlayerDash()
        {
            
        }
        private void PlayerJump()
        {
            
        }
        private void PlayerAttack()
        {
            for(int i = 0; i < obstaclesInRange.Count; i++)
            {
                Debug.Log("un tour");
                Debug.Log(i);

                Obstacle oneObstacle = obstaclesInRange[i];
                Destroy(oneObstacle.gameObject);
            }
        }

        private void ResetPower()
        {
            playerPower = Power.NONE;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            //jump end
            if (collision.gameObject.tag == "floor" && playerPower == Power.JUMP)
            {
               ResetPower();
               isJumping = false;
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            Obstacle newObstacleInRange = other.gameObject.GetComponent<ObstacleStone>();

            if (newObstacleInRange != null && !obstaclesInRange.Contains(newObstacleInRange))
                obstaclesInRange.Add(newObstacleInRange);
        }

        void OnTriggerExit2D(Collider2D other)
        {
            Obstacle oldObstacleInRange = other.gameObject.GetComponent<ObstacleStone>();

            if (oldObstacleInRange != null)
                obstaclesInRange.Remove(oldObstacleInRange);
        }

        
    }
}

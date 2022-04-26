using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EntireGame
{
    public class BackgroundTile : MonoBehaviour
    {
        public float tileSpeed = 5.0f;
        public GameObject tile;

        public Color randomColor;


        // Start is called before the first frame update
        void Start()
        {
            tileSpeed = 5.0f;
            randomColor = new Color(Random.Range(0F,1F), Random.Range(0, 1F), Random.Range(0, 1F));

            tile.GetComponent<SpriteRenderer>().material.color = randomColor;
        }

        // Update is called once per frame
        void Update()
        {
            //tileMove
            tile.transform.position -= new Vector3(2.0f * tileSpeed * Time.deltaTime, 0, 0);

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EntireGame
{
    public class BackgroundTile : MonoBehaviour
    {
        public GameManager gm;
        private float tileSpeed;

        public float TileSpeed
        {
            get { return tileSpeed; }
            set { tileSpeed = value; }
        }
 
        public GameObject tile;

        public Color randomColor;
        public Sprite tileSprite;

        private List<Sprite> availableSprites;


        // Start is called before the first frame update
        void Start()
        {
            gm = GameObject.Find("/GameManager").GetComponent<GameManager>();

            tileSpeed = gm.GeneralSpeed;

            randomColor = new Color(Random.Range(0F,1F), Random.Range(0, 1F), Random.Range(0, 1F));

            /*//apply random sprite
            int index = Random.Range(0, availableSprites.Count - 1);
            tile.GetComponent<SpriteRenderer>().sprite = availableSprites[index];*/

            tile.GetComponent<SpriteRenderer>().material.color = randomColor;
        }

        // Update is called once per frame
        void Update()
        {
            //check and update speed
            tileSpeed = gm.GeneralSpeed;

            //tileMove
            transform.position -= new Vector3(2.0f * tileSpeed * Time.deltaTime, 0, 0);

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EntireGame
{
    public class BackgroundTile : MonoBehaviour
    {
        public GameManager gm;
        private float tileSpeed;
        private float selfSpeedRatio = 1;

        public float bgSpeedRatio = 0.8f;
        public float fgSpeedRatio = 1.5f;

        public float TileSpeed
        {
            get { return tileSpeed; }
            set { tileSpeed = value; }
        }
 
        public GameObject tile;

        public Color randomColor;
        public Sprite tileSprite;

        [SerializeField]
        public List<Sprite> availableSpritesBG;
        [SerializeField]
        public List<Sprite> availableSpritesFG;


        // Start is called before the first frame update
        void Start()
        {
            gm = GameObject.Find("/GameManager").GetComponent<GameManager>();

            tileSpeed = gm.GeneralSpeed;

            //randomColor = new Color(Random.Range(0F,1F), Random.Range(0, 1F), Random.Range(0, 1F));

            /*//apply random sprite
            int index = Random.Range(0, availableSprites.Count - 1);
            tile.GetComponent<SpriteRenderer>().sprite = availableSprites[index];*/

            /*tile.GetComponent<SpriteRenderer>().sprite = availableSprites[0];*/
        }

        // Update is called once per frame
        void Update()
        {
            //check and update speed
            tileSpeed = gm.GeneralSpeed;

            //tileMove
            transform.position -= new Vector3(2.0f * tileSpeed * selfSpeedRatio * Time.deltaTime, 0, 0);

        }

        public void SetSprite(int layer, int index = 0)
        {
            switch (layer)
            {
                case 0:
                    tile.GetComponent<SpriteRenderer>().sprite = availableSpritesBG[index];
                    selfSpeedRatio = bgSpeedRatio;
                    break;
                case 1:
                    tile.GetComponent<SpriteRenderer>().sprite = availableSpritesFG[Random.Range(0, availableSpritesFG.Count)];
                    selfSpeedRatio = fgSpeedRatio;
                    break;
                default:
                    break;
            }
        }
    }
}

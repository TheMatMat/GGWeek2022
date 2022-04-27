using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EntireGame
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        public List<GameObject> backgroundTiles;
        public GameObject tilePrefab;
        [SerializeField]
        private float generalSpeed;

        public float GeneralSpeed
        {
            get { return generalSpeed; }
            set { generalSpeed = value; }
        }

        // Start is called before the first frame update
        void Start()
        {
            generalSpeed = 2.0f;

            //Instanciate 3 first tiles
            backgroundTiles.Add(Instantiate(tilePrefab, new Vector3(0 - tilePrefab.GetComponent<SpriteRenderer>().bounds.size.x / 2, 0, 1), Quaternion.identity));
            backgroundTiles.Add(Instantiate(tilePrefab, backgroundTiles[0].transform.position + new Vector3(backgroundTiles[0].GetComponent<SpriteRenderer>().bounds.size.x, 0, 0), Quaternion.identity));
            backgroundTiles.Add(Instantiate(tilePrefab, backgroundTiles[1].transform.position + new Vector3(backgroundTiles[1].GetComponent<SpriteRenderer>().bounds.size.x, 0, 0), Quaternion.identity));
        }

        // Update is called once per frame
        void Update()
        {
            if (backgroundTiles[0].transform.position.x < -20)
            {
                SpawnNewTile();
            }
        }

        void SpawnNewTile()
        {
            //delete first tile from game and list
            GameObject tileToDelete = backgroundTiles[0];
            GameObject lastTile = backgroundTiles[backgroundTiles.Count - 1];

            backgroundTiles.Remove(tileToDelete);
            Destroy(tileToDelete);

            //add a new tile at the end
            GameObject tileToAdd = Instantiate(tilePrefab, lastTile.transform.position + new Vector3(lastTile.GetComponent<SpriteRenderer>().bounds.size.x - 1, 0, 0), Quaternion.identity);

            backgroundTiles.Add(tileToAdd);
        }

        public void GameOver()
        {
            generalSpeed = 0;
        }
    }
}


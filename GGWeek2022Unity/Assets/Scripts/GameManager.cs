using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EntireGame
{
    public class GameManager : MonoBehaviour
    {
        [Header("-- BG Tiles --")]
        [SerializeField]
        public List<GameObject> backgroundTiles;
        public GameObject tilePrefab;

        [Header("-- Game Speed --")]
        [SerializeField]
        private float generalSpeed;
        public float maxSpeed;

        public float GeneralSpeed
        {
            get { return generalSpeed; }
            set { generalSpeed = value; }
        }
        public float timeBtwnSpeedUpdate = 10.0f;
        private float nextUpdate;

        [Header("-- Obstacle Settings --")]
        public GameObject obstaclePrefab;
        public bool canSpawnObstacle = false;
        public float spawnDelay = 3.0f;

        // Start is called before the first frame update
        void Start()
        {
            canSpawnObstacle = true;

            generalSpeed = 2.0f;
            maxSpeed = 10.0f;
            nextUpdate = timeBtwnSpeedUpdate;

            //Instanciate 3 first tiles
            backgroundTiles.Add(Instantiate(tilePrefab, new Vector3(0 - tilePrefab.GetComponent<SpriteRenderer>().bounds.size.x / 2, 0, 1), Quaternion.identity));
            backgroundTiles.Add(Instantiate(tilePrefab, backgroundTiles[0].transform.position + new Vector3(backgroundTiles[0].GetComponent<SpriteRenderer>().bounds.size.x, 0, 0), Quaternion.identity));
            backgroundTiles.Add(Instantiate(tilePrefab, backgroundTiles[1].transform.position + new Vector3(backgroundTiles[1].GetComponent<SpriteRenderer>().bounds.size.x, 0, 0), Quaternion.identity));

            Invoke("SpawnNewObstacle", 3.0f);
        }

        // Update is called once per frame
        void Update()
        {
            if (backgroundTiles[0].transform.position.x < -20)
            {
                SpawnNewTile();
            }

            if(Time.time > nextUpdate && generalSpeed <= maxSpeed)
            {
                generalSpeed += 0.5f;
                nextUpdate += timeBtwnSpeedUpdate;
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

        void SpawnNewObstacle()
        {
            if (canSpawnObstacle)
            {
                int index = Random.Range(0, 4);
                GameObject newObstacle = null;

                switch (index)
                {
                    //stone
                    case 0:
                        newObstacle = Instantiate(obstaclePrefab, new Vector3(10, -3, 0), Quaternion.identity);
                        newObstacle.AddComponent<ObstacleStone>();
                        break;
                    //tornado
                    case 1:
                        newObstacle = Instantiate(obstaclePrefab, new Vector3(10, -3, 0), Quaternion.identity);
                        newObstacle.AddComponent<ObstacleFireWall>();
                        break;
                    //fireball
                    case 2:
                        newObstacle = Instantiate(obstaclePrefab, new Vector3(Random.Range(-10, 10), 8, 0), Quaternion.identity);
                        newObstacle.AddComponent<ObstacleFireBall>();
                        break;
                    //weapon
                    case 3:
                        newObstacle = Instantiate(obstaclePrefab, new Vector3(10, -3, 0), Quaternion.identity);
                        newObstacle.AddComponent<ObstacleWeapon>();
                        break;
                    default:
                        break;
                }

            
                Invoke("SpawnNewObstacle", spawnDelay);
            }
        }

        public void GameOver()
        {
            generalSpeed = 0;
            canSpawnObstacle = false;
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EntireGame
{
    public class GameManager : MonoBehaviour
    {
        [Header("-- BG Tiles --")]
        [SerializeField]
        public List<GameObject> backgroundTilesBG;
        [SerializeField]
        public List<GameObject> backgroundTilesFG;
        public GameObject tilePrefab;

        private int indexBGTile = 0;

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

            //Instanciate 3 first tiles BG
            backgroundTilesBG.Add(Instantiate(tilePrefab, new Vector3(0, 0, 2), Quaternion.identity));
            backgroundTilesBG.Add(Instantiate(tilePrefab, new Vector3(0, 0, 2), Quaternion.identity));
            backgroundTilesBG.Add(Instantiate(tilePrefab, new Vector3(0, 0, 2), Quaternion.identity));

            backgroundTilesBG[0].GetComponent<BackgroundTile>().SetSprite(0, 0);
            backgroundTilesBG[1].GetComponent<BackgroundTile>().SetSprite(0, 1);
            backgroundTilesBG[2].GetComponent<BackgroundTile>().SetSprite(0, 2);

            backgroundTilesBG[1].transform.position = backgroundTilesBG[0].transform.position + new Vector3(backgroundTilesBG[0].GetComponent<SpriteRenderer>().bounds.size.x, 0, 0);
            backgroundTilesBG[2].transform.position = backgroundTilesBG[1].transform.position + new Vector3(backgroundTilesBG[1].GetComponent<SpriteRenderer>().bounds.size.x, 0, 0); 
            
            //Instanciate 3 first tiles FG
            backgroundTilesFG.Add(Instantiate(tilePrefab, new Vector3(0, 0.4f, 1), Quaternion.identity));
            backgroundTilesFG.Add(Instantiate(tilePrefab, new Vector3(0, 0.4f, 1), Quaternion.identity));
            backgroundTilesFG.Add(Instantiate(tilePrefab, new Vector3(0, 0.4f, 1), Quaternion.identity));

            backgroundTilesFG[0].GetComponent<BackgroundTile>().SetSprite(1, 0);
            backgroundTilesFG[1].GetComponent<BackgroundTile>().SetSprite(1, 1);
            backgroundTilesFG[2].GetComponent<BackgroundTile>().SetSprite(1, 2);

            backgroundTilesFG[1].transform.position = backgroundTilesFG[0].transform.position + new Vector3(backgroundTilesFG[0].GetComponent<SpriteRenderer>().bounds.size.x, 0, 0);
            backgroundTilesFG[2].transform.position = backgroundTilesFG[1].transform.position + new Vector3(backgroundTilesFG[1].GetComponent<SpriteRenderer>().bounds.size.x, 0, 0);

            Invoke("SpawnNewObstacle", 3.0f);
        }

        // Update is called once per frame
        void Update()
        {
            if (backgroundTilesBG[0].transform.position.x < -200)
            {
                SpawnNewTile(0);
            }

            if (backgroundTilesFG[0].transform.position.x < -20)
            {
                SpawnNewTile(1);
            }


            if (Time.time > nextUpdate && generalSpeed <= maxSpeed)
            {
                generalSpeed += 0.5f;
                nextUpdate += timeBtwnSpeedUpdate;
            }

        }

        void SpawnNewTile(int i = -1)
        {
            //i = 0 BG
            //i = 1 FG

            switch (i)
            {
                case 0:
                    //background
                    //delete first tile from game and list
                    if (indexBGTile >= 3)
                    {
                        indexBGTile = 0;
                    }

                    GameObject tileToDelete = backgroundTilesBG[0];
                    GameObject lastTile = backgroundTilesBG[backgroundTilesBG.Count - 1];

                    backgroundTilesBG.Remove(tileToDelete);
                    Destroy(tileToDelete);

                    //add a new tile at the end
                    GameObject tileToAdd = Instantiate(tilePrefab, lastTile.transform.position + new Vector3(lastTile.GetComponent<SpriteRenderer>().bounds.size.x - 1, 0, 0), Quaternion.identity);
                    tileToAdd.GetComponent<BackgroundTile>().SetSprite(0, indexBGTile);

                    backgroundTilesBG.Add(tileToAdd);

                    indexBGTile++;
                    break;
                case 1:
                    //foreground
                    //delete first tile from game and list
                    tileToDelete = backgroundTilesFG[0];
                    lastTile = backgroundTilesFG[backgroundTilesFG.Count - 1];

                    backgroundTilesFG.Remove(tileToDelete);
                    Destroy(tileToDelete);

                    //add a new tile at the end
                    tileToAdd = Instantiate(tilePrefab, lastTile.transform.position + new Vector3(lastTile.GetComponent<SpriteRenderer>().bounds.size.x - 1, 0, 0), Quaternion.identity);
                    tileToAdd.GetComponent<BackgroundTile>().SetSprite(1);

                    backgroundTilesFG.Add(tileToAdd);
                    break;
                default:
                    break;
            }

            
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
                        newObstacle = Instantiate(obstaclePrefab, new Vector3(10, -3.3f, 0), Quaternion.identity);
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
                        newObstacle = Instantiate(obstaclePrefab, new Vector3(10, -3.8f, 0), Quaternion.identity);
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


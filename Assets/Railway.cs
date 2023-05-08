using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Railway : Terrain
{
    [SerializeField] Train trainPrefab;
    [SerializeField] float minTrainInterval;
    [SerializeField] float maxTrainInterval;
    [SerializeField] float trainPosY;
    Vector3 trainSpawnPosition;
    Quaternion trainRotation;

    private void Start()
    {
        if (Random.value > 0.5f)
        {
            trainSpawnPosition = new Vector3((horizontalSize / 2 + 3) + outsideSize, trainPosY, this.transform.position.z);
            trainRotation = Quaternion.Euler(0, -90, 0);
        }
        else
        {
            trainSpawnPosition = new Vector3(-(horizontalSize / 2 + 3) - outsideSize, trainPosY, this.transform.position.z);
            trainRotation = Quaternion.Euler(0, 90, 0);
        }

    }
    
    float timer;


    private void Update()
    {
        if (timer <= 0)
        {
            timer = Random.Range(minTrainInterval, maxTrainInterval);
            SpawnCar();
            return;
        }
        timer -= Time.deltaTime;
    }

    private void SpawnCar()
    {
        var train = Instantiate(trainPrefab, trainSpawnPosition, trainRotation);
        train.SetUpDistanceLimit(horizontalSize + outsideSize + 10);
    }

}

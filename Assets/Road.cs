using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : Terrain
{   
    [SerializeField] Car carPrefab;
    [SerializeField] float minCarInterval;
    [SerializeField] float maxCarInterval;
    [SerializeField] float carPosY;
    Vector3 carSpawnPosition;
    Quaternion carRotation;

    private void Start()
    {
        if (Random.value > 0.5f)
        {
            carSpawnPosition = new Vector3(horizontalSize / 2 + 3, carPosY, this.transform.position.z);
            carRotation = Quaternion.Euler(0, -90, 0);
        }
        else
        {
            carSpawnPosition = new Vector3(-(horizontalSize / 2 + 3), carPosY, this.transform.position.z);
            carRotation = Quaternion.Euler(0, 90, 0);
        }

    }

    float timer;

    private void Update()
    {
        if (timer <= 0)
        {
            timer = Random.Range(minCarInterval, maxCarInterval);
            SpawnCar();
            return;
        }
        timer -= Time.deltaTime;
    }

    private void SpawnCar()
    {
        var car = Instantiate(carPrefab, carSpawnPosition, carRotation);
        car.SetUpDistanceLimit(horizontalSize + 6);
    }
}

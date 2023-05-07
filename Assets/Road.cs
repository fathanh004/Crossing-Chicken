using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : Terrain
{
    [SerializeField] List<Car> carPrefabList;
    [SerializeField] float minCarInterval;
    [SerializeField] float maxCarInterval;
    [SerializeField] float carPosY;
    Vector3 carSpawnPosition;
    Quaternion carRotation;
    float fixedSpeed;
    Car carPrefab;


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
        fixedSpeed = Random.Range(2, 7);
    }

    float timer;

    private void Update()
    {
        if (timer <= 0)
        {
            timer = Random.Range(minCarInterval, maxCarInterval);
            SpawnCar(fixedSpeed);
            return;
        }
        timer -= Time.deltaTime;
    }

    private void SpawnCar(float speed)
    {
        if (speed > 4)
        {
            carPrefab = carPrefabList[0];
        } else
        {
            carPrefab = carPrefabList[1];
        }

        var car = Instantiate(carPrefab, carSpawnPosition, carRotation);
        car.SetUpDistanceLimit(horizontalSize + 6);

        car.SetUpSpeed(speed);
    }
}

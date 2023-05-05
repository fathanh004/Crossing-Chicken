using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleSpawner : MonoBehaviour
{
    [SerializeField] Eagle eagle;
    [SerializeField] Chicken chicken;
    [SerializeField] float initialEagleTimer = 10;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = initialEagleTimer;
        eagle.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0 && eagle.gameObject.activeInHierarchy == false)
        {
            eagle.gameObject.SetActive(true);
            eagle.transform.position = chicken.transform.position + new Vector3(0, 0, 13);
            chicken.SetUnmoveable(false);
        }
        timer -= Time.deltaTime;
    }

    public void ResetTimer()
    {
        timer = initialEagleTimer;
    }
}

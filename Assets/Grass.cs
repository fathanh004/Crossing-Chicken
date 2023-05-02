using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : Terrain
{
    [SerializeField] GameObject treePrefab;
    [SerializeField, Range(0, 1)] float treeProbability;

    public void SetTreePercentage(float newProbability)
    {
        this.treeProbability = Mathf.Clamp01(newProbability);
    }

    public override void Generate(int size)
    {
        base.Generate(size);

        var limit = Mathf.FloorToInt((float)size / 2);
        var treeCount = Mathf.FloorToInt((float)size * treeProbability);

        //membuat daftar posisi yang masih kosong
        List<int> emptyPositionList = new List<int>();
        for (int i = -limit; i <= limit; i++)
        {
            emptyPositionList.Add(i);
        }


        for (int i = 0; i < treeCount; i++)
        {
            //mengambil posisi random dari daftar posisi yang masih kosong
            var randomIndex = Random.Range(0, emptyPositionList.Count);
            var xPos = emptyPositionList[randomIndex];

            //menghapus posisi yang sudah dipakai
            emptyPositionList.RemoveAt(randomIndex);

            //spawn pohon dengan posisi yang terpilih
            SpawnTree(xPos);
        }

        SpawnTree(-limit - 1);
        SpawnTree(limit + 1);
    }

    private void SpawnTree(int xPos)
    {
        var go = Instantiate(treePrefab,
        new Vector3(xPos, 0, this.transform.position.z),
        Quaternion.identity, 
        transform);
    }
}

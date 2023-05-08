using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;
    int movableLimit;
    protected int outsideSize = 10;

    protected int horizontalSize;

    public virtual void Generate(int size)
    {
        if (size == 0)
        {
            return;
        }
        horizontalSize = size;

        if ((float)size % 2 == 0)
        {
            size--;
        }

        int limit = Mathf.FloorToInt((float)size / 2);

        for (int i = -limit; i <= limit; i++)
        {
            SpawnTile(i);
        }
        
        //spawn darken tile outside of the movable tile
        for (int i = -limit-outsideSize; i < -limit; i++)
        {
            SpawnDarkenTile(i);
        }

        for (int i = limit+1; i < limit+outsideSize; i++)
        {
            SpawnDarkenTile(i);
        }
    }

    private GameObject SpawnTile(int xPos)
    {
        var go = Instantiate(tilePrefab, transform);
        go.transform.localPosition = new Vector3(xPos, 0, 0);
        return go;
    }

    private void DarkenObject(GameObject go)
    {
        var rendererList = go.GetComponentsInChildren<MeshRenderer>(includeInactive: true);
        foreach (var rend in rendererList)
        {
            rend.material.color *= Color.grey;
        }
    }

    private void SpawnDarkenTile(int xPos)
    {
        var go = SpawnTile(xPos);
        DarkenObject(go);
    }
}

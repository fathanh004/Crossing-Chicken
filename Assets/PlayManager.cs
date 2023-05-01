using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    [SerializeField] Grass grassPrefab;
    [SerializeField] Road roadPrefab;
    [SerializeField] int initialGrassCount = 5;
    [SerializeField] int horizontalSize;
    [SerializeField] int backRelativeDistance = -4;
    [SerializeField] int forwardViewDistance = 15;
    [SerializeField, Range(0, 1)] float treeProbability;

    private List<Terrain> terrainList;
    Dictionary<int, Terrain> activeTerrainDict = new Dictionary<int, Terrain>(20);

    private void Start()
    {
        terrainList = new List<Terrain>(){
            grassPrefab,
            roadPrefab
        };

        //membuat initial grass
        for (int zPos = backRelativeDistance; zPos < initialGrassCount; zPos++)
        {
            var grass = Instantiate(grassPrefab);
            grass.transform.localPosition = new Vector3(0, 0, zPos);
            grass.SetTreePercentage(zPos < 0 ? 1 : 0);
            grass.Generate(horizontalSize);
            activeTerrainDict[zPos] = grass;
        }

        for (int zPos = initialGrassCount; zPos < forwardViewDistance; zPos++)
        {
            var randomIndex = Random.Range(0, terrainList.Count);
            var terrain = Instantiate(terrainList[randomIndex]);
            terrain.transform.localPosition = new Vector3(0, 0, zPos);
            terrain.Generate(horizontalSize);

            activeTerrainDict[zPos] = terrain;
        }
    }

    private Terrain SpawnRandomTerrain(int zPos)
    {
        Terrain terrainCheck = null;
        for (int z = -1; z >= -3; z--)
        {
            var checkPos = zPos + forwardViewDistance + z;
            if (terrainCheck == null)
            {
                terrainCheck = activeTerrainDict[checkPos];
                continue;
            }
            else if (terrainCheck.GetType() != activeTerrainDict[checkPos].GetType())
            {
                var randomIndex = Random.Range(0, terrainList.Count);
                return Instantiate(terrainList[randomIndex]);
            }
            else
            {
                continue;
            }
        }

        if (terrainCheck is Road)
        {
            return Instantiate(grassPrefab);
        }
        else
        {
            return Instantiate(roadPrefab);
        }

        return null;
    }
}

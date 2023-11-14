using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkerRadius;
    Vector3 noTerrainPosition;
    public LayerMask terrainMask;
    public PlayerMovement pm;
    public GameObject currentChunk;

    [Header("Optimization")]
    public List<GameObject> spawnedChunks;
    public GameObject latestChunk;
    public float maxOpDist; //be grater than the length and with tilemap
    float opDist;



    void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
    }

    
    void Update()
    {
        ChunkChecker();
        ChunkOptimizer();
    }

    void ChunkChecker() {

        if (!currentChunk)
        {
            return;
        }

        if (pm.InputVector.x > 0 && pm.InputVector.y == 0) //right
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Right").position;
                SpawnChunk();
            }
        }
        else if (pm.InputVector.x < 0 && pm.InputVector.y == 0) //left  
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Left").position;
                SpawnChunk();
            }
        }
        //else if (pm.InputVector.x == 0 && pm.InputVector.y > 0) //up
        //{
        //    if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Up").position, checkerRadius, terrainMask))
        //    {
        //        noTerrainPosition = currentChunk.transform.Find("Up").position;
        //        SpawnChunk();
        //    }
        //}
        //else if (pm.InputVector.x == 0 && pm.InputVector.y < 0) //down
        //{
        //    if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Down").position, checkerRadius, terrainMask))
        //    {
        //        noTerrainPosition = currentChunk.transform.Find("Down").position;
        //        SpawnChunk();
        //    }
        //}
        //else if (pm.InputVector.x > 0 && pm.InputVector.y > 0) //rigt up
        //{
        //    if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right Up").position, checkerRadius, terrainMask))
        //    {
        //        noTerrainPosition = currentChunk.transform.Find("Right Up").position;
        //        SpawnChunk();
        //    }
        //}
        //else if (pm.InputVector.x > 0 && pm.InputVector.y < 0) //rigt down
        //{
        //    if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right Down").position, checkerRadius, terrainMask))
        //    {
        //        noTerrainPosition = currentChunk.transform.Find("Right Down").position;
        //        SpawnChunk();
        //    }
        //}
        //else if (pm.InputVector.x < 0 && pm.InputVector.y > 0) //left up
        //{
        //    if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left Up").position, checkerRadius, terrainMask))
        //    {
        //        noTerrainPosition = currentChunk.transform.Find("Left Up").position;
        //        SpawnChunk();
        //    }
        //}
        //else if (pm.InputVector.x < 0 && pm.InputVector.y < 0) //left down
        //{
        //    if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left Down").position, checkerRadius, terrainMask))
        //    {
        //        noTerrainPosition = currentChunk.transform.Find("Left Down").position;
        //        SpawnChunk();
        //    }
        //}
    }

    void SpawnChunk() {
        int rand = Random.Range(0, terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[rand], noTerrainPosition, Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }

    void ChunkOptimizer()
    {
        foreach (GameObject chunk in spawnedChunks)
        {
            opDist = Vector3.Distance(player.transform.position, chunk.transform.position);
            if (opDist > maxOpDist)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }

    }
}

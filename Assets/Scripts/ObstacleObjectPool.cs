using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObjectPool : MonoBehaviour
{
    public GameObject obstacleBarrelPrefab;
    public GameObject obstacleBarrierPrefab;
    public GameObject obstacleStoneWallPrefab;
    public int poolSize = 10;

    private List<GameObject> obstacleBarrelPool;
    private List<GameObject> obstacleBarrierPool;
    private List<GameObject> obstacleStoneWallPool;

    public static ObstacleObjectPool instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        obstacleBarrelPool = new List<GameObject>();
        obstacleBarrierPool = new List<GameObject>();
        obstacleStoneWallPool = new List<GameObject>();
    }

    private IEnumerator Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            CreateNewObjects();
            if (i % 5 == 0)
            {
                yield return null;
            }
        }
    }

    private void CreateNewObjects()
    {
        var barrel = Instantiate(obstacleBarrelPrefab);
        barrel.SetActive(false);
        obstacleBarrelPool.Add(barrel);
        
        var barrier = Instantiate(obstacleBarrierPrefab);
        barrier.SetActive(false);
        obstacleBarrierPool.Add(barrier);

        var stoneWall = Instantiate(obstacleStoneWallPrefab);
        stoneWall.SetActive(false);
        obstacleStoneWallPool.Add(stoneWall);
    }

    public GameObject Acquire(int obstacleType)
    {
        List<GameObject> selectedPool = null;

        if (obstacleType == 0) selectedPool = obstacleBarrelPool;
        else if (obstacleType == 1) selectedPool = obstacleBarrierPool;
        else if (obstacleType == 2) selectedPool = obstacleStoneWallPool;

        if (selectedPool ==  null) return null;

        foreach (GameObject go in selectedPool)
        {
            if (!go.activeInHierarchy)
            {
                go.SetActive(true);
                return go;
            }
        }

        return null;
    }

    public void Release(GameObject obstacle, int obstacleType)
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Zenject;
using UnityEngine.Tilemaps;
using Unity.VisualScripting;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;



public class EnemySpawner : MonoBehaviour
{
    //[SerializeField] private BasicEnemy enemies;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Transform EnemyContainer;
    
    private Camera mainCamera;
    public float spawnRadius = 10f;
    public float spawnInterval = 2f;
    private bool isSpawning = true;

    private ResourceManager resourceManager;
    private Player player;

    [Inject]
    private void Construct(ResourceManager resourceManager, Player player)
    { 
        this.resourceManager = resourceManager;
        this.player = player;
    }
    
    private void Awake()
    {
        mainCamera = Camera.main;
    }
    private void Start()
    {
        if (resourceManager.enemies.Count() > 0)
        {
            StartSpawning().Forget();
        }
    }

    private Vector3 GetSpawnPositionOutsideView()
    {  
        Vector3 spawnPosition;        
        Vector3 viewportBottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 viewportTopRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        float spawnX = Random.Range(viewportBottomLeft.x - spawnRadius, viewportTopRight.x + spawnRadius);
        float spawnY = Random.Range(viewportBottomLeft.y - spawnRadius, viewportTopRight.y + spawnRadius);

        if (Random.Range(0, 2) == 0) 
        {
            spawnX = (Random.Range(0, 2) == 0) ? viewportBottomLeft.x - spawnRadius : viewportTopRight.x + spawnRadius;
        }
        else 
        {
            spawnY = (Random.Range(0, 2) == 0) ? viewportBottomLeft.y - spawnRadius : viewportTopRight.y + spawnRadius;
        }

        spawnPosition = new Vector3(spawnX, spawnY, 0);
        return spawnPosition;
    }

    private bool PointOnTilemap(Vector3 point)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(point);
        return tilemap.HasTile(cellPosition);
    }
        

    private Vector3 GetPointOnTileMap()
    {
        Vector3 point;
        while (true)
        {
            point = GetSpawnPositionOutsideView();
            if (PointOnTilemap(point))
            {
                return point;
            }
        }        
    }

    private async UniTaskVoid StartSpawning()
    {
        while (isSpawning)
        {            
            await UniTask.Delay((int)(spawnInterval * 1000));
            
            if (!isSpawning)
            {
                break;
            }

            Vector3 spawnPoint = GetPointOnTileMap();
            BasicEnemy enemy = Instantiate(resourceManager.enemies[Random.Range(0,resourceManager.enemies.Count())], spawnPoint, Quaternion.identity, EnemyContainer);
            enemy.SetTarget(player.transform);
        }
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }

    
    public void ResumeSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            StartSpawning().Forget();
        }
    }
    private void OnDestroy()
    {
        StopSpawning();
    }

    private void OnDrawGizmos()
    {
        if (mainCamera != null)
        {
            Vector3 viewportBottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
            Vector3 viewportTopRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(viewportBottomLeft.x - spawnRadius, viewportBottomLeft.y - spawnRadius, 0),
                            new Vector3(viewportTopRight.x + spawnRadius, viewportBottomLeft.y - spawnRadius, 0));

            Gizmos.DrawLine(new Vector3(viewportTopRight.x + spawnRadius, viewportBottomLeft.y - spawnRadius, 0),
                            new Vector3(viewportTopRight.x + spawnRadius, viewportTopRight.y + spawnRadius, 0));

            Gizmos.DrawLine(new Vector3(viewportTopRight.x + spawnRadius, viewportTopRight.y + spawnRadius, 0),
                            new Vector3(viewportBottomLeft.x - spawnRadius, viewportTopRight.y + spawnRadius, 0));

            Gizmos.DrawLine(new Vector3(viewportBottomLeft.x - spawnRadius, viewportTopRight.y + spawnRadius, 0),
                            new Vector3(viewportBottomLeft.x - spawnRadius, viewportBottomLeft.y - spawnRadius, 0));
        }
    }


}

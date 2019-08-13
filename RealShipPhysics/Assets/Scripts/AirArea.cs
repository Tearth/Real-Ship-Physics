using System.Collections;
using UnityEngine;

public class AirArea : MonoBehaviour
{
    public Vector3Int Size;
    public AirVoxel AirVoxelPrefab;

    private AirVoxel[,,] _airGrid;

    void Awake()
    {
        ClearAirGrid();
        GenerateAirGrid();
    }

    void Start()
    {
        
    }

    void Update()
    {

    }

    void OnDrawGizmos()
    {
        var centerOfArea = ((Vector3)Size / 2) - new Vector3(0.5f, 0.5f, 0.5f);
        var startPoint = transform.position - centerOfArea;

        for (var x = 0; x < Size.x; x++)
        {
            for (var y = 0; y < Size.y; y++)
            {
                for (var z = 0; z < Size.z; z++)
                {
                    Gizmos.DrawWireCube(startPoint + new Vector3(x, y, z), Vector3.one);
                }
            }
        }
    }

    private void ClearAirGrid()
    {
        var destroyedAirVoxels = 0;
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
            destroyedAirVoxels++;
        }

        Debug.Log($"{destroyedAirVoxels} air voxels destroyed");
    }

    private void GenerateAirGrid()
    {
        _airGrid = new AirVoxel[Size.x, Size.y, Size.z];

        var id = 0;
        for (var x = 0; x < Size.x; x++)
        {
            for (var y = 0; y < Size.y; y++)
            {
                for (var z = 0; z < Size.z; z++)
                {
                    _airGrid[x, y, z] = Instantiate(AirVoxelPrefab, new Vector3(x, y, z), Quaternion.identity, transform);
                    _airGrid[x, y, z].name = $"AirVoxel {id++}";
                }
            }
        }

        Debug.Log($"{_airGrid.Length} air voxels generated");
    }
}

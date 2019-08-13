using System.Collections;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class AirArea : MonoBehaviour
{
    public Vector3Int Size;

    public float VoxelLength;
    public AirVoxel AirVoxelPrefab;

    public AirVoxel[,,] _airGrid;

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
        var (voxelSize, startPoint, endPoint) = GetStartAndEndPointForGrid();

        int xx = 0;
        for (var x = startPoint.x; x < endPoint.x; x += VoxelLength)
        {
            int yy = 0;
            for (var y = startPoint.y; y < endPoint.y; y += VoxelLength)
            {
                int zz = 0;
                for (var z = startPoint.z; z < endPoint.z; z += VoxelLength)
                {
                    if(_airGrid != null)
                    Gizmos.color = _airGrid[xx, yy, zz].BuoyancyForce > 150 / 2 ? Color.green : Color.red;
                    Gizmos.DrawWireCube(new Vector3(x, y, z), voxelSize);

                    zz++;
                }

                yy++;
            }

            xx++;
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
        var (voxelSize, startPoint, _) = GetStartAndEndPointForGrid();

        for (var x = 0; x < Size.x; x++)
        {
            for (var y = 0; y < Size.y; y++)
            {
                for (var z = 0; z < Size.z; z++)
                {
                    _airGrid[x, y, z] = Instantiate(AirVoxelPrefab, startPoint + Vector3.Scale(voxelSize, new Vector3(x, y, z)), Quaternion.identity, transform);
                    _airGrid[x, y, z].name = $"AirVoxel {id++}";
                    _airGrid[x, y, z].VoxelLength = VoxelLength;
                }
            }
        }

        Debug.Log($"{_airGrid.Length} air voxels generated");
    }

    private (Vector3 voxelSize, Vector3 startPoint, Vector3 endPoint) GetStartAndEndPointForGrid()
    {
        var voxelSize = Vector3.one * VoxelLength;
        var centerOfArea = Vector3.Scale(Size, voxelSize) / 2 - (voxelSize / 2);
        var startPoint = transform.position - centerOfArea;
        var endPoint = startPoint + (Vector3)Size * VoxelLength;

        return (voxelSize, startPoint, endPoint);
    }
}

using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class AirArea : MonoBehaviour
{
    [Header("Area parameters")]
    public Vector3Int Size;
    public float VoxelLength;

    [Header("Prefabs")]
    public AirVoxel AirVoxelPrefab;

    [Header("Gizmos")]
    public float BuoyancyEdge;

    public AirVoxel[,,] AirGrid { get; private set; }

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
        var startPoint = GetStartPointOfAirGrid();
        var cubeSize = VoxelLength * Vector3.one;

        for (var x = 0; x < Size.x; x++)
        {
            for (var y = 0; y < Size.y; y++)
            {
                for (var z = 0; z < Size.z; z++)
                {
                    if (AirGrid != null && AirGrid.Length > 0)
                    {
                        var greenColor = Mathf.Clamp(AirGrid[x, y, z].BuoyancyForce / BuoyancyEdge, 0, 1);
                        var redColor = Mathf.Clamp(1 - greenColor, 0, 1);

                        Gizmos.color = new Color(redColor, greenColor, 0, 1);
                    }

                    Gizmos.matrix = transform.localToWorldMatrix;
                    Gizmos.DrawWireCube(startPoint + VoxelLength * new Vector3(x, y, z), cubeSize);
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
        AirGrid = new AirVoxel[Size.x, Size.y, Size.z];

        var id = 0;
        var startPoint = GetStartPointOfAirGrid();

        for (var x = 0; x < Size.x; x++)
        {
            for (var y = 0; y < Size.y; y++)
            {
                for (var z = 0; z < Size.z; z++)
                {
                    AirGrid[x, y, z] = Instantiate(AirVoxelPrefab, startPoint + VoxelLength * new Vector3(x, y, z), Quaternion.identity, transform);
                    AirGrid[x, y, z].name = $"AirVoxel {id++}";
                    AirGrid[x, y, z].VoxelLength = VoxelLength;
                }
            }
        }

        Debug.Log($"{AirGrid.Length} air voxels generated");
    }

    private Vector3 GetStartPointOfAirGrid()
    {
        var voxelSize = Vector3.one * VoxelLength;
        var centerOfArea = Vector3.Scale(Size, voxelSize) / 2 - (voxelSize / 2);
        var startPoint = transform.position - centerOfArea;

        return startPoint;
    }
}

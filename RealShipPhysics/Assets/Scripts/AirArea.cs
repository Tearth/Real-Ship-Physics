using UnityEngine;

[ExecuteInEditMode]
public class AirArea : MonoBehaviour
{
    public Vector3 Size;

    void Start()
    {
        
    }

    void Update()
    {

    }

    void OnDrawGizmos()
    {
        var centerOfArea = (Size / 2) - new Vector3(0.5f, 0.5f, 0.5f);

        for (var x = 0; x < Size.x; x++)
        {
            for (var y = 0; y < Size.y; y++)
            {
                for (var z = 0; z < Size.z; z++)
                {
                    Gizmos.DrawWireCube(transform.position - centerOfArea + new Vector3(x, y, z), Vector3.one);
                }
            }
        }
    }
}

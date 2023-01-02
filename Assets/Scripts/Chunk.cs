using System.Linq;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [Tooltip("Increase of bounds constructed via the child objects via the given amount. This may save some recomputations when adding new childs during runtime.")]
    public Vector3Int buffer;
    private BoundsInt bounds;
    private Transform[,,] childs;
    private int prevChildCount;

    // Start is called before the first frame update
    void Start()
    {
        CalculateBoundsAndChilds();
    }

    private GameObject[] GetRelevantObjects()
    {
        var consumerObjects = this.GetComponentsInChildren<PowerConsumer>().Select(c => c.gameObject).ToArray();
        var routerObjects = this.GetComponentsInChildren<Router>().Select(c => c.gameObject).ToArray();
        return consumerObjects.Concat(routerObjects).Distinct().ToArray();
    }

    void CalculateBoundsAndChilds()
    {
        var relevantObjects = GetRelevantObjects();
        prevChildCount = relevantObjects.Length;

        Transform[] childTransforms = new Transform[relevantObjects.Length];
        Vector3Int[] voxelPositions = new Vector3Int[relevantObjects.Length];
        for (int i = 0; i < relevantObjects.Length; i++)
        {
            childTransforms[i] = relevantObjects[i].transform;
            var pos = Quaternion.Inverse(transform.rotation).normalized * childTransforms[i].position;
            voxelPositions[i] = Vector3Int.RoundToInt(pos);
        }
        var xMin = voxelPositions.Select(v => v.x).Min();
        var yMin = voxelPositions.Select(v => v.y).Min();
        var zMin = voxelPositions.Select(v => v.z).Min();

        var xMax = voxelPositions.Select(v => v.x).Max();
        var yMax = voxelPositions.Select(v => v.y).Max();
        var zMax = voxelPositions.Select(v => v.z).Max();

        var boundsMin = new Vector3Int(xMin, yMin, zMin);
        var boundsMax = new Vector3Int(xMax, yMax, zMax);
        bounds = new BoundsInt(boundsMin, boundsMax - boundsMin);

        childs = new Transform[bounds.size.x + 1 + 2 * buffer.x, bounds.size.y + 1 + 2 * buffer.y, bounds.size.z + 1 + 2 * buffer.z];
        for (int i = 0; i < childTransforms.Length; i++)
        {
            var transform = childTransforms[i];
            var voxelPos = voxelPositions[i];
            var index = voxelPos - bounds.min;
            childs[index.x, index.y, index.z] = transform;
        }
    }

    // Returns the GameObject at coords in grid space if any.
    public GameObject GameObjectAt(Vector3Int coords)
    {
        if (!IsInBounds(coords))
        {
            return null;
        }
        var index = coords - bounds.min;
        var childTransform = childs[index.x, index.y, index.z];
        if (childTransform == null)
        {
            return null;
        }
        return childTransform.gameObject;
    }

    // Returns the GameObject at coords in world space if any.
    public GameObject GameObjectAt(Vector3 coords)
    {
        var pos = Quaternion.Inverse(transform.rotation).normalized * coords;
        return GameObjectAt(Vector3Int.RoundToInt(pos));
    }

    public bool IsInBounds(Vector3Int coords)
    {
        // return this.bounds.Contains(coords);
        // for some reason the in-built method doesn't work like the one below
        return coords.x >= bounds.min.x && coords.x <= bounds.max.x &&
            coords.y >= bounds.min.y && coords.y <= bounds.max.y &&
            coords.z >= bounds.min.z && coords.z <= bounds.max.z;
    }

    public bool IsGloballyPowered(Vector3Int direction, PowerType powerType)
    {
        var check = -direction;
        var globalEmitters = transform.gameObject.GetComponents<GlobalEmitter>();
        foreach (var emitter in globalEmitters)
        {
            if (emitter.direction == check && emitter.powerType == powerType)
            {
                return true;
            }
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (prevChildCount != GetRelevantObjects().Length)
        {
            CalculateBoundsAndChilds();
        }
    }

    public int CountPower(int owner)
    {
        return this.GetComponentsInChildren<Ownable>()
                .Where(o => o.owner == owner && o.GetComponent<PowerConsumer>() != null)
                .Select(o => o.GetComponent<PowerConsumer>())
                .Where(pc => pc.bringsPoints)
                .Select(pc => pc.powerPoints)
                .Sum();
    }
}

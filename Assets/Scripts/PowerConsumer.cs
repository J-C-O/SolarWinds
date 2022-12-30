using UnityEngine;

public class PowerConsumer : MonoBehaviour
{
    [Tooltip("Whether the consumer generated points for the owner or not.")]
    public bool bringsPoints;
    [Tooltip("Type of the ray this consumer terminates. Use multiple consumers for terminating multiple types.")]
    public PowerType powerType;
    private Chunk chunk;
    public bool IsPowered;

    public void Start()
    {
        this.chunk = GetComponentInParent<Chunk>();
    }

    public void Update()
    {
        if (!bringsPoints)
        {
            // no need to raycast if no energy is counted
            return;
        }
        var raycaster = new Raycaster(chunk);
        var intPos = Vector3Int.RoundToInt(transform.position);
        var right = raycaster.Cast(intPos, Vector3Int.right, powerType);
        var left = raycaster.Cast(intPos, Vector3Int.left, powerType);
        var forward = raycaster.Cast(intPos, Vector3Int.forward, powerType);
        var back = raycaster.Cast(intPos, Vector3Int.back, powerType);
        var up = raycaster.Cast(intPos, Vector3Int.up, powerType);
        var down = raycaster.Cast(intPos, Vector3Int.down, powerType);
        IsPowered = right || left || forward || back || up || down;
        //Debug.Log("["+ gameObject.name +"]isPowered: " + IsPowered.ToString());
    }
}

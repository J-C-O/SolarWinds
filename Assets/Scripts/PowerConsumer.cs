using UnityEngine;

public class PowerConsumer : MonoBehaviour {
    public bool bringsPoints;
    public PowerType powerType;
    public int owner;
    private Chunk chunk;

    public void Start() {
        this.chunk = GetComponentInParent<Chunk>();
    }

    public void Update() {
        if (!bringsPoints) {
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
        var isPowered = right || left || forward || back || up || down;
        Debug.Log("isPowered: " + isPowered.ToString());
    }
}

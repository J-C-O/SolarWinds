using System;
using UnityEngine;

public class PowerConsumer : MonoBehaviour
{
    [Tooltip("Whether the consumer generated points for the owner or not.")]
    public bool bringsPoints;
    [Tooltip("Type of the ray this consumer terminates. Use multiple consumers for terminating multiple types.")]
    public PowerType powerType;
    private Chunk chunk;
    public int powerPoints;
    // continue raycasting
    public bool passthrough;

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
        int points = 0;
        points += Convert.ToInt32(raycaster.Cast(intPos, Vector3Int.right, powerType));
        points += Convert.ToInt32(raycaster.Cast(intPos, Vector3Int.left, powerType));
        points += Convert.ToInt32(raycaster.Cast(intPos, Vector3Int.forward, powerType));
        points += Convert.ToInt32(raycaster.Cast(intPos, Vector3Int.back, powerType));
        points += Convert.ToInt32(raycaster.Cast(intPos, Vector3Int.up, powerType));
        points += Convert.ToInt32(raycaster.Cast(intPos, Vector3Int.down, powerType));
        powerPoints = points;
        //Debug.Log("["+ gameObject.name +"]isPowered: " + IsPowered.ToString());
    }
}

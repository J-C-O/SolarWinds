using UnityEngine;

[RequireComponent(typeof(Chunk))]
public class GlobalEmitter : MonoBehaviour
{
    public Vector3Int direction;
    public PowerType powerType;
}

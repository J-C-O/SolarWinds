using UnityEngine;

[RequireComponent(typeof(Chunk))]
public class GlobalEmitter : MonoBehaviour
{
    [Tooltip("Direction of the emitted power rays. Only set one of X, Y or Z to 1 or -1. If multiple directions shall be emitted use multiple GlobalEmitters.")]
    public Vector3Int direction;
    [Tooltip("Power type emitted by this global emitter. If multiple types shall be emitted use multiple GlobalEmitters.")]
    public PowerType powerType;
}

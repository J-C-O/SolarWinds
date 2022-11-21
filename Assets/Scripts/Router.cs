using System;
using UnityEngine;

public class Router : MonoBehaviour
{
    [Tooltip("Directions of rays the flowing in. Multiple input directions are somewhat computionally expensive.")]
    public Directions inputDirections;
    [Tooltip("Directions of the rays flowing out.")]
    public Directions outputDirections;
    [Tooltip("Power type, which is consumed by this router. Use multiple routers if multiple power types shall be routed.")]
    public PowerType inputType;
    [Tooltip("Power type, which is output by this router. Use multiple routers if multiple power types shall be routed.")]
    public PowerType outputType;

    // http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetNaive
    private static int CountBits(uint value)
    {
        int count = 0;
        while (value != 0)
        {
            count++;
            value &= value - 1;
        }
        return count;
    }

    private static Vector3[] VectorsOf(Directions dirs)
    {
        var length = CountBits((uint)dirs);
        Vector3[] vecs = new Vector3[length];
        var index = 0;
        if (dirs.HasFlag(Directions.Right))
        {
            vecs[index] = Vector3.right;
            index++;
        }
        if (dirs.HasFlag(Directions.Left))
        {
            vecs[index] = Vector3.left;
            index++;
        }
        if (dirs.HasFlag(Directions.Forward))
        {
            vecs[index] = Vector3.forward;
            index++;
        }
        if (dirs.HasFlag(Directions.Back))
        {
            vecs[index] = Vector3.back;
            index++;
        }
        if (dirs.HasFlag(Directions.Up))
        {
            vecs[index] = Vector3.up;
            index++;
        }
        if (dirs.HasFlag(Directions.Down))
        {
            vecs[index] = Vector3.down;
            index++;
        }
        return vecs;
    }

    private static Vector3Int[] RotatedVectorsOf(Directions dirs, Quaternion rotation)
    {
        var vecs = Router.VectorsOf(dirs);
        var result = new Vector3Int[vecs.Length];
        for (int i = 0; i < vecs.Length; i++)
        {
            result[i] = Vector3Int.RoundToInt(rotation * vecs[i]);
            AssertDirectionVector(result[i]);
        }
        return result;
    }

    private static void AssertDirectionVector(Vector3Int vec) {
        var absDimensions = Math.Abs(vec.x) + Math.Abs(vec.y) + Math.Abs(vec.z);
        if (absDimensions != 1) {
            Debug.Log("Router direction vector absolut sum of components is not 1. This prevents further logic from functioning correctly. Use only 90 degree rotations on GameObjects with routers attached");
        }
    }

    public Vector3Int[] inputVectors
    {
        get
        {
            return Router.RotatedVectorsOf(inputDirections, transform.rotation);
        }
    }

    public Vector3Int[] outputVectors
    {
        get
        {
            return Router.RotatedVectorsOf(outputDirections, transform.rotation);
        }
    }

    public bool HasInputVector(Vector3Int coords) {
        foreach (var vec in inputVectors)
        {
            if (coords == vec) {
                return true;
            }
        }
        return false;
    }

    public bool HasOutputVector(Vector3Int coords) {
        foreach (var vec in outputVectors)
        {
            if (coords == vec) {
                return true;
            }
        }
        return false;
    }

    void Start()
    {

    }

    void Update()
    {
        
    }
}

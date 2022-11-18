using UnityEngine;

public class Raycaster
{
    private Chunk chunk;

    public Raycaster(Chunk chunk)
    {
        this.chunk = chunk;
    }

    // Returns whether start is powered by the given sort
    public bool Cast(Vector3Int start, Vector3Int initalDirection, PowerType powerType)
    {
        Vector3Int currentPos = start;
        while (true)
        {
            currentPos += initalDirection;
            if (!chunk.IsInBounds(currentPos))
            {
                // out of bounds
                return chunk.IsGloballyPowered(initalDirection, powerType);
            }
            var currentObj = chunk.GameObjectAt(currentPos);
            if (currentObj == null)
            {
                // air
                continue;
            }

            if (IsPoweredByRouters(currentObj, initalDirection, powerType)) {
                return true;
            }
            // TODO: a router currently does not consume power
            // that means that if directions do not match, the traversal
            // continues as if the router would have been air
            // The open question is if that behavior makes sense

            var currentConsumers = currentObj.GetComponents<PowerConsumer>();
            if (currentConsumers == null)
            {
                // no consumers, so we don't care
                continue;
            }
            foreach (var consumer in currentConsumers)
            {
                if (consumer.powerType == powerType)
                {
                    // there is a consumer of a relevant type in the ray direction
                    return false;
                }
            }
            // consumers of unrelevant types
            continue;
        }
    }

    public bool IsPoweredByRouters(GameObject obj, Vector3Int direction, PowerType powerType)
    {
        var currentRouters = obj.GetComponents<Router>();
        if (currentRouters == null)
        {
            return false;
        }
        foreach (var router in currentRouters)
        {
            if (router.outputType != powerType)
            {
                // powerType does not match so, ignore the router
                continue;
            }
            var check = -direction;
            if (!router.HasOutputVector(check))
            {
                // router has no output in the relevant direction, so ignore the router
                continue;
            }
            // router is relevant for us, so create a raycast for each of it's inputs
            foreach (var input in router.inputVectors)
            {
                Vector3Int pos = Vector3Int.RoundToInt(obj.transform.position);
                if (Cast(pos, -input, router.inputType))
                {
                    // router is powered and so are we
                    return true;
                }
            }
        }
        return false;
    }
}

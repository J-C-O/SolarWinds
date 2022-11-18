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
            // TODO: check emitter first to change direction of traversal
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
}

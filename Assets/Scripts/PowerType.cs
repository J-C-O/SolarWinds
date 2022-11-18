using System;

// TODO: likely this should not be flags
// and PowerConsumers in raycaster should be iterated
[Flags] public enum PowerType {
    None = 0,
    Solar = 1,
    Wind = 2,
}

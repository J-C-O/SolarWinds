using System;

public enum PowerType
{
    Solar = 0,
    Wind = 1,
}

[Flags]
public enum Directions : uint {
    None = 0,
    Left = 1,
    Right = 2,
    Forward = 4,
    Back = 8,
    Up = 16,
    Down = 32,
}

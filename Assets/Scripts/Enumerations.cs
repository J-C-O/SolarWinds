using System;

public enum PowerType
{
    Solar = 0,
    Wind = 1,
    EveryPower = 2
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

//Gamestates for papermodel implementation
public enum GameState
{
    //at the start the player should see the main menu
    GameStart,
    //then the main player should be able to register fellow players
    RegisterPlayer,
    //each player has n turns in a game
    PlayerTurn,
    PlayerTurnRandomCard,
    PlayerTurnPlayerAction,
    //pause menu
    GamePause,
    //at the end the player should see the main menu... again
    GameEnd,
    GameOptions,
    InventoryUpdate,
    RiddleDemoStart,
    UndefinedState
}

public enum PaperModelItem
{
    DefaultItem = 0,
    Solarzelle = 1,
    Windrad = 2,
    Spiegel = 4,
    Müll = 8,
    FridaysForFuture = 16,
    ChangeSunDirection = 32,
    ChangeWindDirection = 64,
    Ventilator = 128,
    Prisma = 256
}

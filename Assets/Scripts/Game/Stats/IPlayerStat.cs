namespace Game.Stats
{
    public delegate void PlayerStatChangeHandler(int id, float value);
    public interface IPlayerStat
    {
        Stat Config { get; }
        float Max { get; }
        float Value { get; }
        event PlayerStatChangeHandler OnValueChange;
    }
}
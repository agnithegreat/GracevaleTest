namespace Game
{
    public interface IGameModel
    {
        void Start(bool allowBuffs);
        void Attack(int attackerId);
    }
}
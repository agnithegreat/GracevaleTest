namespace Game.Buffs
{
    public class PlayerBuff : IPlayerBuff
    {
        private Buff _config;
        public Buff Config => _config;

        public PlayerBuff(Buff config)
        {
            _config = config;
        }
    }
}
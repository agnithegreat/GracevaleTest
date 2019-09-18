using System.Collections.Generic;
using Game.Buffs;
using Game.Stats;

namespace Game
{
    public delegate void PlayerAttackHandler();
    public delegate void PlayerHealthChangeHandler(float health, float max);
    public delegate void PlayerHealHandler(int delta);
    public delegate void PlayerDamageHandler(int delta);
    public delegate void PlayerFinalizeHandler();
    public interface IPlayerModel
    {
        int Id { get; }
        int Target { get; }
        bool IsDead { get; }
        IEnumerable<IPlayerStat> Stats { get; }
        float GetStat(int id);
        IEnumerable<IPlayerBuff> Buffs { get; }
        event PlayerAttackHandler AttackHandler;
        event PlayerHealthChangeHandler HealthChangeHandler;
        event PlayerHealHandler HealHandler;
        event PlayerDamageHandler DamageHandler;
        event PlayerFinalizeHandler FinalizeHandler;
    }
}
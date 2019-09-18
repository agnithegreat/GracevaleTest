using Game;
using Presenters;

namespace UI
{
    public interface IPlayerView
    {
        void Init(IPlayerPresenter presenter, IPlayerModel player);
        void UpdateStat(int stat, float value);
        void Attack();
        void UpdateHealth(float value, float max);
        void ShowHealthDelta(int delta);
    }
}
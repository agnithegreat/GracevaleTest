using Game;
using UI;

namespace Presenters
{
    public class PlayerPresenter : IPlayerPresenter
    {
        private IPlayerModel _model;
        private IPlayerView _view;

        private IGameModel _game;

        public PlayerPresenter(IGameModel game, IPlayerModel model, IPlayerView view)
        {
            _game = game;
            
            _model = model;
            _model.FinalizeHandler += OnFinalize;
            _model.AttackHandler += OnAttack;
            _model.HealthChangeHandler += OnHealthChange;
            _model.DamageHandler += OnDamage;
            _model.HealHandler += OnHeal;
            foreach (var playerStat in _model.Stats)
            {
                playerStat.OnValueChange += OnValueChange;
            }
            
            _view = view;
        }

        private void OnFinalize()
        {
            _view.Init(this, _model);
        }

        private void OnAttack()
        {
            _view.Attack();
        }

        private void OnValueChange(int id, float value)
        {
            _view.UpdateStat(id, value);
        }
        
        private void OnHealthChange(float health, float max)
        {
            _view.UpdateHealth(health, max);
        }

        private void OnDamage(int damage)
        {
            _view.ShowHealthDelta(damage);
        }
        
        private void OnHeal(int heal)
        {
            _view.ShowHealthDelta(heal);
        }

        void IPlayerPresenter.Attack()
        {
            _game.Attack(_model.Id);
        }
    }
}
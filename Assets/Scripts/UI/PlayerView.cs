using System.Collections.Generic;
using Game;
using Presenters;
using UnityEngine;

namespace UI
{
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        [SerializeField]
        private StatView _statPrefab;
        [SerializeField]
        private HealthBarView _healthPrefab;

        [SerializeField]
        private PlayerPanelHierarchy _panel;

        private IPlayerPresenter _presenter;
        
        private List<StatView> _stats = new List<StatView>();
        private List<StatView> _buffs = new List<StatView>();
        
        private IHealthBarView _healthBar;

        private void Awake()
        {
            _panel.attackButton.onClick.AddListener(OnAttackClick);

            _healthBar = Instantiate(_healthPrefab, transform);
        }

        private void LateUpdate()
        {
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(_panel.healthAnchor.transform.position);
            _healthBar.Transform.position = screenPoint;
        }

        private void OnAttackClick()
        {
            _presenter?.Attack();
        }

        private void Reset()
        {
            for (var i = 0; i < _stats.Count; i++)
            {
                Destroy(_stats[i].gameObject);
            }
            _stats.Clear();
            
            for (var i = 0; i < _buffs.Count; i++)
            {
                Destroy(_buffs[i].gameObject);
            }
            _buffs.Clear();
            
        }

        void IPlayerView.Init(IPlayerPresenter presenter, IPlayerModel player)
        {
            Reset();
            
            _presenter = presenter;
            
            foreach (var playerStat in player.Stats)
            {
                var statView = Instantiate(_statPrefab, _panel.statsPanel);
                statView.Init(playerStat.Config.icon);
                statView.SetValue(playerStat.Value);
                _stats.Add(statView);
            }

            foreach (var playerBuff in player.Buffs)
            {
                var buff = playerBuff.Config;
                var buffView = Instantiate(_statPrefab, _panel.statsPanel);
                buffView.Init(buff.icon, buff.title);
                _buffs.Add(buffView);
            }
        }

        void IPlayerView.UpdateStat(int stat, float value)
        {
            if (_stats.Count <= stat) return;
            _stats[stat].SetValue(value);
        }

        void IPlayerView.Attack()
        {
            _panel.character.SetTrigger("Attack");
        }

        void IPlayerView.UpdateHealth(float value, float max)
        {
            _panel.character.SetInteger("Health", (int) value);
            _healthBar.UpdateBar(value / max);
        }

        void IPlayerView.ShowHealthDelta(int delta)
        {
            if (delta == 0) return;
            _healthBar.ShowDelta(delta);
        }
    }
}
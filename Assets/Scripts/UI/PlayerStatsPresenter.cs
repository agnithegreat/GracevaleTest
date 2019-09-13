using System.Collections.Generic;
using Game.Common;
using UnityEngine;

namespace UI
{
    public class PlayerStatsPresenter : MonoBehaviour
    {
        [SerializeField]
        private StatView _prefab;
        
        [SerializeField]
        private PlayerPanelHierarchy _panel;
        
        private List<StatView> _stats = new List<StatView>();
        private List<StatView> _buffs = new List<StatView>();

        private IPlayerState<float> _statsData;
        private IPlayerState<int> _buffsData;

        public void Init(IPlayerState<float> stats, IPlayerState<int> buffs, Data config)
        {
            _statsData = stats;
            _buffsData = buffs;

            for (var i = 0; i < config.stats.Length; i++)
            {
                var statView = Instantiate(_prefab, _panel.statsPanel);
                statView.Init(config.stats[i].icon);
                _stats.Add(statView);
            }

            if (_buffsData == null) return;
            
            for (var i = 0; i < _buffsData.Count; i++)
            {
                var buff = config.buffs[_buffsData.GetValue(i)];
                var buffView = Instantiate(_prefab, _panel.statsPanel);
                buffView.Init(buff.icon, buff.title);
                _buffs.Add(buffView);
            }
        }

        private void Update()
        {
            for (var i = 0; i < _statsData.Count; i++)
            {
                _stats[i].SetValue(_statsData.GetValue(i));
            }
            
            _panel.character.SetInteger("Health", (int) _statsData.GetValue(StatsId.LIFE_ID));
        }

        public void Attack()
        {
            _panel.character.SetTrigger("Attack");
        }

        public void Reset()
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
    }
}
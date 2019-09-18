using Presenters;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameView : MonoBehaviour, IGameView
    {
        [SerializeField]
        private Button _startWithBuffsButton;
        [SerializeField]
        private Button _startWithoutBuffsButton;
        
        private IGamePresenter _presenter;

        private void Awake()
        {
            _startWithBuffsButton.onClick.AddListener(OnStartWithBuffsClick);
            _startWithoutBuffsButton.onClick.AddListener(OnStartWithoutBuffsClick);
        }

        private void OnStartWithBuffsClick()
        {
            _presenter?.StartGame(true);
        }

        private void OnStartWithoutBuffsClick()
        {
            _presenter?.StartGame(false);
        }

        void IGameView.Init(IGamePresenter presenter)
        {
            _presenter = presenter;
        }
    }
}
using Game;
using UI;

namespace Presenters
{
    public class GamePresenter : IGamePresenter
    {
        private IGameModel _model;
        private IGameView _view;

        public GamePresenter(IGameModel model, IGameView view)
        {
            _model = model;
            
            _view = view;
            _view.Init(this);
        }
        
        void IGamePresenter.StartGame(bool allowBuffs)
        {
            _model.Start(allowBuffs);
        }
    }
}
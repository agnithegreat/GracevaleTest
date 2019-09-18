using UnityEngine;

namespace UI
{
    public interface IHealthBarView
    {
        Transform Transform { get; }
        void UpdateBar(float progress);
        void ShowDelta(int delta);
    }
}
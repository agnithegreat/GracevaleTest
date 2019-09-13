using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StatView : MonoBehaviour
    {
        private const string IconsFolder = "Icons/";
        
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private Text _text;

        public void Init(string icon)
        {
            _icon.sprite = Resources.Load<Sprite>(IconsFolder + icon);
        }
        
        public void Init(string icon, string text)
        {
            _icon.sprite = Resources.Load<Sprite>(IconsFolder + icon);
            _text.text = text;
        }

        public void SetValue(float value)
        {
            _text.text = "" + value;
        }
    }
}
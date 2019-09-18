using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBarView : MonoBehaviour, IHealthBarView
    {
        [SerializeField]
        private Image _fill;
        [SerializeField]
        private Text _damagePrefab;

        private void Awake()
        {
            _damagePrefab.gameObject.SetActive(false);
        }

        public Transform Transform => gameObject.transform;

        void IHealthBarView.UpdateBar(float progress)
        {
            _fill.fillAmount = progress;
        }

        void IHealthBarView.ShowDelta(int delta)
        {
            var damageInst = Instantiate(_damagePrefab, _damagePrefab.transform.parent);
            damageInst.text = (delta < 0 ? "-" : "") + Math.Abs(delta);
            damageInst.color = delta < 0 ? Color.red : Color.green;
            damageInst.gameObject.SetActive(true);
            StartCoroutine(TweenDamage(damageInst));
        }

        private IEnumerator TweenDamage(Text damage)
        {
            var time = 1f;
            while (time > 0)
            {
                yield return new WaitForEndOfFrame();
                time -= Time.deltaTime;
                damage.transform.position += 100f * Time.deltaTime * Vector3.up;
            }
            Destroy(damage.gameObject);
        }
    }
}
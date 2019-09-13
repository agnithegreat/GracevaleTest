using UnityEngine;
using Random = UnityEngine.Random;

namespace UI
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private Transform _cameraAnchor1;
        [SerializeField]
        private Transform _cameraAnchor2;
        [SerializeField]
        private Camera _camera;

        private CameraModel _config;

        private float _nextFovChangeTimeRemaining;
        private float _fovTweenTimeRemaining;
        private float _targetFov;
        private float _previousFov;
        
        public void Init(CameraModel config)
        {
            _config = config;
            
            _camera.transform.localPosition = new Vector3(0f, _config.height, _config.roundRadius);
            _camera.transform.LookAt(Vector3.zero + Vector3.up * _config.lookAtHeight);
        }

        private void Update()
        {
            _nextFovChangeTimeRemaining -= Time.deltaTime;
            _fovTweenTimeRemaining -= Time.deltaTime;
            
            if (_nextFovChangeTimeRemaining <= 0)
            {
                _previousFov = _camera.fieldOfView;
                _targetFov = Random.Range(_config.fovMin, _config.fovMax);
                _fovTweenTimeRemaining = _config.fovDuration;
                _nextFovChangeTimeRemaining = _config.fovDelay;
            }
        }

        private void LateUpdate()
        {
            _cameraAnchor1.transform.eulerAngles += new Vector3(0, 360f, 0) * Time.deltaTime / _config.roundDuration;

            if (_fovTweenTimeRemaining > 0)
            {
                _camera.fieldOfView = Mathf.Lerp(_previousFov, _targetFov, 1f - _fovTweenTimeRemaining / _config.fovDuration);
            }
        }
    }
}
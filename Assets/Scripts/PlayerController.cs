using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OneHourJam458
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance;

        [SerializeField]
        private GameObject _bullet;

        [SerializeField]
        private Transform _enemy;

        [SerializeField]
        private GameObject _winTExt;

        private SpriteRenderer _sr;

        private float _heatCounter;

        private Vector2 _mov;

        private Rigidbody2D _rb;

        private bool _canShoot = true;
        private bool _isShooting;

        private Camera _cam;

        private bool _didWin;

        private int _clothCount = 5;
        public void LooseCloth()
        {
            _clothCount--;

            if (_clothCount == 0 )
            {
                _didWin = true;
                _winTExt.SetActive(true);
            }
        }

        private void Awake()
        {
            Instance = this;
            _cam = Camera.main;
            _rb = GetComponent<Rigidbody2D>();
            _sr = GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            _rb.velocity = _mov * 5f;
        }

        // http://answers.unity.com/answers/502236/view.html
        public Bounds CalculateBounds()
        {
            float screenAspect = Screen.width / (float)Screen.height;
            float cameraHeight = _cam.orthographicSize * 2;
            Bounds bounds = new(
                _cam.transform.position,
                new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
            return bounds;
        }

        private void Update()
        {
            var b = CalculateBounds();

            if (_isShooting && _canShoot)
            {
                var go = Instantiate(_bullet, transform.position, Quaternion.identity);
                go.GetComponent<Rigidbody2D>().velocity = Vector2.up * 10f;
                Destroy(go, 10f);
                StartCoroutine(Reload());
            }

            if (_didWin) return;

            var d = Vector2.Distance(_enemy.position, transform.position);
            var rD = 5f;

            if (transform.position.x < b.min.x || transform.position.x > b.max.x || transform.position.y < b.min.y || transform.position.y > b.max.y)
            {
                _heatCounter += 1f * Time.deltaTime;
                var v = Mathf.Clamp01(_heatCounter);
                _sr.color = new(1f - v, 1f - v, v);
                if (_heatCounter >= 1f)
                {
                    GameManager.Instance.Loose();
                    Destroy(gameObject);
                }

            }

            if (d < rD)
            {
                _heatCounter += ((rD - d) / rD) * Time.deltaTime * .5f;
                var v = Mathf.Clamp01(_heatCounter);
                _sr.color = new(1f - v, 1f - v, v);
                if (_heatCounter >= 1f)
                {
                    GameManager.Instance.Loose();
                    Destroy(gameObject);
                }
            }
            else
            {
                _heatCounter -= .1f * Time.deltaTime;
                if (_heatCounter < 0f) _heatCounter = 0f;
                var v = Mathf.Clamp01(_heatCounter);
                _sr.color = new(1f - v, 1f - v, v);
            }
        }

        public void OnMove(InputAction.CallbackContext value)
        {
            _mov = value.ReadValue<Vector2>();
        }

        public void OnShoot(InputAction.CallbackContext value)
        {
            if (value.phase == InputActionPhase.Started) _isShooting = true;
            else if (value.phase == InputActionPhase.Canceled) _isShooting = false;
        }

        private IEnumerator Reload()
        {
            _canShoot = false;
            yield return new WaitForSeconds(.1f);
            _canShoot = true;
        }
    }
}

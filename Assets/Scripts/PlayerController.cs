using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OneHourJam458
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _bullet;

        private Vector2 _mov;

        private Rigidbody2D _rb;

        private bool _canShoot = true;
        private bool _isShooting;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rb.velocity = _mov * 5f;
        }

        private void Update()
        {
            if (_isShooting && _canShoot)
            {
                var go = Instantiate(_bullet, transform.position, Quaternion.identity);
                go.GetComponent<Rigidbody2D>().velocity = Vector2.up * 10f;
                Destroy(go, 10f);
                StartCoroutine(Reload());
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

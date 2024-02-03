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

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rb.velocity = _mov * 5f;
        }

        public void OnMove(InputAction.CallbackContext value)
        {
            _mov = value.ReadValue<Vector2>();
        }

        public void OnShoot(InputAction.CallbackContext value)
        {
            if (value.performed)
            {
                var go = Instantiate(_bullet, transform.position, Quaternion.identity);
                go.GetComponent<Rigidbody2D>().velocity = Vector2.up * 10f;
            }
        }
    }
}

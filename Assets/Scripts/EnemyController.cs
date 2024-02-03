using UnityEngine;

namespace OneHourJam458
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private NextNode _start;

        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rb.velocity = (_start.transform.position - transform.position).normalized * 3f;

            if (Vector2.Distance(transform.position, _start.transform.position) < .1f)
            {
                _start = _start.Next[Random.Range(0, _start.Next.Length)];
            }
        }
    }
}

using UnityEngine;

namespace OneHourJam458
{
    public class Bullet : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            collision.gameObject.GetComponent<Cloth>().TakeDamage();
            Destroy(gameObject);
        }
    }
}

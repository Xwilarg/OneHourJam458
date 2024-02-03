using UnityEngine;

namespace OneHourJam458
{
    public class DCloth : MonoBehaviour
    {
        [SerializeField]
        private int _health;

        public void TakeDamage()
        {
            _health--;
            if (_health == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

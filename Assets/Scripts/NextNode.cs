using UnityEngine;

namespace OneHourJam458
{
    public class NextNode : MonoBehaviour
    {
        public NextNode[] Next;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;

            foreach (var n in Next)
            {
                Gizmos.DrawLine(transform.position, n.transform.position);
            }
        }
    }
}

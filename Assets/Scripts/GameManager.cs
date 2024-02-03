using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OneHourJam458
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { private set; get; }

        private void Awake()
        {
            Instance = this;
        }

        public void Loose()
        {
            StartCoroutine(LooseCoroutine());
        }

        private IEnumerator LooseCoroutine()
        {
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("Disclaimer");
        }
    }
}

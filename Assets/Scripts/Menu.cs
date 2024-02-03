using UnityEngine;
using UnityEngine.SceneManagement;

namespace OneHourJam458
{
    public class Menu : MonoBehaviour
    {
        public void LoadGame()
        {
            SceneManager.LoadScene("Main");
        }
    }
}


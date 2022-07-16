using UnityEngine;
using UnityEngine.SceneManagement;

namespace scrips.UI
{
    public class ButtonManager : MonoBehaviour
    {
        public Animator anim;

        public void OnPressedPlay()
        {
            anim.Play("levelSelectSwitch");
        }

        public void OnPressedBack()
        {
            anim.Play("levelSelectSwitchBack");
        }

        public void OnPressedQuit()
        {
            Application.Quit();
        }

        public void LevelLoad(int level)
        {
            SceneManager.LoadScene("Level_" + level);
        }

        public void StartScreenLoad()
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
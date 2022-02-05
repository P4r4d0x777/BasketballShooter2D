using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    private Animator mainAnim, ballAnim;

    private void Awake()
    {
        mainAnim = GameObject.Find("Main Holder").GetComponent<Animator>();
        ballAnim = GameObject.Find("Balls Holder").GetComponent<Animator>();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void SelectBall()
    {
        mainAnim.Play("FadeOut");
        ballAnim.Play("FadeIn");
    }

    public void BackToMenu()
    {
        mainAnim.Play("FadeIn");
        ballAnim.Play("FadeOut");
    }
}

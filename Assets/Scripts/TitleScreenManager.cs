using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    [SerializeField] AudioSource buttonAudio;

    public void StartGame()
    {
        ReproduceClip();
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
        StartCoroutine(ExitG());
    }

    public void ReproduceClip()
    {
        buttonAudio.Play();
    }

    IEnumerator ExitG()
    {
        ReproduceClip();
        yield return new WaitForSeconds(0.5F);
        //Si esta en el editor de unity se cierra de esta forma
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
        //Si no esta en el editor de unity se pueden agregar otras formas, dependiendo de la plataforma.
        //https://docs.unity3d.com/Manual/PlatformDependentCompilation.html
            Application.Quit();
        #endif
    }

}

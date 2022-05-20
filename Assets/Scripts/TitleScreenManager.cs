using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] int time = 30;
    //Range limita la variable para que tenga valores de 1 a 10. Aparece un slider en unity
    [Range(1, 10)][SerializeField] float spawnRate = 1;

    public int difficulty = 1;

    private void Awake() {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        while(time > 0)
        {
            yield return new WaitForSeconds(1/spawnRate);
            time--;
        }

        //Game Over
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int gameMode;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       // gameMode = 4;
    }

    /**run this on the home menu buttons, feeding in the correct game type
     * 1= AddGame
     * 2= sub
     * 3= multi
     * 4= combo/mixed
     * **/

    public void ChangeGameMode(int mode)
    {
        gameMode = mode;
    }

}

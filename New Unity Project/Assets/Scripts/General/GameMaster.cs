using UnityEngine;

public class GameMaster : MonoBehaviour
{

    public static GameMaster instance;
    public Vector2 lastCheckPointPosition;
    public Vector2 savePointPosition;
    public GameObject playerInstance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
            Destroy(gameObject);
        playerInstance = GameObject.FindGameObjectWithTag("Player");
    }

}

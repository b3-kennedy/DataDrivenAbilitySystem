using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public enum GameState {PICK, PLAY};
    
    GameState state;

    public GameState GetState()
    {
        return state;
    }

    public void SetState(GameState newState)
    {
        state = newState;
        if(newState == GameState.PICK)
        {
            UIManager.Instance.OnPickState();
        }
        else if(newState == GameState.PLAY)
        {
            UIManager.Instance.OnPlayState();
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetState(GameState.PICK);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

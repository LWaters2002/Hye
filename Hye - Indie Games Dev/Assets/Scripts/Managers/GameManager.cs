using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public Transform playerSpawnpoint;

    [SerializeField] private PlayerController playerController;

    public PlayerController player { get; private set; }
    private UIManager UImanager;

    public string scenePrefix;
    public string[] sceneTypes;

    private List<string> loadedScenes;

    // Start is called before the first frame update
    void Awake()
    {

        loadedScenes = new List<string>();
        
        //Scene Management and Collections
        SceneManager.sceneLoaded += SceneLoaded;
        SceneManager.sceneUnloaded += SceneUnloaded;

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            loadedScenes.Add(SceneManager.GetSceneAt(i).name);
        }

        foreach (string s in sceneTypes)
        {
            if (!loadedScenes.Contains(scenePrefix + s))
            {
                SceneManager.LoadScene(scenePrefix + s, LoadSceneMode.Additive);
            }
        }

        //Init Chain Start
        player = playerController;
        player.Setup(this);

        //Subscribing to events
        player.playerStats.OnDeath += OnDeath;
    }


    private void OnDeath()
    {
        SceneManager.UnloadSceneAsync(scenePrefix + "Master");
        SceneManager.LoadScene(scenePrefix + "Master", LoadSceneMode.Additive);
    }

    //Dynamically adds or removes scnes from active scene lists.
    private void SceneLoaded(Scene s, LoadSceneMode lsm)
    {
        loadedScenes.Add(s.name);
        //Get items that need setup
        IManagable[] mScripts = FindObjectsOfType<MonoBehaviour>().OfType<IManagable>().ToArray();

        foreach (IManagable sc in mScripts)
        {
            sc.Setup(this);
        }
    }

    private void SceneUnloaded(Scene s)
    {
        loadedScenes.Remove(s.name);
    }
}


using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayInSecond = 1f;
    [SerializeField] AudioClip successAudio;
    [SerializeField] AudioClip HitAudio;

    [SerializeField] ParticleSystem successParticel;
    [SerializeField] ParticleSystem HitParticel;
    [SerializeField] ParticleSystem LandingParticel;



    AudioSource audioSource;

    bool deadLevel = false;
    bool CollisionDisabled = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKey();
    }

    void RespondToDebugKey()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            CollisionDisabled = !CollisionDisabled;
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (deadLevel || CollisionDisabled)
        {
            return;
        }
        switch (other.gameObject.tag)
        {
            case "Start":
                Debug.Log("this thing is frindly!");
                break;
            case "Finish":
                startSuccessSequance();
                break;
            case "Fuel":
                Debug.Log("OH, you touch the fuel!");
                break;
            default:
                startCrashSequance();
                break;
        }
    }


    void startSuccessSequance()
    {
        deadLevel = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successAudio);
        successParticel.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delayInSecond);
        LandingParticel.Stop();
    }

    void startCrashSequance()
    {
        deadLevel = true;
        audioSource.Stop();
        audioSource.PlayOneShot(HitAudio);
        HitParticel.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("RelodLevel", delayInSecond);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);

    }

    void RelodLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}

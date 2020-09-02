using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    AudioSource audioSource;
    enum State { Live, Dying, Transcending };
    State state = State.Live;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip deadSound;
    [SerializeField] AudioClip levelComplete;


    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem successParticles;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Live)
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if(state != State.Live)
        {
            return;
        }
        switch(collision.gameObject.tag)
        {
            case "Friendly":
                print("OK");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        mainEngineParticles.Stop();
        deathParticles.Play();
        audioSource.PlayOneShot(deadSound);
        Invoke("LoadFirstLevel", 2f);
    }

    private void StartSuccessSequence()
    {
        state = State.Transcending;
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        audioSource.PlayOneShot(levelComplete);
        mainEngineParticles.Stop();
        successParticles.Play();
        Invoke("LoadNextLevel", 2f);
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
        state = State.Live;
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            audioSource = GetComponent<AudioSource>();
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
                mainEngineParticles.Play();
            }
        }
        else
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void RespondToRotateInput()
    {
        rigidbody.freezeRotation = true;
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
        rigidbody.freezeRotation = false;
    }
}

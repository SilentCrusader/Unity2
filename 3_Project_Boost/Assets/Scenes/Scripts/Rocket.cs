using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    //Serialize field allows parameters to be accessible via the inspector
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float rcsRotate = 100f;
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip deathExplosion;
    [SerializeField] AudioClip goal;    
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem deathExplosionParticles;
    [SerializeField] ParticleSystem goalParticles;

    //Declare components
    Rigidbody rigidBody;
    AudioSource audioSource;    

    //Declare and initialize state of GameObject
    enum State { Alive, Dying, Transcending };
    State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(state == State.Alive)
        {
            Thrust();
            Rotate();
        }        
    }

    void Thrust()
    {

        //Formula to accomodate frame rate of any device
        float thrustFrame = rcsThrust * Time.deltaTime;                

        if(Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * thrustFrame); //manipulate the y plane
            
            mainEngineParticles.Play();

            //Required so that sounds aren't overplaying each other to make a strange reverberation of all sounds
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }            
        }
        
        else
        {
            mainEngineParticles.Stop();
            audioSource.Stop();            
        }
    }
    
    void Rotate()
    {
        rigidBody.freezeRotation = true;
                
        float rotationFrame = rcsRotate * Time.deltaTime; //formula to accomodate frame rate of any device    

        if(Input.GetKey(KeyCode.A))
        {
            
            transform.Rotate(Vector3.forward * rotationFrame); //rotate on z plane
        }
        else if(Input.GetKey(KeyCode.D))
        {            
            transform.Rotate(-Vector3.forward * rotationFrame); //rotate on z plane
        }

        rigidBody.freezeRotation = false;
    }

    void OnCollisionEnter(Collision collision) 
    {
        
        //Conditional logic to ignore collisions after dead or goal is reached
        if(state != State.Alive)
        {
            return;
        }
        
        switch (collision.gameObject.tag)
        {
            case "Friendly":                
                break;
            
            case "Finish":
                state = State.Transcending;
                audioSource.Stop();                
                goalParticles.Play();
                audioSource.PlayOneShot(goal);
                Invoke("LoadNextLevel", levelLoadDelay);                
                break;
            
            default:
                state = State.Dying;
                audioSource.Stop();
                audioSource.PlayOneShot(deathExplosion);
                deathExplosionParticles.Play();
                Invoke("LoadFirstLevel", levelLoadDelay);
                break;
        }
    }

    private void LoadNextLevel()
    {
        
        int sceneTracker = SceneManager.GetActiveScene().buildIndex;        
        SceneManager.LoadScene(sceneTracker + 1);
    }
    
    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }
}

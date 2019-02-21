using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    [SerializeField] float rcsThrust = 150f;
    [SerializeField] float mainThrust = 25f;

    Rigidbody rigidBody;
    AudioSource audioSource;

    enum State {Alive, Dying, Tanscending};
    State state = State.Alive;
   
    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        //todo fix audio when not alive
        if (state == State.Alive) {
            Thrust();
            Rotate();
        }
	}

    void OnCollisionEnter(Collision collision)
    {

        if (state != State.Alive) { return; } ;
        

        switch (collision.gameObject.tag) {
            case "Friendly":
                //Do nothing
                break;
            case "Finish":
                state = State.Tanscending;
                Invoke("LoadNextLevel", 1f); //todo parameritize time
                break;
            default:
                print("DEAD");
                state = State.Dying;
                Invoke("LoadFirstLevel", 1f); //todo parameritize time
                break;
        }
    }

    private  void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    //TODO allow for more than 2 levels
    private  void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //float thrustThisFrame = mainThrust * Time.deltaTime;
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }

        }
        else
        {
            audioSource.Stop();
        }
    }

    private void Rotate()
    {

        rigidBody.freezeRotation = true; // take manual control of rotation.

        
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidBody.freezeRotation = false; // Resume physics control of rotation
    }

}

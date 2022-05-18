using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    GameManager gameManager;
    Rigidbody _rigidBody;
    private float ySpawnPos = -2;
    private float xSpawnRange = 4;
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    [SerializeField] int pointValue;
    [SerializeField] ParticleSystem explosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        _rigidBody = GetComponent<Rigidbody>();

        //On spawn, place somewhere at the bottom of the screen
        transform.position = RandomSpawnPos();

        //Add random forces to make it jump and spin
        _rigidBody.AddForce(RandomForce(), ForceMode.Impulse);
        _rigidBody.AddTorque(new Vector3(RandomTorque(), RandomTorque(), RandomTorque()), ForceMode.Impulse);

        //Make '?' box a random score
        if(pointValue == 15)
        {
            pointValue = Random.Range(-5, 20);
        }
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomForce()
    {
        return (Vector3.up * Random.Range(minSpeed, maxSpeed));
    }

    Vector3 RandomSpawnPos()
    {
         return new Vector3(Random.Range(-xSpawnRange, xSpawnRange), ySpawnPos);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if(!gameManager.isGameOver)
        {
            Destroy(gameObject);
            gameManager.UpdateScore(pointValue);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}

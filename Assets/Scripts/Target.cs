using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    private Rigidbody rb;
    private GameManager gameManager;
    public int pointValue;
    public ParticleSystem explosionParticle;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(GenerateRandomUpwardForce(), ForceMode.Impulse);
        rb.AddTorque(GenerateRandomTorque(), GenerateRandomTorque(), GenerateRandomTorque(), ForceMode.Impulse);
        rb.transform.position = GenerateRandomPosition();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 GenerateRandomUpwardForce()
    {
        return Random.Range(10, 15) * Vector3.up;
    }

    Vector3 GenerateRandomPosition()
    {
        return new Vector3(Random.Range(-4.5f, 4.5f), -1, 0);
    }

    float GenerateRandomTorque()
    {
        return Random.Range(-10, 10);
    }

    public void SliceTarget()
    {


    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad") && other.gameObject.CompareTag("Sensor"))
        {
            gameManager.DecreaseLife();
        }
        else if (other.CompareTag("Trail"))
        {
            if (gameManager.isGameActive && Time.timeScale == 1)
            {
            gameManager.UpdateScore(pointValue);
            Instantiate(explosionParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
            if (gameObject.CompareTag("Bad"))
            {
                gameManager.DecreaseLife();
            }
            }
        }
        
    }
}

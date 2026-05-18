using UnityEngine;
using System.Collections;

public class WanderingAI : MonoBehaviour {
	public float speed = 3.0f;
	public float obstacleRange = 5.0f;
	public float speed2 = 0.1f;
	
	[SerializeField] private GameObject fireballPrefab;
	private GameObject _fireball;
	
	private bool _alive;
	
	void Start() {
		_alive = true;
        transform.Rotate(0, 0, 0);
    }
	
	void Update() {
		if (_alive) {
			transform.Translate(speed2 * Time.deltaTime, 0, speed * Time.deltaTime);
			
            
            
				

        }
	}

	void OnCollisionEnter(Collision collision)
	{
        if (collision.gameObject.tag == "InvisibleWall")
        {
            float angle = Random.Range(-10, 10);
            transform.Rotate(0, angle, 0);
			Debug.Log("mentok");
        }

    }
	public void SetAlive(bool alive) {
		_alive = alive;
	}
}

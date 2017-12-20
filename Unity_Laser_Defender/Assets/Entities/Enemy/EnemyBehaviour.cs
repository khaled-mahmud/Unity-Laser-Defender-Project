using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
	public GameObject projectile;
	public float projectileSpeed = 10f;
	public float health = 150f;
	public float shotsPerSeconds = 0.5f;
	public int scoreValue = 150;

	public AudioClip fireSound;
	public AudioClip deathSound;

	private ScoreKeeper scorekeeper;

	void Start ()
	{
		scorekeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
	}

	void Update ()
	{
		float probability = Time.deltaTime * shotsPerSeconds;
		if (Random.value < probability) {
			Fire();
		}

	}

	void Fire ()
	{
		Vector3 startPosition = transform.position + new Vector3(0, -1, 0);
		GameObject missile = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
		missile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}

	void OnTriggerEnter2D (Collider2D collider)
	{
		Projectile missile = collider.gameObject.GetComponent<Projectile> ();
		if (missile) {
			health -= missile.GetDamage ();
			missile.Hit();
			if (health <= 0) {
				Die();
			}
		}
	}

	void Die ()
	{
		AudioSource.PlayClipAtPoint(deathSound, transform.position);
		Destroy(gameObject);
		scorekeeper.Score(scoreValue);
	}
}

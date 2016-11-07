using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class C : MonoBehaviour {

	private bool facingRight = true;

	public int maxPlatforms = 15;
	public GameObject platform;
	public float horizontalMin = 5f;
	public float horizontalMax = 10f;
	public float verticalMin = -1f;
	public float verticalMax = 1f;

	private Vector2 originPosition;

	public Animator anim;
	//private float offsetx;
	//private float offsety;

	private bool isGrounded;
	private float radius = 1.5f;
	private float force = 250;
	public Transform groundPoint;
	public LayerMask ground;
	public AudioSource audioSource;
	public AudioClip jump;
	public AudioClip point;
	public AudioClip win;
	public AudioClip gun;
	public TextMesh text;
	public TextMesh score;

	public GameObject b;

	public int diamonds;
	public float movespeed_r;
	public float movespeed_l;


	void Start () 
	{
		movespeed_r = 10f;
		movespeed_l = 10f;
		originPosition = b.transform.position;
		//offsetx = text.transform.position.x - transform.position.x;
		//offsety = text.transform.position.y - transform.position.y;
		Spawn ();
	}

	void Spawn () 
	{
		for (int i=0; i<maxPlatforms; i++)
		{
			Vector2 randomPosition = originPosition + new Vector2 (Random.Range (horizontalMin, horizontalMax), Random.Range (verticalMin, verticalMax));
			Instantiate (platform, randomPosition, Quaternion.identity);
			originPosition = randomPosition;
		}
	}
	void Update()
	{
		isGrounded = Physics2D.OverlapCircle (groundPoint.position, radius, ground);
		if (isGrounded) {
			if (Input.GetKey(KeyCode.Space)) {				
				GetComponent<Rigidbody2D> ().AddForce (Vector2.up * force);
				audioSource.clip = jump;
				audioSource.Play ();
			}
		}

		if (Input.GetKey (KeyCode.RightArrow)) 
		{
			transform.Translate (movespeed_r * Input.GetAxis ("Horizontal") * Time.deltaTime, 0f,0f);
			if (!facingRight) {
				Flip ();
			}

		}
		else if (Input.GetKey (KeyCode.LeftArrow)) 
		{
			transform.Translate (movespeed_l * Input.GetAxis ("Horizontal") * Time.deltaTime, 0f, 0f);
			if (facingRight) {
				Flip ();
			}

		}


		if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)){
			anim.enabled = true;
		}
		else
			anim.enabled = false;

		
	}

	void LateUpdate()
	{
		//score.transform.position = new Vector2 (transform.position.x + offsetx + 4f, transform.position.y + offsety);
		//text.transform.position = new Vector2 (transform.position.x + offsetx , transform.position.y + offsety);
	}

	public void Reset()
	{
		SceneManager.LoadScene("Level 1");
	}

	void OnTriggerEnter2D (Collider2D collider2D)
	{
		if (collider2D.tag == "Diamond") {
			diamonds++;
			score.text = diamonds + "";
			Destroy (collider2D.gameObject);
			audioSource.clip = point;
			audioSource.Play ();
		} 

		else if (collider2D.tag == "Deadzone") {

			Reset ();

		} 
		else if (collider2D.tag == "Finish") {

			SceneManager.LoadScene ("Level 1.5");
		}

	}
	void Flip()
	{
		facingRight = !facingRight;
		Vector2 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
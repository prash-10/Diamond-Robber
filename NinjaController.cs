using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NinjaController : MonoBehaviour {

	private float min;
	private float sec;

	public bool facingRight = true;
	private bool isGrounded;
	private float radius = 0.7f;
	private float force = 250;
	public Transform groundPoint;
	public LayerMask ground;
	public AudioSource audioSource;
	public AudioClip Jump;
	public AudioClip Diamond;
	public AudioClip Win;

	//public Animator animator;
	//public int diamonds;

	public float timeLeft = 300.0f;
	public TextMesh time;
	public int point=5;
	public TextMesh score;
	public TextMesh points;
	public float movespeed_r;
	public GameObject Enemy;
	//private GameObject e;

	void Start ()
	{
		movespeed_r = 10f;
		//e = Enemy.transform.GetChild (0).gameObject;
		startTimer (timeLeft);
	}

	void Update () 
	{
		isGrounded = Physics2D.OverlapCircle (groundPoint.position, radius, ground);
		if (isGrounded) {
			if (Input.GetKey(KeyCode.Space)) {				
				transform.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * force);
				audioSource.clip = Jump;
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
			transform.Translate (movespeed_r * Input.GetAxis ("Horizontal") * Time.deltaTime, 0f, 0f);
			if (facingRight) {
				Flip ();
			}
		}	

		timeLeft -= Time.deltaTime;

		if (timeLeft == 0 && point != 0) {
			print ("You Won");
			Time.timeScale = 0;
		}
		if (point == 0) {
			print ("You Lost");
			SceneManager.LoadScene ("Level 0");
		}
		min = Mathf.Floor (timeLeft / 60);
		sec = timeLeft % 60;
		if (sec > 59)
			sec = 59;
		if (min < 0) {
			min = 0;
			sec = 0;
		}

	}
		
	void startTimer(float x)
	{
		
		timeLeft = x;
		Update ();
		StartCoroutine (updateTime ());
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.transform.tag == "Bullets") 
		{
			print (point);
			//if (point == -1)
			//	SceneManager.LoadScene ("Level 3");
		

				point--;
				points.text = point + "";

		}
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector2 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	private IEnumerator updateTime()
	{
		while (true) {
			time.text = string.Format ("{0:0}:{1:00}", min, sec);
			yield return new WaitForSeconds (0.2f);
		}
	}
}




	

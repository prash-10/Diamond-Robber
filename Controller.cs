using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour {


	private bool facingRight = true;

	private float offsetx;
	private float offsety;


	private bool isGrounded;
	private float radius = 1.5f;
	private float force = 300;

	private bool trigger1; 
	private bool trigger2;

	private bool stop = true;
	private float min;
	private float sec;


	public int lives = 3;

	public Animator anim;


	public Transform box_elev;
	public Transform target;
	public Transform source;

	public GameObject l;
	public GameObject d;
	public Transform groundPoint;
	public LayerMask ground;
	public AudioSource audioSource;
	public AudioClip jump;
	public AudioClip point;
	public AudioClip win;
	public AudioClip gun;
	public Animator animator;
	public int diamonds;
	public float timeLeft = 300.0f;

	public TextMesh time;
	public TextMesh text;
	public TextMesh score;

	public float movespeed_r;
	public float movespeed_l;

	public Sprite sprite_p;
	public Sprite sprite_d;

	private GameObject[] d1;
	private GameObject[] l1;


	// Use this for initialization
	void Awake()
	{
		print ("Hello");
	}

	void Start () {


		lives = PlayerPrefs.GetInt ("lives",3);

		print (lives);

		if (lives == 0)
        //lives = 3;
		   SceneManager.LoadScene ("Level 0");

		l1 = new GameObject[3];

		for (int i = 0; i < 3; i++) {
			l1 [i] = l.transform.GetChild (i).gameObject;
			l1 [i].SetActive (false);
		}

		for (int i = 0; i < lives; i++) {
			
			l1 [i].SetActive (true);
		}

		d1 = new GameObject[20];
		for(int i=0;i<20;i++)
			d1[i] = d.transform.GetChild(i).gameObject;

		StartCoroutine (Wait2 ());

		movespeed_r = 7f;
		movespeed_l = 12f;

		offsetx = text.transform.position.x - transform.position.x;
		offsety = text.transform.position.y - transform.position.y;

		trigger2 = false;

		startTimer (timeLeft);

	}
	


	void startTimer(float x)
	{
		stop = false;
		timeLeft = x;
		Update ();
		StartCoroutine (updateTime ());
	}

	void Update () {


		trigger1 = false;

		isGrounded = Physics2D.OverlapCircle (groundPoint.position, radius, ground);
		if (isGrounded) {
			if (Input.GetKey(KeyCode.Space)) {				
				GetComponent<Rigidbody2D> ().AddForce (Vector2.up * force);
				audioSource.clip = jump;
				audioSource.Play ();
			}
		}

		if (Input.GetKeyDown (KeyCode.DownArrow)) {
		
			PlayerPrefs.DeleteKey ("lives");
		}

		if (Input.GetKey (KeyCode.RightArrow)) 
		{
			transform.Translate (movespeed_r * Input.GetAxis ("Horizontal") * Time.deltaTime,0f,0f);
			if (!facingRight) {
				Flip ();
			}

		}
		else if (Input.GetKey (KeyCode.LeftArrow)) 
		{
			transform.Translate (movespeed_l * Input.GetAxis ("Horizontal") * Time.deltaTime,0f,0f);
			if (facingRight) {
				Flip ();
			}


		}

		Up_Box ();
		Down_Box ();

		if (stop)
			Time.timeScale = 0;

		timeLeft -= Time.deltaTime;
		min = Mathf.Floor (timeLeft / 60);
		sec = timeLeft % 60;
		if (sec > 59)
			sec = 59;
		if (min < 0) {
			stop = true;
			min = 0;
			sec = 0;
		}

		if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)){
			anim.enabled = true;
		}
		else
			anim.enabled = false;
		
	}
		

	void LateUpdate()
	{
		
		score.transform.position = new Vector2 (transform.position.x + offsetx + 4f, transform.position.y + offsety);
		text.transform.position = new Vector2 (transform.position.x + offsetx , transform.position.y + offsety);
		time.transform.position = new Vector2 (transform.position.x + offsetx - 15f, transform.position.y + offsety);

	}

	public void Reset()
	{
		SceneManager.LoadScene("Level 2");
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
		else if (collider2D.tag == "Druby") {
			diamonds+= 5;
			score.text = diamonds + "";
			Destroy (collider2D.gameObject);
			audioSource.clip = point;
			audioSource.Play ();
		}
		else if (collider2D.tag == "Deadzone") {

			if (lives != 1)
				Check ();
			else
				Check1 ();

		} else if (collider2D.tag == "Police") {
			
			diamonds--;
			score.text = diamonds + "";
			Destroy (collider2D.gameObject);
			audioSource.clip = gun;
			audioSource.Play ();
		} else if (collider2D.tag == "Finish") {
		    
			SceneManager.LoadScene ("Level 2.75");
		}

	}
		

	private IEnumerator updateTime()
	{
		while (!stop) {
			time.text = string.Format ("{0:0}:{1:00}", min, sec);
			yield return new WaitForSeconds (0.2f);
		}
	}

	IEnumerator Wait1()
	{
		
		yield return new WaitForSeconds (1);
		trigger1 = true;
	}

	IEnumerator Wait2()
	{
		for (int i = 0; i < 20; i++) {
			if (d1 [i]) {
				d1 [i].GetComponent<SpriteRenderer> ().sprite = sprite_d;
				d1 [i].transform.localScale = new Vector2 (0.2f, 0.2f);
				d1 [i].tag = "Diamond";
			}
		}
		yield return new WaitForSeconds (3);
		StartCoroutine (Wait3());

	}

	IEnumerator Wait3()
	{
		for (int i = 0; i < 20; i++) {
			if (d1 [i]) {
				d1 [i].GetComponent<SpriteRenderer> ().sprite = sprite_p;
				d1 [i].transform.localScale = new Vector2 (0.6f, 0.6f);
				d1 [i].GetComponent<BoxCollider2D> ().size = new Vector2 (5, 5);
				d1 [i].tag = "Police";
			}
		}
		yield return new WaitForSeconds (3);
			StartCoroutine (Wait2());
	}

	void OnCollisionStay2D(Collision2D col)
	{
		if (col.transform.tag == "ground")
		{   
			StartCoroutine (Wait1 ());
			if (trigger1 == true) {
				col.gameObject.GetComponent<Rigidbody2D> ().isKinematic = false;
			}
		}
           
	}

	void Up_Box()
	{
		if (trigger2 == false) {
			box_elev.position = Vector3.MoveTowards (box_elev.position, target.position, 2.0f * Time.deltaTime);
			if (Vector3.Distance (box_elev.position, target.position) <= 0) {
				trigger2 = true;
				Down_Box ();
			}
		}
	}

	void Down_Box()
	{
		if (trigger2 == true) {
			box_elev.position = Vector3.MoveTowards (box_elev.position, source.position, 2.0f * Time.deltaTime);
			if (Vector3.Distance (box_elev.position, source.position) <= 0) {
				trigger2 = false;
				Up_Box ();
			}
		}
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector2 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void Check()
	{
		print (lives);
		lives--;
		PlayerPrefs.SetInt ("lives", lives);
		PlayerPrefs.Save ();
		SceneManager.LoadScene ("Level 2");
	}
	void Check1()
	{
		PlayerPrefs.DeleteKey ("lives");
		print ("You Lost");
		SceneManager.LoadScene ("Level 0");
	}
		
}

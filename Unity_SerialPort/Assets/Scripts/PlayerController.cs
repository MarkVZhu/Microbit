using MarkFramework;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float moveSpeed = 5f;
	public float jumpForce = 10f;
	public float maxSpeed = 5f;
	public LayerMask groundLayer;
	public float groundCheckRadius = 0.1f;  // Radius of the ground check circle

	[SerializeField]private Rigidbody2D rb;
	private bool isGrounded;
	private float moveInput;
	[SerializeField] private Transform groundCheckPosition;  // Position of the ground check circle
	private Animator anim;

	void OnEnable()
	{
		rb = gameObject.GetComponent<Rigidbody2D>();
		anim = gameObject.GetComponent<Animator>();
	}

	void Update()
	{
		// // Get horizontal input
		// moveInput = Input.GetAxisRaw("Horizontal");

		// // Move the player
		// Vector2 velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
		// if(rb.velocity.x < -0.01f)
		// {
		// 	transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
		// }
		// else if(rb.velocity.x > 0.01f)
		// {
		// 	transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
		// }
		// rb.velocity = velocity;
		// anim.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
		// anim.SetFloat("velocityY", rb.velocity.y);

		// // Check if the player is grounded
		// isGrounded = Physics2D.OverlapBox(groundCheckPosition.position, new Vector2(0.6f,0.1f), 0, groundLayer);
		// anim.SetBool("onGround", isGrounded);

		// // Jump
		// if (isGrounded && Input.GetButtonDown("Jump"))
		// {
		// 	rb.velocity = new Vector2(rb.velocity.x, jumpForce);
		// 	anim.SetTrigger("jump");
		// 	if(AudioManager.Instance != null)
		// 	{
		// 		AudioManager.Instance.PlayAudio("Jump");
		// 	}
		// }
		
		anim.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
		anim.SetFloat("velocityY", rb.velocity.y);
		
		// if(Input.GetKeyDown(KeyCode.D))
		// {
		// 	MoveForward();
		// }
		
		isGrounded = Physics2D.OverlapBox(groundCheckPosition.position, new Vector2(0.6f,0.1f), 0, groundLayer);
		anim.SetBool("onGround", isGrounded);
		
		// if(isGrounded && Input.GetKeyDown(KeyCode.Space))
		// {
		// 	anim.SetTrigger("jump");
		// 	Jump();
		// }
		
		if(GameStateManager.Instance.currentState == GameStateManager.GameState.Fail)
		{
			rb.bodyType = RigidbodyType2D.Static;
			anim.SetTrigger("Hurt");
			GameStateManager.Instance.EnterState(GameStateManager.GameState.End);
		}
		
		if(GameStateManager.Instance.currentState == GameStateManager.GameState.Success)
		{
			rb.bodyType = RigidbodyType2D.Static;
			anim.SetTrigger("Success");
			GameStateManager.Instance.EnterState(GameStateManager.GameState.End);
		}
	}
	
	public void MoveForward()
	{
		if (rb.velocity.x < 2f) rb.AddForce(Vector2.right * moveSpeed * 1.2f, ForceMode2D.Force);
		else if (rb.velocity.x < 5.5f) rb.AddForce(Vector2.right * moveSpeed, ForceMode2D.Force);
		Debug.Log(rb.velocity.x);
	}
	
	public void Jump(int voiceInt)
	{
		if(isGrounded)
		{
			SoundMgr.Instance.PlaySound("Jump");
			anim.SetTrigger("jump");
			float jumpMul = 1f + voiceInt/10f;
			rb.AddForce(Vector2.up * jumpForce * jumpMul, ForceMode2D.Impulse);
			Debug.Log("Jump Force: " + jumpMul);
		}
	}

	void OnDrawGizmos()
	{
		if (groundCheckPosition != null)
		{
			// Draw a line from the player to the ground check position
			Gizmos.color = Color.red;
			
			// Draw a circle at the ground check position
			Gizmos.DrawWireCube(groundCheckPosition.position, new Vector2(0.6f,0.1f));
		}
	}
	
	private void OnDestroy() 
	{
		StopAllCoroutines();
	}
}

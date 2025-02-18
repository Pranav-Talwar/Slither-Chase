using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveForce = 10f;

    [SerializeField]
    private float jumpForce = 11f;

    private float movementX;

    private Rigidbody2D myBody;

    private SpriteRenderer sr;
    private bool isGrounded;
    private string GROUND_TAG = "Ground";

    private Animator anim;
    private string WALK_ANIMATION = "Walk";

    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        PlayerMoveKeyboard();
        AnimatePlayer(); // Now properly called outside
    }
    private void FixedUpdate()
    {

        PlayerJump();
    }
    void PlayerMoveKeyboard()
    {
        movementX = Input.GetAxisRaw("Horizontal");

        // Directly modifying transform.position with physics is not ideal but left as is
        transform.position += new Vector3(movementX, 0f, 0f) * moveForce * Time.deltaTime;
    }

    void AnimatePlayer()
    {

        // we are going to the right side
        if (movementX > 0)
        {

            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = false;
        }

        else if (movementX < 0)
        {

            // we are going to the left side
            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = true;
        }

        else
        {

            anim.SetBool(WALK_ANIMATION, false);

        }
    }
    void PlayerJump()
    {

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;
            myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag(GROUND_TAG))
            isGrounded = true;


        //if (collision.gameObject.CompareTag(ENEMY_TAG))
        //    Destroy(gameObject);


    }

}
using UnityEngine;

public class Snake : MonoBehaviour
{
    [HideInInspector]
    public float speed;

    private Rigidbody2D myBody;

    // Awake is called before Start
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        speed = 2;
    }

    // FixedUpdate is called at fixed intervals for physics updates
    void FixedUpdate()
    {
        myBody.linearVelocity = new Vector2(speed, myBody.linearVelocity.y);
    }
}

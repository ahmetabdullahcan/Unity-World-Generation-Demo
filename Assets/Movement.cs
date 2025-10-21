using UnityEngine;


public class Movement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}

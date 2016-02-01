using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour
{

    public KeyCode forward;
    public KeyCode backwards;
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
    public float bounds = 7;
    public GameObject otherPlayer;
    public float speed = 20;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

        if (Input.GetKey(forward))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

        if (Input.GetKey(left))
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        if (Input.GetKey(right))
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);

        }

        if (Input.GetKey(backwards))
        {
            transform.Translate(Vector3.back * Time.deltaTime * speed);
        }

        if (Input.GetKey(up))
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }

        if (Input.GetKey(down))
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
        }

        rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, otherPlayer.transform.position.x - bounds, otherPlayer.transform.position.x + bounds),
            Mathf.Clamp(rb.position.y, otherPlayer.transform.position.y - bounds, otherPlayer.transform.position.y + bounds),
            Mathf.Clamp(rb.position.z, otherPlayer.transform.position.z - bounds, otherPlayer.transform.position.z + bounds)
        );

    }

}

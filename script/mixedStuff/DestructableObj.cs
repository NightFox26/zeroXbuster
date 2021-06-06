using UnityEngine;

public class DestructableObj : MonoBehaviour
{
    public Vector2 explodeDirection;
    public float explosionTorque;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        explode();
    }

    private void explode(){
        rb.AddForce(explodeDirection);
        rb.AddTorque(explosionTorque);
        Destroy(transform.parent.gameObject,2.5f);
    }
}

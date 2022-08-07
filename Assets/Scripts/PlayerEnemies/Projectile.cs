using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] 
public class Projectile : MonoBehaviour, IProjectile
{
    private float speed;

    private Vector2 direction;

    private Rigidbody2D rigidbody;

    private Vector3 firedPoint;

    [SerializeField] private float livingRange;

    private void OnEnable()
    {
        firedPoint = transform.position;
    }

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.bodyType = RigidbodyType2D.Kinematic;
    }

    public void SetFireParams(bool toRight, float speed)
    {
        this.speed = speed;
        direction = toRight ? Vector2.right : Vector2.left;
    }

    private void Update()
    {
        rigidbody.velocity = direction * speed;
        if (Mathf.Abs(firedPoint.x - transform.position.x) > livingRange) gameObject.SetActive(false);
    }
}

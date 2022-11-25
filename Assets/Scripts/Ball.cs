using System;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Ball : MonoBehaviour
{
    //Set elsewhere
    private float speed;
    private Vector2 direction;
    private Transform target;
    private Vector2 bounds = new Vector2(5, 5);
    private SortManager sv;
    [HideInInspector] public bool IsWithin;

    //Set in editor
    [SerializeField] private int substeps = 1;
    [SerializeField] private float radius = 0.5f;
    [SerializeField] private float collisionThreshold = 0.1f;
    private SpriteRenderer sRend = null;
    private float timeSinceLastCollision = 0f;

    //Calculated
    public float DstFromTarget { set; get; }
    private float realRadius;

    private void OnEnable()
    {
        transform.localScale = Vector3.one * radius * 0.5f;
        timeSinceLastCollision = collisionThreshold;
        sRend = GetComponentInChildren<SpriteRenderer>();
        realRadius = radius * 0.5f;
    }

    private void Start()
    {
        SetColor(Color.red);
    }

    internal virtual void FixedUpdate()
    {
        SetColor(Color.white);
        SetColor(IsWithin ? Color.red : Color.white);

        timeSinceLastCollision += Time.deltaTime;

        Vector3 position = transform.position;
        
        for (int i = 0; i < substeps; i++)
        {
            if (transform.position.x + realRadius > bounds.x)
            {
                float delta =  bounds.x - (transform.position.x + realRadius);
                position = new Vector3(position.x + delta, position.y);
                direction *= Collided(new Vector2(-1, 1));
            }

            if (transform.position.x - realRadius < -bounds.x)
            {
                float delta =  -bounds.x - (transform.position.x - realRadius);
                position = new Vector3(position.x + delta, position.y);
                direction *= Collided(new Vector2(-1, 1));
            }

            if (transform.position.y + realRadius > bounds.y)
            {
                float delta =  bounds.y - (transform.position.y + realRadius);
                position = new Vector3(position.x, position.y + delta);
                direction *= Collided(new Vector2(1, -1));
            }

            if (transform.position.y - realRadius < -bounds.y)
            {
                float delta =  -bounds.y - (transform.position.y - realRadius);
                position = new Vector3(position.x, position.y + delta);
                direction *= Collided(new Vector2(1, -1));
            }
        }
        
        position += (Vector3) (direction * speed * Time.deltaTime);
        transform.position = position;
        
        DstFromTarget = GetDistance();
    }

    public float GetDistance()
    {
        return Vector2.Distance(transform.position, target.position);
    }
    
    private Vector2 Collided(Vector2 dir)
    {
        if (timeSinceLastCollision >= collisionThreshold)
        {
            timeSinceLastCollision = 0f;

            return dir;
        }

        return new Vector2(1, 1);
    }

    public void InitBall(Transform _target, float _speed, Vector2 _direction, Vector2 _bounds, SortManager _sortManager)
    {
        target = _target;
        speed = _speed;
        direction = _direction;
        bounds = _bounds;
        sv = _sortManager;
    }

    public void SetColor(Color _color)
    {
        sRend.color = _color;
    }
}
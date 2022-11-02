using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[ExecuteAlways]
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
    public float DstFromTarget { private set; get; }
    private Vector2 lastPos = Vector2.zero;

    private void OnEnable()
    {
        transform.localScale = Vector3.one * radius * 0.5f;
        timeSinceLastCollision = collisionThreshold;
        sRend = GetComponentInChildren<SpriteRenderer>();
    }

    internal virtual void Update()
    {
        if (!sv.Running) return;

        SetColor(IsWithin ? Color.red : Color.white);

        timeSinceLastCollision += Time.deltaTime;

        for (int i = 0; i < substeps; i++)
        {
            
        //Do not multiply radius during runtime, fix later
        if (transform.position.x + radius * 0.5f > bounds.x)  { direction *= Collided(new Vector2(-1, 1)); }
        if (transform.position.x - radius * 0.5f < -bounds.x) { direction *= Collided(new Vector2(-1, 1)); }
        if (transform.position.y + radius * 0.5f > bounds.y)  { direction *= Collided(new Vector2(1, -1)); }
        if (transform.position.y - radius * 0.5f < -bounds.y) { direction *= Collided(new Vector2(1, -1)); }
        
        }
        
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
        
        DstFromTarget = Vector2.Distance(transform.position, target.position);
        
        lastPos = transform.position;
    }

    private Vector2 Collided(Vector2 dir)
    {
        if (timeSinceLastCollision >= collisionThreshold)
        {
            timeSinceLastCollision = 0f;
            transform.position = lastPos;
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
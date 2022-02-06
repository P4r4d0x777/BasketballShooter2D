using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    public float power = 2.0f;
    public float lifeAlphaChannel = 1.0f;

    //if mouse or finger touch too much we cancel the shoot
    public float deadSence = 25f;

    public int dots = 30;

    private Vector2 startPosition;

    private bool shoot = false;
    private bool aiming = false;
    private bool hitGround = false;

    private GameObject Dots;
    private List<GameObject> dotsPath;

    private Rigidbody2D myBody;
    private Collider2D myCollider;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();

        myCollider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        Dots = GameObject.Find("dots");

        myBody.isKinematic = true;

        myCollider.enabled = false;

        // ?
        startPosition = transform.position;

        dotsPath = Dots.transform.Cast<Transform>().ToList().ConvertAll(t => t.gameObject);

        for(int i = 0; i < dotsPath.Count; i++)
        {
            dotsPath[i].GetComponent<Renderer>().enabled = false;
        }
    }

    private void Update()
    {
        Aim();
    }
    void Aim()
    {
        if (shoot)
            return;

        if(Input.GetAxis("Fire 1") == 1)
        {
            if(!aiming)
            {
                aiming = true;

                startPosition = Input.mousePosition;

                CalculatePath();
            }
        }
    }

    Vector2 GetForce(Vector3 mouse)
    {
        return (new Vector2(startPosition.x, startPosition.y) - new Vector2(mouse.x, mouse.y)) * power;
    }

    bool inDeadZone(Vector2 mouse)
    {
        return false;
    }

    bool inReleaseZone(Vector2 mouse)
    {
        return false;
    }

    void CalculatePath()
    {
        Vector2 velocity = GetForce(Input.mousePosition) * Time.fixedDeltaTime / myBody.mass;
    }

    Vector2 PathPoint(Vector2 StartP, Vector2 StartVel, float t)
    {
        return Vector2.zero;
    }

    void HidePath()
    {

    }

    void ShowPath()
    {

    }
}

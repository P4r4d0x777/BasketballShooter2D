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

        if(hitGround)
        {
            lifeAlphaChannel -= Time.deltaTime;

            Color c = GetComponent<Renderer>().material.GetColor("_Color");

            GetComponent<Renderer>().material.SetColor("_Color", new Color(c.r, c.g, c.b, lifeAlphaChannel));

            if(lifeAlphaChannel < 0 )
            {
                if(GameManager.instance != null)
                {
                    GameManager.instance.CreateBall();
                }

                Destroy(gameObject);
            }
        }
    }
    void Aim()
    {
        if (shoot)
            return;

        if(Input.GetAxis("Fire1") == 1)
        {
            if(!aiming)
            {
                aiming = true;

                startPosition = Input.mousePosition;

                CalculatePath();

                ShowPath();
            }
            else
            {
                CalculatePath();
            }
        }
        else if(aiming && !shoot)
        {
            if(inDeadZone(Input.mousePosition) || inReleaseZone(Input.mousePosition))
            {
                aiming = false;

                HidePath();

                return;
            }

            myBody.isKinematic = false;
            myCollider.enabled = true;
            shoot = true;
            aiming = false;
            myBody.AddForce(GetForce(Input.mousePosition));
            HidePath();
        }
    }

    Vector2 GetForce(Vector3 mouse)
    {
        return (new Vector2(startPosition.x, startPosition.y) - new Vector2(mouse.x, mouse.y)) * power;
    }

    bool inDeadZone(Vector2 mouse)
    {
        if (Mathf.Abs(startPosition.x - mouse.x) <= deadSence && Mathf.Abs(startPosition.y - mouse.y) <= deadSence)
        {
            return true;
        }
        else
            return false;
    }

    bool inReleaseZone(Vector2 mouse)
    {
        if(mouse.x <= 70 )
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void CalculatePath()
    {
        Vector2 velocity = GetForce(Input.mousePosition) * Time.fixedDeltaTime / myBody.mass;

        for (int i = 0; i < dotsPath.Count; i++)
        {
            dotsPath[i].GetComponent<Renderer>().enabled = true;

            float t = i / 30f;

            Vector3 point = PathPoint(transform.position, velocity, t);

            point.z = 1.0f;

            dotsPath[i].transform.position = point;
        }
    }

    Vector2 PathPoint(Vector2 StartP, Vector2 StartVel, float t)
    {

        //TODO: узнать что это за формула и откуда она
        return StartP + StartVel * t + 0.5f * Physics2D.gravity * t * t;
    }

    void HidePath()
    {
        for (int i = 0; i < dotsPath.Count; i++)
        {
            dotsPath[i].GetComponent<Renderer>().enabled = false;
        }
    }

    void ShowPath()
    {
        for (int i = 0; i < dotsPath.Count; i++)
        {
            dotsPath[i].GetComponent<Renderer>().enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            hitGround = true;
        }
    }
}

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

}

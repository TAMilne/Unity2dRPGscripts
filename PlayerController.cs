using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Rigidbody2D theRB;
    public float moveSpeed;
    public Animator myAnim;
    public static PlayerController instance;
    public string areaTransitionName;

    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    public bool canMove = true;


    // Start is called before the first frame update
    void Start() {
        if(instance == null) {
            instance = this;
        } else {
            if(instance != this) {
            Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update() {

        // if determines if they're in conversation
        if(canMove) {
            theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;
        } else {
            theRB.velocity= Vector2.zero;
        }

        myAnim.SetFloat("moveX", theRB.velocity.x);
        myAnim.SetFloat("moveY", theRB.velocity.y);
        if(Input.GetAxisRaw("Horizontal") == 1 || 
           Input.GetAxisRaw("Horizontal") == -1 || 
           Input.GetAxisRaw("Vertical") == 1 || 
           Input.GetAxisRaw("Vertical") == -1) 
        {
            if(canMove) {
                myAnim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                myAnim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
            }
        }

        //keep the Player within bounds
        transform.position= new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x),
                                        Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y),
                                        transform.position.z);
    }

    public void setBounds(Vector3 bottomLeft, Vector3 topRight) {
        bottomLeftLimit = bottomLeft + new Vector3(.5f, .5f, 0f);
        topRightLimit = topRight + new Vector3(.5f, .5f, 0f);
    }
}

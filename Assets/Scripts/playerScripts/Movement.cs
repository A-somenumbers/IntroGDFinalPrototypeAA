using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float moveF = 10f;
    public float jumpF = 11f;
    private float moveX;
    [SerializeField] private Rigidbody2D myBody;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Animator anim;
    [SerializeField] private TrailRenderer tr;

    //dashing
    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float DashPower = 24f;
    [SerializeField] private float DashTime = 0.2f;
    [SerializeField] private float dashCool = 1f;

    private string WALK_ANIMATION = "Walk";
    private string GROUND_TAG_ = "Ground";
    private bool grounded = true;
   
    //double jump power up 
    private bool doubleJump;
    public static bool doubleJumpA = false;

    //grappling hook
    [SerializeField] private float grappleLength;
    [SerializeField] private LayerMask grappleLayer;
    [SerializeField] private LineRenderer lr;
    private Vector3 grapplePoint;
    private DistanceJoint2D dj;
    public static bool grappleAllowed = false;

    //audio
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip dash;
    [SerializeField] private AudioClip pickup1;
    [SerializeField] private AudioClip pickup2;
    [SerializeField] private AudioClip grapple1;
    [SerializeField] private AudioClip grapple2;
    //[SerializeField] private AudioClip goal;
    // Start is called before the first frame update
    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        grappleAllowed = false;
        doubleJumpA = false;

        dj = gameObject.GetComponent<DistanceJoint2D>();
        dj.enabled = false;
        lr.enabled = false;
      
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }
        
        PlayerMoveKeyboard();
        AnimatePlayer();
        PlayerJump();
        if (grappleAllowed) 
        { 
            grapple(); 
        }
        
        if (Input.GetKeyDown(KeyCode.LeftShift)&& canDash)
        {
            StartCoroutine(Dash());
        }

    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return ;
        }
    }


    void PlayerMoveKeyboard()
    {
        moveX = Input.GetAxisRaw("Horizontal");

        transform.position += new Vector3(moveX, 0f, 0f) * Time.deltaTime * moveF;
    }

    void AnimatePlayer()
    {
        
        if (moveX > 0f)
        {
            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = false;
        }
        else if (moveX < 0f)
        {
            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = true;
        }
        else
        {
            anim.SetBool (WALK_ANIMATION, false);
        }
    }

    void PlayerJump()
    {
        
       
        
        
        if (Input.GetButtonDown("Jump")) {
            if(grounded||doubleJump)
            {
                AudioManager.Instance.playSFXclip(jump, transform, 1f);
                grounded = false;
                myBody.velocity = new Vector2(myBody.velocity.x, jumpF);
                if (doubleJumpA)
                {
                    doubleJump = !doubleJump;
                    
                }
                
            }
            
        }
        if (Input.GetButtonUp("Jump") && myBody.velocity.y > 0f)
        {
            myBody.velocity = new Vector2(myBody.velocity.x,myBody.velocity.y*0.5f);
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float og = myBody.gravityScale;
        myBody.gravityScale = 0f;
        Vector2 ogV = myBody.velocity;
        if (sr.flipX)
        {
            myBody.velocity = new Vector2(DashPower *-1f, 0f);
        }
        else
        {
            myBody.velocity = new Vector2(DashPower, 0f);
        }
        tr.emitting = true;
        AudioManager.Instance.playSFXclip(dash, transform, 1f);
        yield return new WaitForSeconds(DashTime);
        tr.emitting = false;
        yield return new WaitForSeconds(0.3f);
        myBody.velocity = new Vector2(ogV.x, og);
        myBody.gravityScale = og;
        isDashing = false;
        yield return new WaitForSeconds(dashCool);
        canDash = true;

    }

    void grapple()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero,Mathf.Infinity,grappleLayer);
            if(hit.collider != null)
            {
                grapplePoint = hit.point;
                grapplePoint.z = 0f;
                AudioManager.Instance.playSFXclip(grapple1, transform, 1f);
                dj.connectedAnchor = grapplePoint;
                dj.enabled = true;
                dj.distance = grappleLength;
                lr.SetPosition(0, grapplePoint);
                lr.SetPosition(1,transform.position);
                lr.enabled = true;
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            AudioManager.Instance.playSFXclip(grapple2, transform, 1f);
            dj.enabled = false;
            lr.enabled = false;
        }
        if (lr.enabled)
        {
            lr.SetPosition(1, transform.position);
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GROUND_TAG_))
        {
            grounded=true;
        }
    }
    private void OnTriggerEnter2D(Collider2D o)
    {
        if (o.gameObject.CompareTag("Dash"))
        {
            AudioManager.Instance.playSFXclip(pickup1, transform, 1f);
            Destroy(o.gameObject);
            doubleJumpA = true;
            grappleAllowed = false;
        }

        if (o.gameObject.CompareTag("Grapple"))
        {
            AudioManager.Instance.playSFXclip(pickup2, transform, 1f);
            Destroy(o.gameObject);
            grappleAllowed = true;
            doubleJumpA = false;

        }

        if (o.gameObject.CompareTag("goal") && GameObject.FindGameObjectsWithTag("Enemy").Length <=0)
        {
            Time.timeScale = 0f;
            pauseMenu.beaten = true;
        }
    }


}

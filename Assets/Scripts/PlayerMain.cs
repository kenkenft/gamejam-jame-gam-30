using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    [SerializeField]
    private float   _playerSpeed = 10f, 
                    _playerJump = 10f,
                    _speedDecayMultiplier = 0.95f,
                    _jumpVelDecayHigh = 1.4f, 
                    _jumpVelDecayLow = 1.9f;

    [SerializeField] private bool _isFacingRight = true, _isAirborne = false, _isMovingSideways = false;
    private Vector2 _moveXY = new Vector2(0f, 0f);
    private Vector3 _maskX, _maskY;
    public Rigidbody2D PlayerRig;

    private Ray2D[] _rays;
    public LayerMask GroundLayerMask;

    // public Animator animator;

    [HideInInspector] public delegate void OnSomeEvent();
    [HideInInspector] public static OnSomeEvent RestartButtonPressed;
    [HideInInspector] public static OnSomeEvent DimensionButtonPressed;

    [HideInInspector] public delegate bool OnInteractKeyDown();
    [HideInInspector] public static OnInteractKeyDown CheckIsPlaying;

    [HideInInspector] public delegate void OnPlaySFX(string audioName);
    [HideInInspector] public static OnPlaySFX PlaySFX;

    void OnEnable()
    {
    }
    
    void OnDisable()
    {
    }

    void Start()
    {
        Vector2 spriteSize =  GetComponent<SpriteRenderer>().bounds.size;
        _maskX = new Vector3(spriteSize.x / 2f, 0, 0);
        _maskY = new Vector3(0,spriteSize.y / 2f, 0);
    }

    void Update()
    {
        // if(CheckIsPlaying.Invoke())
        // {
            Move();

            if(Input.GetKeyDown("space")) 
                Jump();

            // SetPlayerAnimation();

            VelocityDecay();

            if(Input.GetKeyDown(KeyCode.G))
                RestartButtonPressed?.Invoke();

            if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.I))
                DimensionButtonPressed?.Invoke();
        // }
    }

    void Move()
    {
        _moveXY[0] = Input.GetAxis("Horizontal") * _playerSpeed;
        _moveXY[1] = PlayerRig.velocity.y;

        if( _moveXY[0] !=0 || _moveXY[1] !=0)
            PlayerRig.velocity = _moveXY;
    }

    public void Jump()
    {
        if(IsGrounded())
        {    
            PlayerRig.velocity = Vector2.up * _playerJump;
            // _isAirborne = true;
            PlaySFX?.Invoke("Jump");
        }
    }

    public bool IsGrounded()
    {
        Ray2D[] jumpRays = CreateRays();
        foreach(Ray2D ray in jumpRays)
        {
            Debug.DrawRay(ray.origin, ray.direction, Color.green, 5f);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, _maskY[1], GroundLayerMask);
            
            if (hit.collider != null) 
            {    
                // Debug.Log(hit.collider.gameObject.name);
                return true;
            }
        }
        // Debug.Log("Not on the ground!");
        return false;
    }//// End of IsGrounded()

    public Ray2D[] CreateRays()
    {
        // Construct 3 rays at the bottom of the player's sprite.
        return _rays = new Ray2D[3]
                                    {
                                        // Create raycast at position clickedTiles's origin + tile's bounding area + tile margin offset
                                        new Ray2D(transform.position - _maskX, Vector2.down),             // Sends raycast 0.1m above tile
                                        new Ray2D(transform.position + _maskX, Vector2.down),             // Sends raycast 0.1m below tile
                                        new Ray2D(transform.position , Vector2.down),             // Sends raycast 0.1m to the right of the tile
                                    };
    }

    public void VelocityDecay()
    {
        float x = PlayerRig.velocity.x;
        Vector3 mask = PlayerRig.velocity;

        if( x !=0.0f )              // Gradually reduce x-axis velocity (Unless being boosted by ramps)
        {
            mask.x *= _speedDecayMultiplier;
            PlayerRig.velocity = mask;
        }

        if(PlayerRig.velocity.y < 0)              // Reduces floatiness of jumps
            PlayerRig.velocity += Vector2.up * Physics2D.gravity.y * _jumpVelDecayHigh * Time.deltaTime;    
        else if(PlayerRig.velocity.y > 0 && !Input.GetButton("Jump"))     // For low jumps
            PlayerRig.velocity += Vector2.up * Physics2D.gravity.y * _jumpVelDecayLow  * Time.deltaTime;                // Start increasing downward velocity once player lets go of jump input
        
    }//// End of VelocityDecay()

    void OnCollisionEnter2D(Collision2D col)
    {
        ContactPoint2D[] contact = new ContactPoint2D[col.contactCount];
        int points = col.GetContacts(contact);

        // // foreach(ContactPoint2D point in contact)
        // // {
        //     if(col.GetContact(0).normal == Vector2.up)   
        //     {    
        //         PlaySFX?.Invoke("Landed");
        //         _isAirborne = false; 
        //         // break;
        //     }
        // // }
        foreach(ContactPoint2D point in contact)
            {
                // Debug.Log(point.normal);
                if(point.normal == Vector2.up)   
                {    
                    PlaySFX?.Invoke("Landed");
                    _isAirborne = false; 
                    break;
                }
            }
    }

    // void OnCollisionStay2D(Collision2D col)
    // {
    //     if(_isAirborne)
    //     {
    //         ContactPoint2D[] contact = new ContactPoint2D[col.contactCount];
    //         int points = col.GetContacts(contact);

    //         foreach(ContactPoint2D point in contact)
    //         {
    //             Debug.Log(point.normal);
    //             if(point.normal == Vector2.up)   
    //             {    
    //                 _isAirborne = false; 
    //                 break;
    //             }
    //         }
    //     }
    // }

    void OnCollisionExit2D(Collision2D col)
    {
        _isAirborne = true;
    }

    public void RepositionPlayer(Vector3 location)
    {
        this.gameObject.transform.position = location;
    }


// void SetPlayerAnimation()
//     {
        
//         animator.SetFloat("_velocityY", _moveXY[1]);
//         animator.SetFloat("_velocityX", Mathf.Abs(_moveXY[0]));

//         if(_moveXY[1] != 0f && _isAirborne)
//         {    
//             animator.SetBool("_isAirborne", true);
//             if(_moveXY[1] > 0)
//             {
//                 animator.SetBool("_isMovingUp", true);
//                 animator.SetBool("_isMovingDown", false);
//             }
//             else
//             {
//                 animator.SetBool("_isMovingUp", false);
//                 animator.SetBool("_isMovingDown", true);
//             }
//         }
//         else
//         {    
//             animator.SetBool("_isAirborne", false);
//             animator.SetBool("_isMovingUp", false);
//             animator.SetBool("_isMovingDown", false);
//         }
        
//         if(_moveXY[0] != 0f)
//         {
//             _isMovingSideways = true;
//             animator.SetBool("_isMovingSideways", true);
//         }
//         else
//         {
//             _isMovingSideways = false;
//             animator.SetBool("_isMovingSideways", false);
//         }
        
//         if(_isMovingSideways)
//         {    
//             if(_moveXY[0] > 0f && !_isFacingRight && _isMovingSideways)
//                     FlipSprite(); 
//             else if(_moveXY[0] < 0f && _isFacingRight && _isMovingSideways) 
//                     FlipSprite();
//         }    
//     }

    // void FlipSprite()
    // {
    //     _isFacingRight = !_isFacingRight;
    //     Vector3 theScale = transform.localScale;
    //     theScale.x *= -1;
    //     transform.localScale = theScale;
    // }
}

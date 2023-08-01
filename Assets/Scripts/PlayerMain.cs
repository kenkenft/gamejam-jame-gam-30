using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    [SerializeField]
    private float _playerSpeed = 10f;

    [SerializeField] private bool _isFacingRight = true, _isMovingSideways = false;
    private Vector2 _moveXY = new Vector2(0f, 0f);
    private Vector3 _maskX, _maskY;
    public Rigidbody2D PlayerRig;

    private Ray2D[] _rays;
    public LayerMask GroundLayerMask;
    public PlayerHealth HealthScript;

    public PlayerAttack AttackScript;

    [SerializeField] private AgeState _currentAge = AgeState.Young;

    // public Animator animator;

    [HideInInspector] public static ReturnBool CheckIsPlaying;

    [HideInInspector] public static SendString PlaySFX;

    [HideInInspector] public static SendInt AgeRequested;
    [HideInInspector] public static OnSomeEvent HourGlassFlipped;

    void OnEnable()
    {
        PlayerAge.AgeChanged = SetPlayerAge;
        PlayerAge.AgeRequested = GetPlayerAge;
    }
    
    void OnDisable()
    {
        PlayerAge.AgeChanged = null;
        PlayerAge.AgeRequested = null;
    }

    void Start()
    {
        Vector2 spriteSize =  GetComponent<SpriteRenderer>().bounds.size;
        _maskX = new Vector3(spriteSize.x / 2f, 0, 0);
        _maskY = new Vector3(0,spriteSize.y / 2f, 0);
        
        Vector3 offset = new Vector3(0f, 1f, 0f) + _maskY;
        HealthScript.SetOffset(offset);
    }

    void Update()
    {
        // if(CheckIsPlaying.Invoke())
        // {
            Move();

            if(Input.GetKeyDown(KeyCode.H))
                HourGlassFlipped?.Invoke();

            if(Input.GetKeyDown(KeyCode.J))
                AttackScript.CallAbility();

            if(Input.GetKeyDown(KeyCode.K))
                Debug.Log("Ability 2 called");

        // }
    }

    private void Move()
    {
        // Debug.Log("Player Move called!");

        _moveXY[0] = Input.GetAxis("Horizontal") * _playerSpeed;
        _moveXY[1] = Input.GetAxis("Vertical") * _playerSpeed;

        if( _moveXY[0] !=0 || _moveXY[1] !=0)
            PlayerRig.velocity = _moveXY;
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


    public void RepositionPlayer(Vector3 location)
    {
        this.gameObject.transform.position = location;
    }

    void SetPlayerAge(int ageEnum)
    {
        _currentAge = (AgeState)ageEnum;
        Debug.Log($"Player age: {_currentAge}");
    }

    int GetPlayerAge()
    {
        return (int)_currentAge;
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

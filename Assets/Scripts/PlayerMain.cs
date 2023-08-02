using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    [SerializeField]
    private int _chargesLeft;
    private float _playerSpeed = 10f;

    private bool _isFlipOccurring = false;

    private Vector2 _moveXY = new Vector2(0f, 0f), _currentDirection;
    private Vector3 _maskX, _maskY;
    public Rigidbody2D PlayerRig;

    private Ray2D[] _rays;
    public LayerMask GroundLayerMask;
    public PlayerHealth HealthScript;

    public PlayerAttack AttackScript;

    [SerializeField] private AgeState _currentAge = AgeState.Young;

    // public Animator animator;

    [HideInInspector] public static ReturnBool CheckIsPlaying;
    [HideInInspector] public static OnSomeEvent TogglePauseUI;

    [HideInInspector] public static SendString PlaySFX;

    [HideInInspector] public static SendInt AgeRequested;
    [HideInInspector] public static SendInt ChargeUsed;
    [HideInInspector] public static OnSomeEvent HourGlassFlipped;

    void OnEnable()
    {
        PlayerAge.AgeChanged = SetPlayerAge;
        PlayerAge.AgeRequested = GetPlayerAge;
        UIManager.StartGameSetUp += SetUp;
    }
    
    void OnDisable()
    {
        PlayerAge.AgeChanged = null;
        PlayerAge.AgeRequested = null;
        UIManager.StartGameSetUp -= SetUp;
    }

    void Start()
    {
        Vector2 spriteSize =  GetComponent<SpriteRenderer>().bounds.size;
        _maskX = new Vector3(spriteSize.x / 2f, 0, 0);
        _maskY = new Vector3(0,spriteSize.y / 2f, 0);
        _currentDirection = _moveXY;
        
        Vector3 offset = new Vector3(0f, 1f, 0f) + _maskY;
        HealthScript.SetOffset(offset);
        AttackScript.SetAbilities((int)_currentAge);
    }

    void SetUp()
    {
        AttackScript.SetAbilities((int)_currentAge);
        _chargesLeft = 1;
        _isFlipOccurring = false;
    }

    void Update()
    {
        if(CheckIsPlaying.Invoke())
        {
            Move();

            if(Input.GetKeyDown(KeyCode.H) && _chargesLeft > 0 && !_isFlipOccurring)
            {    
                _isFlipOccurring = true;
                _chargesLeft--;
                ChargeUsed?.Invoke(_chargesLeft);
                HourGlassFlipped?.Invoke();
                _isFlipOccurring = false;
            }

            if(Input.GetKeyDown(KeyCode.J))
                AttackScript.Attack(_currentDirection);

            if(Input.GetKeyDown(KeyCode.K))
                Debug.Log("Ability 2 called");

        }
    }

    private void Move()
    {
        // Debug.Log("Player Move called!");

        _moveXY[0] = Input.GetAxis("Horizontal") * _playerSpeed;
        _moveXY[1] = Input.GetAxis("Vertical") * _playerSpeed;

        if( _moveXY[0] !=0 || _moveXY[1] !=0)
            PlayerRig.velocity = _moveXY;

        if( _moveXY[0] != 0 && _moveXY[1] !=0)
            _currentDirection = _moveXY;
    }


    public void RepositionPlayer(Vector3 location)
    {
        this.gameObject.transform.position = location;
    }

    void SetPlayerAge(int ageEnum)
    {
        _currentAge = (AgeState)ageEnum;
        Debug.Log($"Player age: {_currentAge}");
        AttackScript.SetAbilities((int)_currentAge);
    }

    int GetPlayerAge()
    {
        return (int)_currentAge;
    }

    public void ChargePickUp()
    {
        if(_chargesLeft < 3)
        {    
            _chargesLeft++;
            ChargeUsed?.Invoke(_chargesLeft);
        }
        // else
        // GetBonusPoints?.Invoke();
    }

}

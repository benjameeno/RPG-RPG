using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameObject rocketPrefab;

    public Transform rocketReloadPosition;
    
    public InputActionReference positionInput; //WASD
    public InputActionReference rotationInput; //Mouse
    public InputActionReference fireRocketInput;
    
    public float playerSpeed = 1f;
    public float mouseSensitivity = 1f;
    public float MAX_ROCKET_RELOAD_TIME = 3f; //READ ONLY
    
    private RocketController _currentRocketController;
    
    private bool _reloadRequired;

    private float _reloadTimer;
    
    private void Start()
    {
        _reloadTimer = MAX_ROCKET_RELOAD_TIME;
        
        fireRocketInput.action.performed += FireRocketInput_OnPerformed;
        
        Reload();
    }

    private void Update()
    {
        Move();
        
        Rotate();
        
        ReloadTimerCheck();
    }

    private void Move()
    {
        Vector2 positionInputV2 = positionInput.action.ReadValue<Vector2>();
        if(positionInputV2.y <= 0) return;
        
        Vector3 moveDirection = transform.forward * positionInputV2.y;

        transform.position += moveDirection * playerSpeed * Time.deltaTime;
    }

    private void Rotate()
    {
        Vector2 rotationInputV2 = rotationInput.action.ReadValue<Vector2>();

        Vector3 rotationDirection = new Vector3(0, rotationInputV2.x, 0);
        
        transform.rotation *= Quaternion.Euler(rotationDirection * mouseSensitivity * Time.deltaTime);
    }

    private void ReloadTimerCheck()
    {
        if (_reloadRequired)
        {
            _reloadTimer -= Time.deltaTime;
            if (_reloadTimer < 0)
            {
                Reload();
                _reloadTimer = MAX_ROCKET_RELOAD_TIME;
            }
        }
    }
    
    private void FireRocketInput_OnPerformed(InputAction.CallbackContext obj)
    {
        if (_currentRocketController)
        {
            _currentRocketController.FireRocket();

            _reloadRequired = true;
        }
    }

    private void Reload()
    {
        GameObject rocketInstance = Instantiate(rocketPrefab, rocketReloadPosition);
        _currentRocketController = rocketInstance.GetComponent<RocketController>();
        
        _reloadRequired = false;
    }

    private void OnDestroy()
    {
        fireRocketInput.action.performed -= FireRocketInput_OnPerformed;
    }
}

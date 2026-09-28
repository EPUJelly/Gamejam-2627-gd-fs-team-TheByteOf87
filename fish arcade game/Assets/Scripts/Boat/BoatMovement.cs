using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class BoatMovement : MonoBehaviour
{
    private bool canMove = false;
    public static UnityAction<bool, int> toggleMovement;
    private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float rotAmount;
    [SerializeField] private float rotSpeed;
    [SerializeField] private float boatTiltXAmount;
    [SerializeField] private float boatTiltXSpeed;
    [SerializeField] int player;
    private float boatAngleTarg;
    private float boatTiltXTarg;
    private float boatTiltX;
    private float boatAngle;
    [SerializeField] private ParticleSystem engineParticles;
    [SerializeField] private AudioSource motorSound;
    private bool hasPlayedSound = false;
    Vector2 moveVector;

    void Start()
    {
        toggleMovement += ToggleMove;
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }

    private void ToggleMove(bool value, int _player)
    {
        if (player == _player)
            canMove = value;
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            boatAngleTarg = moveVector.x * rotAmount;
            boatTiltXTarg = moveVector.y * boatTiltXAmount; 
            boatAngle = Mathf.LerpAngle(boatAngle, boatAngleTarg, rotSpeed);
            boatTiltX = Mathf.LerpAngle(boatTiltX, boatTiltXTarg, boatTiltXSpeed);
            float boatAngleFinal = boatAngle + transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, boatAngleFinal, transform.eulerAngles.z);
            rb.AddForce(transform.forward * -moveVector.y * speed, ForceMode.Force);
            transform.rotation = Quaternion.Euler(new Vector3(boatTiltX, boatAngleFinal, boatAngle * 10));
            if (moveVector.y > 0)
            {
                engineParticles.Play();
                if (hasPlayedSound == false)

                {
                    motorSound.Play();
                    hasPlayedSound = true;
                }

            }
            else
            {
                engineParticles.Stop();
                if (hasPlayedSound == true)
                {
                    motorSound.Stop();
                    hasPlayedSound = false;
                }
            }
        }
        else
        {
            engineParticles.Stop();
            if (hasPlayedSound == true)
            {
                motorSound.Stop();
                hasPlayedSound = false;
            }
        }
    }
}

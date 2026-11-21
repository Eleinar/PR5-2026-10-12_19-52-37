using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    [Min(0)]
    private float speed;

    [SerializeField]
    private float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move.normalized * speed * Time.deltaTime); //важно нормализовать вектор движения, чтобы скорость была одинаковой во всех направлениях

        velocity.y += gravity * Time.deltaTime; //Time.deltaTime - это время между кадрами, чтобы гравитация была одинаковой на всех кадрах
        controller.Move(velocity * Time.deltaTime); //Time.deltaTme - чтобы скорость двжижения была одинаковой на всех кадрах
    }
}

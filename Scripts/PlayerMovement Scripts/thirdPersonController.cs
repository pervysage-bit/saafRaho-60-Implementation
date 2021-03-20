using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class thirdPersonController : MonoBehaviour
{
    public List<string> inventory;
    public CharacterController controller;
    public Transform camera;

    public float speed = 6f;
    public float smoothTurnAngle = 0.01f;
    float smoothTurnVelocity;


   // public bool running = false;
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        inventory = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {

            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;


            if (direction.magnitude >= 0.1f)
            {

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothTurnVelocity, smoothTurnAngle);

                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }

        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            string itemType = other.gameObject.GetComponent<CollectableScript>().itemType;
            print(" we have collected " + itemType);
            inventory.Add(itemType);
            print(" inventory length: " + inventory.Count);
            Destroy(other.gameObject);

            if(inventory.Count == 9)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                // Application.LoadLevel(1);
            }
        }

    }
}

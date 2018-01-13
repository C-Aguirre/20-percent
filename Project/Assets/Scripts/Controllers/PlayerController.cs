using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

/* Controls the player. Here we chose our "focus" and where to move. */

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public Interactable focus;  // Our current focus: Item, Enemy etc.

    public LayerMask movementMask;      // The ground

    PlayerMotor motor;      // Reference to our motor
    Camera cam;             // Reference to our camera

    // Get references
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        // If we press left mouse
        if (Input.GetMouseButtonDown(0))
        {
            // Shoot out a ray
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // If we hit
            if (Physics.Raycast(ray, out hit, movementMask))
            {
                motor.MoveToPoint(hit.point);

                RemoveFocus ();
            }
        }

        // If we press space
        if (Input.GetKeyDown("space"))
        {
            // Shoot out a ray
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // If we hit
            if (Physics.Raycast(ray, out hit, 100f))
            {
                SetFocus(hit.collider.GetComponent<Interactable>());
            }
        }

    }

    // Set our focus to a new focus
void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDeFocused();
            
            focus = newFocus;
            motor.FollowTarget(newFocus);
        }

        newFocus.OnFocused(transform); 


    }

    void RemoveFocus ()
    {
        if (focus != null)
            focus.OnDeFocused();
        
        focus = null;
        motor.StopFollowingTarget();
    }

}
using UnityEngine;

public class BladeHandler : MonoBehaviour {

    private bool isCutting;
    private Rigidbody2D rigidBody; // enable GameObject to act, to make gameObject move in realistic way !
    private Camera mainCamera;
    private CircleCollider2D circleCollider; // interacts with the 2D physics system
    private Vector2 previousPosition; // used to calculate speed of blade
    public float minSpeed; // blade has to move this fast to cut things

	// Use this for initialization
	void Start () {
        mainCamera = Camera.main;
        rigidBody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            StartCut();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopCut();
        }

        if (isCutting)
        {
            UpdateBlade();
        }
	}

    private void UpdateBlade() {
        // update blade position to where the mouse is in the world
        Vector2 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        rigidBody.position = newPosition;
        // calculate velocity of blade
        float velocity = (newPosition - previousPosition).magnitude * Time.deltaTime;
        // allow blade to cut things if its reached minimum required velocity
        circleCollider.enabled = (velocity > minSpeed) ? true : false;
        previousPosition = newPosition;
    }

    private void StartCut() {
        isCutting = true;
        previousPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void StopCut() {
        isCutting = false;
        circleCollider.enabled = false;
    }
}

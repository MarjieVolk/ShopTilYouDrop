using UnityEngine;
using System.Collections;

public class Grabber : MonoBehaviour
{
    private Camera camera;
    private SpringJoint2D joint;
    private Vector2 lastPosition;

    [SerializeField]
    private float linearDrag;
    [SerializeField]
    private float angularDrag;

    private float oldLinearDrag;
    private float oldAngularDrag;

    // Use this for initialization
    void Start()
    {
        // assumption: exactly one camera in scene
        camera = FindObjectOfType<Camera>();
        joint = gameObject.GetComponent<SpringJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 cursorWorldPostion = camera.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.position = cursorWorldPostion;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GrabObject(cursorWorldPostion);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && joint.connectedBody != null)
        {
            joint.connectedBody.angularDrag = oldAngularDrag;
            joint.connectedBody.drag = oldLinearDrag;
            joint.connectedBody = null;
        }

        lastPosition = cursorWorldPostion;
    }

    private void GrabObject(Vector2 cursorWorldPostion) {
        Debug.Log("Trying to grab");
        Rigidbody2D grabbedObject = FindGrabbableUnder(cursorWorldPostion);

        if (grabbedObject != null)
        {
            Debug.Log("Really grabbing");
            joint.anchor = gameObject.transform.InverseTransformPoint(cursorWorldPostion);
            joint.connectedBody = grabbedObject;
            joint.connectedAnchor = grabbedObject.transform.InverseTransformPoint(cursorWorldPostion);

            oldAngularDrag = grabbedObject.angularDrag;
            grabbedObject.angularDrag = angularDrag;

            oldLinearDrag = grabbedObject.drag;
            grabbedObject.drag = linearDrag;

            Destroy(grabbedObject.GetComponent<ConstantMovement>());
            grabbedObject.isKinematic = false;
        }
    }

    private static Rigidbody2D FindGrabbableUnder(Vector2 cursorWorldPostion)
    {
        Rigidbody2D grabbedObject = null;
        RaycastHit2D[] raycastHits = Physics2D.RaycastAll(cursorWorldPostion, Vector2.zero);
        foreach (RaycastHit2D raycastHit in raycastHits)
        {
            Rigidbody2D other = raycastHit.rigidbody;
            if (other.GetComponent<Grabbable>() != null)
            {
                grabbedObject = other;
            }
        }
        return grabbedObject;
    }
}

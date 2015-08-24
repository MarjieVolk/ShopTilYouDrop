using UnityEngine;
using System.Collections;

public class Grabber : MonoBehaviour
{
    private Camera sceneCamera;
    private SpringJoint2D joint;

    // The layer to which grabbed objects are moved.
    public LayerMask GrabbedObjectsMovedTo;
    public int GrabbedObjectsDepth;
    public float linearDrag;
    public float angularDrag;

    private float oldLinearDrag;
    private float oldAngularDrag;

    // Use this for initialization
    void Start()
    {
        // assumption: exactly one camera in scene
        sceneCamera = FindObjectOfType<Camera>();
        joint = gameObject.GetComponent<SpringJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 cursorWorldPostion = sceneCamera.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.position = cursorWorldPostion;

        if (Input.GetKey(KeyCode.Mouse0) && joint.connectedBody == null)
        {
            GrabObject(cursorWorldPostion);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && joint.connectedBody != null)
        {
            joint.connectedBody.angularDrag = oldAngularDrag;
            joint.connectedBody.drag = oldLinearDrag;
            joint.connectedBody = null;
        }
    }

    private void GrabObject(Vector2 cursorWorldPostion) {
        Rigidbody2D grabbedObject = FindGrabbableUnder(cursorWorldPostion);

        if (grabbedObject != null)
        {
            joint.anchor = gameObject.transform.InverseTransformPoint(cursorWorldPostion);
            joint.connectedBody = grabbedObject;
            joint.connectedAnchor = grabbedObject.transform.InverseTransformPoint(cursorWorldPostion);

            oldAngularDrag = grabbedObject.angularDrag;
            grabbedObject.angularDrag = angularDrag;

            oldLinearDrag = grabbedObject.drag;
            grabbedObject.drag = linearDrag;

            Destroy(grabbedObject.GetComponent<ConstantMovement>());
            grabbedObject.isKinematic = false;
            grabbedObject.gameObject.layer = GrabbedObjectsMovedTo.getFirstSet();
            grabbedObject.transform.position = new Vector3(grabbedObject.transform.position.x, grabbedObject.transform.position.y, GrabbedObjectsDepth);
        }
    }

    private static Rigidbody2D FindGrabbableUnder(Vector2 cursorWorldPostion)
    {
        if (Time.timeScale == 0)
        {
            return null;
        }
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

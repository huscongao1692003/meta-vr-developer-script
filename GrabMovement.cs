using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabMovement : MonoBehaviour
{
    public GameObject L_Hand, R_Hand, XRRig;
    public Vector3 difference, oldhandpos, currenthandpos;
    public float smoothSpeed = 1f;
    private bool scaleActivated = false;
    protected float previousControllerDistance;
    public float scaleMultiplier = 5f;
    public Vector3 minimumScale = Vector3.one;
    public float scaleActivationThreshold = 0.002f;
    public TrackingController rotationTrackingController = TrackingController.BothControllers;
    public float rotationActivationThreshold = 0.1f;
    public bool useOffsetTransform = true;
    protected bool rotationActivated = false;
    public float rotationMultiplier = 0.75f;
    protected Vector2 previousRotationAngle = Vector2.zero;
    protected bool rotationLeftControllerActivated;
    protected bool rotationRightControllerActivated;
    public Vector3 maximumScale = new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
    public GameObject objectToRotate;
   
    public enum TrackingController
    {
       
        LeftController,
        
        RightController,
        
        EitherController,
        
        BothControllers
    }

    public TrackingController scaleTrackingController = TrackingController.BothControllers;

   

    void Update()
    {

        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger) && OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            
            scaleActivated = true;
            rotationActivated = true;
            Scale();
            Rotate();
        }
        else
        {
            rotationActivated = false;
            scaleActivated = false;
        }


            if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger) && scaleActivated == false && rotationActivated == false)
        {
            

            currenthandpos = R_Hand.transform.position;
            // Get the difference between the previous frame's hand position and this frame's current hand position
            difference = difference = (oldhandpos - currenthandpos) * 2.6f; ;
            // Add that difference to the camerarig
            XRRig.transform.position += difference;
        }
        
        oldhandpos = R_Hand.transform.position;
    }


    protected virtual void Scale()
    {
        if (!scaleActivated)
        {
            return;
        }
        float currentDistance = GetControllerDistance();
        float distanceDelta = currentDistance - previousControllerDistance;
        if (Mathf.Abs(distanceDelta) >= scaleActivationThreshold)
        {
            XRRig.transform.localScale += (Vector3.one * Time.deltaTime * -Mathf.Sign(distanceDelta) * scaleMultiplier);
            XRRig.transform.localScale = new Vector3(Mathf.Clamp(XRRig.transform.localScale.x, minimumScale.x, maximumScale.x),
                Mathf.Clamp(XRRig.transform.localScale.y, minimumScale.y, maximumScale.y),
                Mathf.Clamp(XRRig.transform.localScale.z, minimumScale.z, maximumScale.z));
        }
        previousControllerDistance = currentDistance;
    }

    protected virtual float GetControllerDistance()
    {
        switch (scaleTrackingController)
        {
            case TrackingController.BothControllers:
                return Vector3.Distance(GetLeftControllerPosition(), GetRightControllerPosition());
            case TrackingController.LeftController:
                return Vector3.Distance(GetLeftControllerPosition(), XRRig.transform.localPosition);
            case TrackingController.RightController:
                return Vector3.Distance(GetRightControllerPosition(), XRRig.transform.localPosition);
            case TrackingController.EitherController:
                return Vector3.Distance(GetLeftControllerPosition(), XRRig.transform.localPosition) + Vector3.Distance(GetRightControllerPosition(), XRRig.transform.localPosition);
        }

        return 0f;
    }
    private Vector3 GetLeftControllerPosition()
    {
        return (OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch));
    }
    private Vector3 GetRightControllerPosition()
    {
        return (OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch));
    }
    //fixing some bug here
    protected virtual void Rotate()
    {
        if (!rotationActivated)
        {
            return;
        }

            Vector2 currentRotationAngle = GetControllerRotation();
            float newAngle = Vector2.Angle(currentRotationAngle, previousRotationAngle) * Mathf.Sign(Vector3.Cross(currentRotationAngle, previousRotationAngle).z);
            RotateByAngle(newAngle);
            previousRotationAngle = currentRotationAngle;
       
    }

    protected virtual Vector2 GetControllerRotation()
    {
        return new Vector2((GetLeftControllerPosition() - GetRightControllerPosition()).x, (GetLeftControllerPosition() - GetRightControllerPosition()).z);
    }

    protected virtual void RotateByAngle(float angle)
    {
        if (Mathf.Abs(angle) >= rotationActivationThreshold)
        {
            if (useOffsetTransform)
            {
                objectToRotate.transform.RotateAround(objectToRotate.transform.localPosition, Vector3.up, angle * rotationMultiplier);
            }
            else
            {
                objectToRotate.transform.Rotate(Vector3.up * (angle * rotationMultiplier));
            }
        }
    }
    
   
}


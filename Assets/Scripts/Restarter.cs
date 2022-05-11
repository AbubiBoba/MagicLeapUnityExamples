using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.MagicLeap;

public class Restarter : MonoBehaviour
{
    [SerializeField, Tooltip("KeyPose to track.")]
    private MLHandTracking.HandKeyPose _keyPoseToTrack = MLHandTracking.HandKeyPose.NoPose;
    [Space, SerializeField, Tooltip("Flag to specify if left hand should be tracked.")]
    private bool _trackLeftHand = true;

    [SerializeField, Tooltip("Flag to specify id right hand should be tracked.")]
    private bool _trackRightHand = true;
    private const float CONFIDENCE_THRESHOLD = 0.95f;


    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private float GetKeyPoseConfidence(MLHandTracking.Hand hand)
    {
        if (hand != null)
        {
            if (hand.KeyPose == _keyPoseToTrack)
            {
                return hand.HandKeyPoseConfidence;
            }
        }
        return 0.0f;
    }
    private void Update()
    {
        float confidenceLeft = 0.0f;
        float confidenceRight = 0.0f;
        if (_trackLeftHand)
        {
            #if PLATFORM_LUMIN
            confidenceLeft = GetKeyPoseConfidence(MLHandTracking.Left);
            #endif
        }

        if (_trackRightHand)
        {
            #if PLATFORM_LUMIN
            confidenceRight = GetKeyPoseConfidence(MLHandTracking.Right);
            #endif
        }

        float confidenceValue = Mathf.Max(confidenceLeft, confidenceRight);

        if(confidenceValue >= CONFIDENCE_THRESHOLD)
        {
            ReloadScene();
        }
    }
}

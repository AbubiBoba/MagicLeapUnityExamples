using UnityEngine;
using UnityEngine.XR.MagicLeap;
public class DynamicBeam : MonoBehaviour
{
    [SerializeField] private GameObject _controller;
    private LineRenderer _beamLine;
    [SerializeField] private Color _startColor;
    [SerializeField] private Color _endColor;
    private void Start()
    {
        _beamLine = GetComponent<LineRenderer>();
        _beamLine.startColor = _startColor;
        _beamLine.endColor = _endColor;
    }
    private void Update()
    {
        transform.position = _controller.transform.position;
        transform.rotation = _controller.transform.rotation;

        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit))
        {
            _beamLine.useWorldSpace = true;
            _beamLine.SetPosition(0, transform.position);
            _beamLine.SetPosition(1, hit.point);
        }
        else
        {
            _beamLine.useWorldSpace = false;
            _beamLine.SetPosition(0, Vector3.zero);
            _beamLine.SetPosition(1, Vector3.forward * 5f);
        }
    }
}

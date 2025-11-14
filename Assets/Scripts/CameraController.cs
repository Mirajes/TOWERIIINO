using UnityEngine;

public class CameraController : MonoBehaviour 
{
    [SerializeField] private static Camera _playerCamera;

    private static bool _isInteract = false;
    public static bool IsInteract => _isInteract;

    private static bool _isUsingAbiity = false;
    public static bool IsUsingAbility => _isUsingAbiity;

    #region Highlight
    [SerializeField] private LayerMask _interactableLayer;
    [SerializeField] private Material _highlightMaterial;

    private Material _originalMaterial;
    private Renderer _currentHighlightedRenderer;

    public void CheckForInteract()
    {
        Ray ray = _playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _interactableLayer))
        {
            if (_currentHighlightedRenderer != hit.collider.GetComponent<Renderer>())
            {
                RemoveHighlight();
                HighlightObject(hit.collider.gameObject);
            }
        }
        else
        {
            RemoveHighlight();
        }
    }

    private void HighlightObject(GameObject obj)
    {
        _currentHighlightedRenderer = obj.GetComponent<Renderer>();

        if (_currentHighlightedRenderer != null)
        {
            _originalMaterial = _currentHighlightedRenderer.material;
            _currentHighlightedRenderer.material = _highlightMaterial;
            _isInteract = true;
        }
    }
    private void RemoveHighlight()
    {
        if ( _currentHighlightedRenderer != null)
        {
            _currentHighlightedRenderer.material = _originalMaterial;
            _currentHighlightedRenderer = null;
            _isInteract = false;
        }
    }
    #endregion

    public static Vector3 GetWorldMousePos()
    {
        Ray ray = _playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            return hit.point;
        else
            return Vector3.zero;
    }
    private void Start()
    {
        _playerCamera = Camera.main;
    }

    private void Update()
    {
        CheckForInteract();
    }
}

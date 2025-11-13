using UnityEngine;

public class CameraController : MonoBehaviour 
{
    [SerializeField] private Camera _playerCamera;

    private static bool _isInteract = false;
    public static bool IsInteract => _isInteract;

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

    public Vector3 GetWorldMousePos() => _playerCamera.ScreenToWorldPoint(Input.mousePosition);

    private void Start()
    {
        _playerCamera = Camera.main;
    }

    private void Update()
    {
        CheckForInteract();
    }
}

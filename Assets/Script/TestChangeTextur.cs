using System;
using CustomEventBus;
using UnityEngine;


public class TestChangeTextur : MonoBehaviour
{
    [SerializeField] private Texture2D _texture;
    [Range(2, 528)]
    [SerializeField] private int _resolution = 128;
    [SerializeField] private FilterMode _filterMode;
    [SerializeField] private Collider _collider;
    [SerializeField] private TextureWrapMode _textureWrapMode;
    [SerializeField] private float _radiusOut = 5f;
    [SerializeField] private Color _color;


    private void Start()
    {
        if (_texture == null)
        {
            _texture = new Texture2D(_resolution, _resolution);
            gameObject.GetComponent<Renderer>().material.mainTexture = _texture;
        }
        if (_texture.width != _resolution)
            _texture.Reinitialize(_resolution, _resolution);

        _texture.filterMode = _filterMode;
        _texture.wrapMode = _textureWrapMode;
        _texture.Apply();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (_collider.Raycast(ray, out hit,100f))
            {
                int rayX = (int)(hit.textureCoord.x * _resolution);
                int rayY = (int)(hit.textureCoord.y* _resolution);
                for (int y = 0; y < _radiusOut; y++)
                    for (int x = 0; x < _radiusOut; x++)
                    {
                        _texture.SetPixel(x + rayX, y + rayY, _color);
                    }
                _texture.Apply();
            }
        }
    }
}

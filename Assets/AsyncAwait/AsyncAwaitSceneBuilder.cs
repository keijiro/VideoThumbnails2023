using UnityEngine;

sealed class AsyncAwaitSceneBuilder : MonoBehaviour
{
    [SerializeField] int _seed = 100;
    [SerializeField] int _width = 5;
    [SerializeField] int _rowCount = 10;
    [SerializeField] float _rowHeight = 0.1f;
    [SerializeField] Vector2 _columnWidth = new Vector2(0.1f, 0.5f);
    [SerializeField] float _columnInterval = 0.05f;
    [SerializeField] float _rowInterval = 0.05f;
    [SerializeField] Mesh _mesh = null;
    [SerializeField] Material _material = null;
    [SerializeField, ColorUsage(false, true)] Color _color1 = Color.red;
    [SerializeField, ColorUsage(false, true)] Color _color2 = Color.blue;
    [SerializeField, Range(0, 1)] float _color1Prob = 0.5f;

    void Start()
    {
        Random.InitState(_seed);

        var y = 0.0f;

        for (var row = 0; row < _rowCount; row++)
        {
            for (var x = 0.0f; x < _width;)
            {
                var sx = Random.Range(_columnWidth.x, _columnWidth.y);
                CreateBlock(transform, x + sx / 2, y, sx, _rowHeight, 0.01f);
                x += sx + _columnInterval;
            }

            y += _rowHeight + _rowInterval;
        }
    }

    void CreateBlock(Transform parent, float x, float y, float sx, float sy, float sz)
    {
        var go = new GameObject("Block");

        go.transform.parent = parent;
        go.transform.localPosition = new Vector3(x, y, 0);
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = new Vector3(sx, sy, sz);

        var mf = go.AddComponent<MeshFilter>();
        mf.sharedMesh = _mesh;

        var mr = go.AddComponent<MeshRenderer>();
        mr.material = _material;

        var c = Random.value < _color1Prob ? _color1 : _color2;
        mr.material.SetColor("_EmissiveColor", c);
    }
}

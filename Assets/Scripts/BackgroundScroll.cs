using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed;
    [SerializeField] private GameObject[] _backgrounds;
    [SerializeField] private float _endPoint;

    private Vector2 _startingPoint;

    private void Awake()
    {
        _startingPoint = _backgrounds[1].transform.position;
    }

    private void Update()
    {
        foreach (var background in _backgrounds)
        {
            var delta = _scrollSpeed * Time.deltaTime * Vector2.left;
            background.transform.Translate(delta);

            if (background.transform.position.x <= _endPoint)
            {
                background.transform.position = _startingPoint;
            }
        }
    }

}

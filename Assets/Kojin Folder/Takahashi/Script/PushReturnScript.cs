using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class PushReturnScript : MonoBehaviour    
{
    [SerializeField] private Button _leftButton = default;
    [SerializeField] private Button _rightButton = default;
    //  1,2,5,3,4 の順に登録
    [SerializeField] private List<Transform> _cardTransforms = new List<Transform>();
    [SerializeField] private float _cardSpeed = 2f;

    private List<Transform> _cardOrgTransforms = new List<Transform>();
    private List<Vector3> _newCardTransforms = new List<Vector3>();
    private List<Vector3> _oldCardTransforms = new List<Vector3>();

    //  1,2,5,3,4
    //  5,1,4,2,3
    //  4,5,3,1,2
    //  3,4,2,5,1
    //  2,3,1,4,5

    //  1,5,2,4,3
    //  2,1,3,5,4
    //  3,2,4,1,5
    //  
    private static readonly List<int> _cardIndexListOrg = new List<int>() { 0, 1, 4, 2, 3 };

    private int _selectIndex = 0;

    private List<int> _cardIndexList = new List<int>();

    private bool _isScrollAction = false;

    private bool _isRight = true;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private void OnEnable()
    {  
        _leftButton.onClick.AddListener(LeftScroll);
        _rightButton.onClick.AddListener(RightScroll);
    }

    private void OnDisable()
    {
        _leftButton.onClick.RemoveListener(LeftScroll);
        _rightButton.onClick.RemoveListener(RightScroll);
    }

    private void Init()
    {
        _cardIndexList.Clear();
        _cardIndexList.AddRange(_cardIndexListOrg);
        _selectIndex = 0;
        _cardOrgTransforms.Clear();
        _cardOrgTransforms.AddRange(_cardTransforms);
    }

    private void LeftScroll()
    {
        if (_isScrollAction) return;
        _isScrollAction = true;
        _newCardTransforms.Clear();
        _oldCardTransforms.Clear();
        foreach (int index in Enumerable.Range(0, 5))
        {
            _oldCardTransforms.Add(_cardOrgTransforms[index].localPosition);
            _newCardTransforms.Add(_cardOrgTransforms[(index + 4) % 5].localPosition);
        }
        _cardIndexList = _cardIndexList.Select(index => (index + 1) % 5).ToList();
        if (_isRight)
            ExchangeCard();
        _isRight = false;
        _cardIndexList.ForEach(index => _cardTransforms[index].SetAsFirstSibling());
        StartCoroutine(RotateScrollCo());
    }

    private void RightScroll()
    {
        if (_isScrollAction) return;
        _isScrollAction = true;
        _newCardTransforms.Clear();
        _oldCardTransforms.Clear();
        foreach (int index in Enumerable.Range(0, 5))
        {
            _oldCardTransforms.Add(_cardOrgTransforms[index].localPosition);
            _newCardTransforms.Add(_cardOrgTransforms[(index + 1) % 5].localPosition);
        }
        _cardIndexList = _cardIndexList.Select(index => (index + 4) % 5).ToList();
        if (!_isRight)
            ExchangeCard();
        _isRight = true;
        _cardIndexList.ForEach(index => _cardTransforms[index].SetAsFirstSibling());
        StartCoroutine(RotateScrollCo());
    }

    private void ExchangeCard()
    {
        var tmp = _cardIndexList[1];
        _cardIndexList[1] = _cardIndexList[2];
        _cardIndexList[2] = tmp;
        tmp = _cardIndexList[3];
        _cardIndexList[3] = _cardIndexList[4];
        _cardIndexList[4] = tmp;
    }

    private IEnumerator RotateScrollCo()
    {
        float timer = 0f;
        var pos = Vector3.zero;
        while (1f > timer)
        {
            timer += Time.deltaTime * _cardSpeed;
            if (1f < timer) timer = 1f;
            foreach (int index in Enumerable.Range(0, 5))
            {
                pos.x = Mathf.Lerp(_oldCardTransforms[index].x, _newCardTransforms[index].x, timer);
                pos.y = Mathf.Lerp(_oldCardTransforms[index].y, _newCardTransforms[index].y, timer);
                _cardTransforms[index].localPosition = pos;
            }
            yield return null;
        }

        _isScrollAction = false;
    }
}

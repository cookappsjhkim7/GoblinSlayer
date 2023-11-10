using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Button _btnNext;
    [SerializeField] private Button _btnPrev;
    [SerializeField] private Button _btnClose;

    [SerializeField] List<GameObject> _tutorialList;
    private int currentPage = 0;
    
    private void Awake()
    {
        _btnNext.onClick.AddListener(OnClickNext);
        _btnPrev.onClick.AddListener(OnClickPrev);
        _btnClose.onClick.AddListener(() => gameObject.SetActive(false));

    }

    private void OnClickNext()
    {
        if (currentPage >= _tutorialList.Count)
        {
            return;
        }
        
        ++currentPage;
        _tutorialList[currentPage].SetActive(true);
        
        
        if (currentPage == _tutorialList.Count - 1)
        {
            _btnNext.gameObject.SetActive(false);
            
        }
        else
        {
            _btnPrev.gameObject.SetActive(true);
            _btnNext.gameObject.SetActive(true);
        }
    }
    
    private void OnClickPrev()
    {
        if (currentPage == 0)
        {
            return;
        }

        _tutorialList[currentPage].SetActive(false);
        --currentPage;

        if (currentPage == 0)
        {
            _btnPrev.gameObject.SetActive(false);
        }
        else
        {
            _btnNext.gameObject.SetActive(true);
            _btnPrev.gameObject.SetActive(true);
        }
    }

    private void OnEnable()
    {
        currentPage = 0;
        _tutorialList[0].SetActive(true);
        _tutorialList[1].SetActive(false);
        _tutorialList[2].SetActive(false);

        _btnNext.gameObject.SetActive(true);
        _btnPrev.gameObject.SetActive(false);
    }
}

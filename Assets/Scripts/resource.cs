using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Threading;

public class resource : MonoBehaviour
{
    public TMP_Text resource_text;
    public Button _button;
    public GameObject Hero;


    public bool Item1 = false;
    public GameObject Purchased1;

    public int _resourceAmount = 0;
    [SerializeField] private int _heroPrice = 1;

    public void Update()
    {
        if (_button.onClick != null)
        {
            buy();
        }
    }

    private void Start()
    {
        //System.Threading.Thread.Sleep(50);
        StartCoroutine(ResourceCycle());
    }
    IEnumerator ResourceCycle()
    {
        while (true)
        {
            _resourceAmount++;
            resource_text.text = _resourceAmount.ToString();
            yield return new WaitForSeconds(3);
        }
    }


    public void buy()
    {
        if (_resourceAmount > _heroPrice)
        {
            _resourceAmount -= _heroPrice;
            Instantiate(Hero);
        }
    }


}
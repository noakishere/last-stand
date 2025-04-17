using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : SingletonMonoBehaviour<TextManager>
{
    [SerializeField]
    private TextMeshProUGUI canvasText;

    public void UpdateText(string newText)
    {
        StopCoroutine(RemoveText());
        
        canvasText.text = newText;

        StartCoroutine(RemoveText());
    }

    private IEnumerator RemoveText()
    {
        yield return new WaitForSeconds(5f);

        canvasText.text = "";
    }
}

using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class clear_scrollview : MonoBehaviour
{
    public Text content;
    public GameObject scrollContent;
    public GameObject scrollviewbar;
    Scrollbar sb;
    RectTransform rt;
    
    public Text counts;
    public bool onetime = true;

    private void Start()
    {
        rt = scrollContent.GetComponent<RectTransform>();
    }
    private void Update()
    {
        rt.sizeDelta = new Vector2(500, scrollContent.transform.childCount * (30));
        int count = scrollContent.transform.childCount;
        counts.text = $"{count} 명 검색됨.";
        if (scrollviewbar.activeSelf)
        {
            sb = scrollviewbar.GetComponent<Scrollbar>();
            if (sb.value < 0)
            {
                sb.value = 0.1f;
                //CreateContent();
            }
        }
    }
    public void CreateContent()
    {
        if (!onetime) return;
        onetime = false;

        string[] selectList = new string[] { "clearday", "nickname" };
        var bro = Backend.PlayerData.GetAllData("Clearlog", selectList, 100, null,TableSortOrder.ASC);

        if (!bro.IsSuccess())
        {
            Debug.LogError(bro.ToString());
            return;
        }

        LitJson.JsonData gameDataListJson = bro.FlattenRows();

        if (gameDataListJson.Count <= 0)
        {
            Debug.Log("데이터가 존재하지 않습니다");
            return;
        }
        

        foreach (LitJson.JsonData jsonData in bro.FlattenRows())
        {
            Text go = Instantiate(content, Vector3.zero, Quaternion.identity);
            go.text = jsonData["clearday"].ToString() + " - " + jsonData["nickname"].ToString();
            go.transform.SetParent(scrollContent.transform, false);
            Debug.Log("Ranking data loaded");
        }
    }

    public void UpperContent()
    {
        StartCoroutine(upperScrol());
    }
    IEnumerator upperScrol()
    {
        while (true)
        {
            yield return null;
            if (sb.value >= 1) break;
            sb.value += 0.1f;
        }
    }
}

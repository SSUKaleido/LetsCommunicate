using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;


[System.Serializable]
public class Item
{
    public Item(string _Type, string _Name, string _Explain, string _Number, bool _isUsing, string _Index)
    { Type = _Type; Name = _Name; Explain = _Explain; Number = _Number; isUsing = _isUsing; Index = _Index;}
    public string Type, Name, Explain, Number, Index;
    public bool isUsing;
}
public class GameManager : MonoBehaviour
{
    public TextAsset ItemDatabase;
    public List<Item> AllItemList, MyItemList, CurItemList;
    public string curType = "Character";
    public GameObject[] Slot, UsingImage;
    public UnityEngine.UI.Image[] TabImage, ItemImage;
    public Sprite TabIdleSprite, TabSelectSprite;
    public Sprite[] ItemSprite;
    public GameObject ExplainPanel;
    public RectTransform[] SlotPos;
    public RectTransform CanvasRect;
    IEnumerator PointerCoroutine;
    RectTransform ExplainRect;


    void Awake()
    {
        // 전체 아이템 리스트 불러오기
        string[] line = ItemDatabase.text.Substring(0, ItemDatabase.text.Length-1).Split('\n');
        for (int i =0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');
            AllItemList.Add(new Item(row[0], row[1], row[2], row[3], row[4] == "TRUE", row[5]));
        }
        
        Load();
        ExplainRect = ExplainPanel.GetComponent<RectTransform>();
    }
    private void Update()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(CanvasRect, Input.mousePosition, Camera.main, out Vector2 anchoredPos);
        //ExplainRect.anchoredPosition = anchoredPos + new Vector2(-180, -50);
    }
    public void SlotClick (int slotNum){
        Item CurItem = CurItemList[slotNum];
        Item UsingItem = CurItemList.Find(x => x.isUsing == true);

        if (curType == "Character"){
            // 하나만 장착
            if (UsingItem != null) UsingItem.isUsing = false;
            CurItem.isUsing = true;
        }
        else{
            // 장착 해도 되고 안해도 되고
            CurItem.isUsing = !CurItem.isUsing;
            if (UsingItem != null) UsingItem.isUsing = false;
            
        }

        Save();
    }

    public void TabClick(string tabName){
        // 현재 아이템 리스트에 클릭한 타입만 추가
        curType = tabName;
        CurItemList = MyItemList.FindAll(x => x.Type == tabName);

        
        for (int i = 0; i < Slot.Length; i++)
        {
            // 슬롯과 텍스트 보이기
            bool isExist = i < CurItemList.Count;
            Slot[i].SetActive(isExist);
            Slot[i].GetComponentInChildren<UnityEngine.UI.Text>().text = isExist ? CurItemList[i].Name : "";

            if (isExist)
            {
                ItemImage[i].sprite = ItemSprite[AllItemList.FindIndex(x => x.Name == CurItemList[i].Name)];
                UsingImage[i].SetActive(CurItemList[i].isUsing);
            }
        } 

        //탭 이미지
        int tabNum = 0;
        switch (tabName){
            case "Character": tabNum = 0; break;
            case "Balloon": tabNum = 1; break;
        }
        for (int i = 0; i < TabImage.Length; i++)
            TabImage[i].sprite = i == tabNum ? TabSelectSprite : TabIdleSprite;
    }
    
    
    public void PointerEnter(int slotNum)
    {
        // 슬롯에 마우스를 올리면 0.5초 후에 설명창 띄움
        PointerCoroutine = PointerEnterDelay(slotNum);
        StartCoroutine(PointerCoroutine);
        
        // 설명창에 이름, 이미지, 설명 나타내기
        ExplainPanel.GetComponentInChildren<UnityEngine.UI.Text>().text = CurItemList[slotNum].Name;
        ExplainPanel.transform.GetChild(2).GetComponent<UnityEngine.UI.Image>().sprite = Slot[slotNum].transform.GetChild(1).GetComponent<UnityEngine.UI.Image>().sprite;
        ExplainPanel.transform.GetChild(3).GetComponent<UnityEngine.UI.Text>().text = CurItemList[slotNum].Explain;
    }

    IEnumerator PointerEnterDelay (int slotNum)
    {
        yield return new WaitForSeconds (0.5f);
        ExplainPanel.SetActive(true);
    }

    public void PointerExit(int slotNum)
    {
        StopCoroutine(PointerCoroutine);
        ExplainPanel.SetActive(false);
    }
    void Save(){
        string jdata = JsonConvert.SerializeObject(MyItemList);
        File.WriteAllText(Application.dataPath + "/TestFiles/Resources/MyItemBase.txt", jdata);
        TabClick(curType);
    }

    void Load(){
        string jdata = File.ReadAllText(Application.dataPath + "/TestFiles/Resources/MyItemBase.txt");
        MyItemList = JsonConvert.DeserializeObject<List<Item>>(jdata);

        TabClick(curType);
    }
}

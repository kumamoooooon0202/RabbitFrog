using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class ScenarioTextControl : MonoBehaviour
{

    [SerializeField] private string[] sentence; // シナリオを格納する箱
    [SerializeField] private Text uiText;       // 

    [SerializeField, Range(0.01f, 0.3f)]
    float intervalForCharacterDisplay = 0.05f;  // １文字の表示にかかる時間

    [SerializeField]
    Image[] CharPos;
    [SerializeField]
    Sprite[] CharImage;

    Image[] CharPosTemp = new Image[2];

    //--------------------------------------------------
    // キャラの描画順を変える
    [SerializeField] private Canvas[] sortingGroup;
    //public List<SortingGroup> sortingGroups = new List<SortingGroup>(); 

    //public RectTransform InfantryRectTransform; // 歩兵
    //public RectTransform CavalryRectTransform;  // 騎兵
    //public RectTransform SamuraiRectTransform;  // 侍
    //--------------------------------------------------

    private int currentLine = 0;
    private string currentText = string.Empty;  // 現在の文字列
    private float timeUntilDisplay = 0;         // 表示にかかる時間
    private float timeElapsed = 1;              // 文字列の表示を開始した時間
    private int lastUpdateCharacter = -1;       // 表示中の文字列

    // 文字の表示が完了しているかどうか
    public bool IsCompleteDisplayText
    {
        get { return Time.time > timeElapsed + timeUntilDisplay; }
    }

    //====================================================
    [SerializeField, Header("MainCameraにアタッチ")]
    private Effect_Sketch effect_Sketch;

    private List<string> TextContant = new List<string>();
    private List<string> Character_Name = new List<string>();
    private List<string> Character_num = new List<string>();

    void Awake()
    {
        CSVReader("story_01", TextContant, Character_Name, Character_num);
    }

    void Start()
    {
        SetNextLine();
    }

    void Update()
    {
        //if (currentLine == sentence.Length && Input.GetMouseButtonDown(0) && !Scene_changing)
        if (currentLine == TextContant.Count && Input.GetMouseButtonDown(0) && !effect_Sketch.Scene_changing)
        {
            effect_Sketch.Scene_changing = true;
            StartCoroutine(effect_Sketch.NextScene("OptionScene"));
        }

        if (IsCompleteDisplayText)
        {
            //if (currentLine < sentence.Length && Input.GetMouseButtonDown(0))
            if (currentLine < TextContant.Count && Input.GetMouseButtonDown(0) && !effect_Sketch.Scene_changing)
            {
                SetNextLine();
            }
        }
        else
        {
            // 完了していない文字をすべて表示する
            if (Input.GetMouseButtonDown(0))
            {
                timeUntilDisplay = 0;
            }
        }


        // クリックから経過した時間が想定表示時間の何％か確認し、表示文字数を出す
        int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - timeElapsed) / timeUntilDisplay) * currentText.Length);

        // 表示文字数が前回の表示文字数と異なるならテキストを更新する
        if (displayCharacterCount != lastUpdateCharacter)
        {
            uiText.text = currentText.Substring(0, displayCharacterCount);
            lastUpdateCharacter = displayCharacterCount;
        }
    }

    private void SetNextLine()
    {
        //currentText = sentence[currentLine];
        //===================================イゴンヒ
        currentText = TextContant[currentLine];

        string name = Character_Name[currentLine];
        int num = int.Parse(Character_num[currentLine]);

        for (int i = 0; i < CharPos.Length; i++)
        {
            CharPos[i].color = new Color(0.5f, 0.5f, 0.5f,1.0f);
            sortingGroup[i].sortingOrder = 1;
        }

        switch (name)
        {
            case "歩兵":
                //CharPos[1].sprite = CharImage[0];
                CharPos[0].color = new Color(1, 1, 1, 1.0f);
                sortingGroup[0].sortingOrder = 10;
                break;
            
            case "群兵":
                //CharPos[1].sprite = CharImage[2];
                CharPos[1].color = new Color(1, 1, 1, 1.0f);
                sortingGroup[1].sortingOrder = 10;
                break;
            
            case "侍":
                //CharPos[2].sprite = CharImage[4];
                CharPos[2].color = new Color(1, 1, 1, 1.0f);
                sortingGroup[2].sortingOrder = 10;
                break;
            default:
                Debug.Log(name);
                break;
        }

        
        currentLine++;

        // 想定表示時間と現在の時刻をキャッシュ
        timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
        timeElapsed = Time.time;

        // 文字カウントを初期化
        lastUpdateCharacter = -1;
    }

    //=========================================================イゴンヒ
    /// <summary>
    /// CSV FIle読み込む
    /// </summary>
    /// <param name="CSVPATH">読み込むFile名</param>
    /// <param name="text">台本を保存</param>
    /// <param name="Name">キャラクター名前を保存</param>
    void CSVReader(string CSVPATH,
                List<string> text,
                List<string> Name,
                List<string> num)
    {
        TextAsset data;

        data = Resources.Load(CSVPATH, typeof(TextAsset)) as TextAsset;
        StringReader sr = new StringReader(data.text);

        string line;
        string[] text_split;
        line = sr.ReadLine();

        while (line != null)
        {
            text_split = line.Split(',');
            Name.Add(text_split[0]); //name, text, num
            text.Add(text_split[1]); // 0  , 1   , 2
            num.Add(text_split[2]);
            line = sr.ReadLine();
        }
        //1行目を解除
        text.RemoveAt(0);
        Name.RemoveAt(0);
        num.RemoveAt(0);
    }
}
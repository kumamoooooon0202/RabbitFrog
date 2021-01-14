using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewManager : MonoBehaviour
{
    public Image panel;
    public GameObject preview;
    public Text nameText;
    public Text explanationText;
    public Text costText;
    public Image characterImage;
    public Image iconImage;
    public CharacterDeta data;

    public bool isPreview = false;

    void Start()
    {
        panel.enabled = false;
        preview.SetActive(false);
        data.SetExplanation();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isPreview)
        {
            DisplayPreview();
        }
    }

    public void DisplayPreview()
    {
        panel.enabled = !panel.enabled;
        isPreview = !isPreview;
        preview.SetActive(isPreview);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//using UnityEngine.UIElements;
using UnityEngine.UI;
using static DragDrop;

public class ItemSlot : MonoBehaviour, IDropHandler, IEndDragHandler
{

    private Picture picture;
    public string dragName;
    private GameManager gameManager;


    void Awake()
    {
        gameManager = this.transform.parent.transform.parent.GetComponent<GameManager>();
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        bool match = false;

        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<Transform>().SetParent(GetComponent<Transform>().transform);
            eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
            string imageName = eventData.pointerDrag.GetComponent<Picture>().getName();
            string slotName = this.transform.name;
            if (gameManager.isMatch(imageName, slotName))
            {
                match = true;
                gameManager.nextRound(match, eventData.pointerDrag);
            } else
            {
                match = false;
                gameManager.nextRound(match, eventData.pointerDrag);
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPicture(Picture pic)
    {
        this.picture = pic;
    }

    private IEnumerator waitABit(int secs)
    {
            yield return new WaitForSeconds(secs);
    }

    public void loadImage(Sprite img) { 
        if (this.gameObject.name == "startSlot")
        {
            GameObject slot = this.transform.GetChild(0).gameObject;
            slot.GetComponent<Image>().sprite = img;
        }
    }
}

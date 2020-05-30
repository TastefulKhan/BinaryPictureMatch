using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Picture picture;
    public ItemSlot startSlot;
    public Text scoreText;
    private string difficulty;
    private Sprite[] imgArray;
    private Sprite[] shuffledImgArray;
    public Settings settings;
    private int matchCount;
    private AudioSource audio_source;
    private AudioClip sound;

    void Start()
    {
        
    }

    private void Awake()
    {
        audio_source = gameObject.AddComponent<AudioSource>();
        if (GameObject.Find("Settings") != null)
        {
            settings = GameObject.Find("Settings").GetComponent<Settings>();
            difficulty = settings.getDifficulty();
        } else
        {
            difficulty = "easy";
        }
        imgArray = (Resources.LoadAll<Sprite>("Graphics/Pictures"));
        shuffledImgArray = shuffle((Sprite[])imgArray.Clone());
        if (difficulty == "easy") {
            Array.Resize(ref shuffledImgArray, 10);
        }
        matchCount = shuffledImgArray.Length;
        getNext();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Pictures left: " + matchCount;

    }

    // Array functions

    // getNextImg
    public void getNext()
    {
        if (shuffledImgArray.Length > 0)
        {
            picture.loadImage(shuffledImgArray[0]);
            
            
        } else
        {
            SceneManager.LoadScene("Congratulations");
        }
         
    }

    // removeImg
    public void removeImage()
    {
        List<Sprite> tempList = new List<Sprite>(shuffledImgArray);
        tempList.RemoveAt(0);
        shuffledImgArray = tempList.ToArray();
    }

    // sendToBack

    // checkResult
    public bool isMatch(string img, string slot)
    {
        if (img.Substring(0, 3) == slot)
        {
            // play congrats
            sound = Resources.Load<AudioClip>("Audio/yeah");
            audio_source.clip = sound;
            audio_source.Play();
            // delete pic from list
            // load the next image 
            // reset the position of the picture
            return true;
        } else
        {
            // play bah bow
            sound = Resources.Load<AudioClip>("Audio/wrong");
            audio_source.clip = sound;
            audio_source.Play();
            // put the image to the back of the queue
            // load next image 
            // reset the position of the image
            return false;
        }
    }

    private Sprite[] shuffle(Sprite[] imgArray)
    {
        Sprite[] shuffledArray = new Sprite[imgArray.Length];
        int rndNo;

        System.Random rnd = new System.Random();
        for (int i = imgArray.Length; i >= 1; i--)
        {
            rndNo = rnd.Next(1, i + 1) - 1;
            shuffledArray[i - 1] = imgArray[rndNo];
            imgArray[rndNo] = imgArray[i - 1];
        }
        return shuffledArray;
    }

    public void nextRound(bool match, GameObject image)
    {
        if (match)
        {
            // play congrats
            // wait for secs
            StartCoroutine(waitABit(match));
            

        } else
        {
            // play bah bow
            // wait for secs
            StartCoroutine(waitABit(match));
            
        }

    }

    private IEnumerator waitABit(bool match)
    {
        if (match)
        {
            yield return new WaitForSeconds(3.0f);
            // delete pic from list
            removeImage();
            matchCount = matchCount - 1;
            // load the next image 
            getNext();
            // reset currentParent of picture
            picture.GetComponent<Transform>().SetParent(startSlot.GetComponent<Transform>());
            // reset the position of the picture
            picture.GetComponent<RectTransform>().position = startSlot.GetComponent<RectTransform>().position;
        } else
        {
            yield return new WaitForSeconds(3.0f);
            // put the image to the back of the queue
            putToBack();
            // load next image 
            getNext();
            // reset currentParent of picture
            picture.GetComponent<Transform>().SetParent(startSlot.GetComponent<Transform>());
            // reset the position of the picture
            picture.GetComponent<RectTransform>().position = startSlot.GetComponent<RectTransform>().position;
        }
        
    }

    public void putToBack()
    {
        Sprite currentImg = shuffledImgArray[0];
        
        for (int i = 0; i < (shuffledImgArray.Length - 1); i++)
        {
            shuffledImgArray[i] = shuffledImgArray[i + 1];
        }
        shuffledImgArray[shuffledImgArray.Length - 1] = currentImg;
    }
}

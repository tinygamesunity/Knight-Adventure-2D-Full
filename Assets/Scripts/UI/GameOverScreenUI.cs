using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameOverScreenUI : MonoBehaviour {

    [SerializeField] private Image gameOverImage;
    [SerializeField] private float spriteChangeDuration = 0.08f;
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Sprite[] spriteList;

    private bool isButtonsActive;
    private int currentSpriteIndex;
    private float nextSpriteChangeTime = 0f;


    private void Awake() {
        isButtonsActive = false;
        currentSpriteIndex = 0;

        playButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.Level1Scene);
        });

        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }


    private void Update() {
        gameOverImage.gameObject.SetActive(true);
        ChangeSprites();
    }


    private void ChangeSprites() {
        if (Time.time > nextSpriteChangeTime && currentSpriteIndex < spriteList.Length) {
            gameOverImage.sprite = spriteList[currentSpriteIndex];
            currentSpriteIndex += 1;
            nextSpriteChangeTime = Time.time + spriteChangeDuration;
        } else if (currentSpriteIndex == spriteList.Length && !isButtonsActive) {
            isButtonsActive = true;
            ActivateButtons();
        }
    }

    private void ActivateButtons() {
        playButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }

    //private void DeActivateButtons() {
    //    playButton.gameObject.SetActive(false);
    //    quitButton.gameObject.SetActive(false);
    //}

}

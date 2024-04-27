using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldColnCounter : Singleton<GoldColnCounter> {

   // public static GoldColnCounter Instance { get; private set; }

    [SerializeField] private TMP_Text goldCoinText;

    protected override void Awake() {
        base.Awake();
    }

    public void SetGoldCoinAmount(int goldCoinAmount) {
        goldCoinText.text = goldCoinAmount.ToString("D3");
    }

}

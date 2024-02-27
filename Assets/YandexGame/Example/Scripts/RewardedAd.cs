using UnityEngine;
using UnityEngine.UI;

namespace YG.Example
{
    public class RewardedAd : MonoBehaviour
    {
        [SerializeField] int AdID;
        [SerializeField] Text textMoney;

        int moneyCount = 0;

        private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;
        private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;

        void Rewarded(int id)
        {
            if (id == AdID)
                AdMoney();
        }

        void AdMoney()
        {
            YandexGame.savesData.energyInClick *= 2;
            YandexGame.savesData.energyInSecond *= 2;
        }
    }
}
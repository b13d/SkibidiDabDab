
using System.Collections;
using System.Collections.Generic;

namespace YG
{
    [System.Serializable]

    public class Achievements
    {
        public int click = 0;
        public int playTime = 0;
        public int buy = 0;
        public int haveMoney = 0;
        public int thingBuy = 0;
        public int spend = 0;

        public int[] achievementsCompleted = new int[10];
    }

    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Тестовые сохранения для демо сцены
        // Можно удалить этот код, но тогда удалите и демо (папка Example)
        public int money = 1;                       // Можно задать полям значения по умолчанию
        public string newPlayerName = "Hello!";
        public bool[] openLevels = new bool[3];

        // Ваши сохранения

        public int energy = 0;
        public int energyInClick = 1;
        public int test = 10;
        public int energyInSecond = 1;
        public int damage = 1;
        public bool firstTry = true;
        public int timerToUnblockReward = 300;
        public bool wasShowReward = false;
        public int maxRecord = 0;

        public Achievements achievements = new Achievements();
        
        public int[] priceThings = new int[15];
        public int[] percentAddThings = new int[15];
        public int[] firstBuyThing = new int[15];

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива

            openLevels[1] = true;
        }
    }
}

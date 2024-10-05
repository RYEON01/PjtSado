using UnityEngine;

public class BattleCharacter : MonoBehaviour
{
    private static BattleCharacter instance;

    public static BattleCharacter Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BattleCharacter>();
                Debug.Log("BattleCharacter instance: " + instance);
                if (instance == null)
                {
                    GameObject obj = new GameObject("BattleCharacter");
                    instance = obj.AddComponent<BattleCharacter>();
                    DontDestroyOnLoad(obj);
                }
            }
            return instance;
        }
    }
    
    private static BattleCharacter enemyInstance;

    public static BattleCharacter EnemyInstance
    {
        get
        {
            if (enemyInstance == null)
            {
                enemyInstance = FindObjectOfType<BattleCharacter>();
                if (enemyInstance == null)
                {
                    GameObject obj = new GameObject("EnemyCharacter");
                    enemyInstance = obj.AddComponent<BattleCharacter>();
                    DontDestroyOnLoad(obj);
                }
            }
            return enemyInstance;
        }
        set
        {
            enemyInstance = value;
        }
    }
    public bool StatsInitialized { get; protected set; }
    public PentagonGraph PentagonGraph { get; set; }
    
    public virtual string GetDialogue()
    {
        return "Default dialogue";
    }
    
    public string Name { get; set; }
    public int Water { get; set; }
    public int Fire { get; set; }
    public int Earth { get; set; }
    public int Wood { get; set; }
    public int Metal { get; set; }
    public int HP { get; set; }
    
    public int WoodElement { get; set; }
    public int FireElement { get; set; }
    public int EarthElement { get; set; }
    public int MetalElement { get; set; }
    public int WaterElement { get; set; }
    
    public int WaterStat { get; set; }
    public int FireStat { get; set; }
    public int EarthStat { get; set; }
    public int WoodStat { get; set; }
    public int MetalStat { get; set; }
    
    public int Compassion { get; set; }
    
    public virtual void InitializeStats()
    {
        // This method should be overridden in each subclass
    }

    public int RollDice(string element)
    {
        int maxRoll = 0;
        switch (element.ToLower())
        {
            case "water":
                maxRoll = WaterStat;
                break;
            case "fire":
                maxRoll = FireStat;
                break;
            case "earth":
                maxRoll = EarthStat;
                break;
            case "wood":
                maxRoll = WoodStat;
                break;
            case "metal":
                maxRoll = MetalStat;
                break;
        }
        return Random.Range(1, maxRoll + 1);
    }
}

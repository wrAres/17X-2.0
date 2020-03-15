using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AIDataManager : MonoBehaviour
{
    private static Dictionary<string, int> spellAccessCount;
    private static Dictionary<string, int> spellAccessStandardCount;
    private static string[] spellListBeforeWaterBoss;
    private static string[] elementListBeforeWaterBoss;

    public static int wrongItemPlacementCount = 0;
    public static bool gentlypassthedoor = true;

    public static int movingPuzzleMoves = 0;
    public static float movingPuzzleTime = 0.0f;
    

	static int incorrectMirrorCount = 0;
	static List<double> timeForSpellUnlock = new List<double>();
    static List<double> bestTimeForSpellUnlock = new List<double>(); //TODO add perfect times for spell discovery
	static int nonExistentRecipeTries = 0;

    public static int walkingPuzzleFalls = 0;
    public static float previousUnlockTime = 0;
    // Start is called before the first frame update
    void Start()
    {   
        spellListBeforeWaterBoss = new string[] {"Taiji Key", "Board", "Life Water", "Taoist Wind", "Firewood", "Glowing Sun", "Dirt"};
        elementListBeforeWaterBoss = new string[] {"thunder", "sun", "wind", "moon", "water", "fire", "wood", "earth", "metal"};
        spellAccessCount = new Dictionary<string, int>();
        for (int i = 0; i < spellListBeforeWaterBoss.Length; i++) {
            spellAccessCount.Add(spellListBeforeWaterBoss[i], 0);
        }
        for (int i = 0; i < elementListBeforeWaterBoss.Length; i++) {
            spellAccessCount.Add(elementListBeforeWaterBoss[i], 0);
        }
        spellAccessCount.Add("", 0);

        spellAccessStandardCount = new Dictionary<string, int>();
        spellAccessStandardCount.Add("thunder", 0);
        spellAccessStandardCount.Add("sun", 0);
        spellAccessStandardCount.Add("wind", 0);
        spellAccessStandardCount.Add("moon", 0);
        spellAccessStandardCount.Add("Taiji Key", 0);
        spellAccessStandardCount.Add("Board", 0);
        spellAccessStandardCount.Add("Life Water", 0);
        spellAccessStandardCount.Add("Taoist Wind", 0);
        spellAccessStandardCount.Add("Firewood", 0);
        spellAccessStandardCount.Add("Glowing Sun", 0);
        spellAccessStandardCount.Add("Dirt", 0);
        spellAccessStandardCount.Add("water", 0);
        spellAccessStandardCount.Add("fire", 0);
        spellAccessStandardCount.Add("wood", 0);
        spellAccessStandardCount.Add("earth", 0);
        spellAccessStandardCount.Add("metal", 0);
        spellAccessStandardCount.Add("", 0);
    }

    public static void UpdateStandardSpellCount(string spell, int count) {
        spellAccessStandardCount[spell] = spellAccessStandardCount[spell] + count;
    }

    public static void IncrementSpellAccess(string spell) {
        spellAccessCount[spell] = spellAccessCount[spell] + 1;
    }

    public static void IncrementElementAccess(TalisDrag.Elements e) {
        string element = "";
        if (e.Equals(TalisDrag.Elements.METAL))
            element = "metal";
        else if (e.Equals(TalisDrag.Elements.WOOD))
            element = "wood";
        else if (e.Equals(TalisDrag.Elements.FIRE))
            element = "fire";
        else if (e.Equals(TalisDrag.Elements.WATER))
            element = "water";
        else if (e.Equals(TalisDrag.Elements.EARTH))
            element = "earth";
        else if (e.Equals(TalisDrag.Elements.THUNDER))
            element = "thunder";
        else if (e.Equals(TalisDrag.Elements.WIND))
            element = "wind";
        else if (e.Equals(TalisDrag.Elements.SUN))
            element = "sun";
        else if (e.Equals(TalisDrag.Elements.MOON))
            element = "moon";
        spellAccessCount[element] = spellAccessCount[element] + 1;
    }

    public double TalismanSmartness() {
        double smartOnTalisman = 0; // the higher the smarter; max 1
        for (int i = 0; i < spellListBeforeWaterBoss.Length; i++) {
            string spell = spellListBeforeWaterBoss[i];
            smartOnTalisman += Math.Exp(spellAccessStandardCount[spell] - spellAccessCount[spell]) / 16.0f;
        }

        for (int i = 0; i < elementListBeforeWaterBoss.Length; i++) {
            string element = elementListBeforeWaterBoss[i];
            smartOnTalisman += Math.Exp(spellAccessStandardCount[element] - spellAccessCount[element]) / 16.0f;
        }
        return smartOnTalisman;
    }

    public double ItemPlacementSmartness() {
        return Math.Exp(wrongItemPlacementCount);
    }

    public static string DecideTrigram() {
        int sum = 0;
        if (sum == 32) {
            print("\u2630"); //qian; Firmament
        } else if (sum > 32 && sum <= 35) {
            print("\u2637"); //kun; Ground
        }  else if (sum > 35 && sum <= 38) {
            print("\u2633"); //zhen; Thunder
        }  else if (sum > 38 && sum <= 40) {
            print("\u2634"); //xun; Wind
        }  else if (sum > 40 && sum <= 42) {
            print("\u2635"); //kan; Water
        }  else if (sum > 42 && sum <= 45) {
            print("\u2632"); //li; Fire
        }  else if (sum > 45 && sum <= 48) {
            print("\u2635"); //gen; Mountain
        }  else {
            print("\u2631"); //yue; Lake
        }
        return "";
    }
    

	public static void ClickedWrongMirror()
	{
		incorrectMirrorCount++;
	}
	public static void TryNonExistentRecipe()
	{
		nonExistentRecipeTries++;
	}
	public static void DiscoverNewSpell(float time)
	{
		timeForSpellUnlock.Add(time);
	}

    public double MirrorSmartness()
    {
        return Mathf.Exp(incorrectMirrorCount * -1);
    }

    public double SpellSmartness()
    {
        double totalsmartness = 0;
        for(int i = 0; i < timeForSpellUnlock.Count; i++)
        {
            totalsmartness += Math.Exp(timeForSpellUnlock[i] - bestTimeForSpellUnlock[i]);
        }
        return Math.Exp(totalsmartness * -1);
    }

    public double RecipeSmartness()
    {
        return Math.Exp(nonExistentRecipeTries * -1);
    }

    public double MovingPuzzleSmartness()
    {
        // Smartness is (Puzzle Moves + Puzzle Time)
        return Math.Exp((movingPuzzleMoves + movingPuzzleTime) * -1);
    }

    public double WalkingPuzzleSmartness()
    {
        // Smartness is Puzzle Falls
        return Math.Exp(walkingPuzzleFalls * -1);
    }
}

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

    public int movingPuzzleMoves = 0;
    public float movingPuzzleTime = 0.0f;


	int incorrectMirrorCount = 0;
	List<float> timeForSpellUnlock = new List<float>();
	int nonExistentRecipeTries = 0;

    public int walkingPuzzleFalls = 0;
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
            spellAccessCount.Add(spellListBeforeWaterBoss[i], 0);
        }
        spellAccessCount.Add("", 0);

        spellAccessStandardCount = new Dictionary<string, int>();
        spellAccessStandardCount.Add("thunder", 0);
        spellAccessStandardCount.Add("sun", 3);
        spellAccessStandardCount.Add("wind", 1);
        spellAccessStandardCount.Add("moon", 1);
        spellAccessStandardCount.Add("Taiji Key", 1);
        spellAccessStandardCount.Add("Board", 1);
        spellAccessStandardCount.Add("Life Water", 1);
        spellAccessStandardCount.Add("Taoist Wind", 1);
        spellAccessStandardCount.Add("Firewood", 1);
        spellAccessStandardCount.Add("Glowing Sun", 1);
        spellAccessStandardCount.Add("Dirt", 2);
        spellAccessStandardCount.Add("water", 6);
        spellAccessStandardCount.Add("fire", 2);
        spellAccessStandardCount.Add("wood", 4);
        spellAccessStandardCount.Add("earth", 7);
        spellAccessStandardCount.Add("metal", 0);
        spellAccessStandardCount.Add("", 0);
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

    public static double TalismanSmartness() {
        double smartOnTalisman = 0; // the higher the smarter; max 1
        for (int i = 0; i < spellListBeforeWaterBoss.Length; i++) {
            string spell = spellListBeforeWaterBoss[i];
            smartOnTalisman += Math.Exp(spellAccessCount[spell] - spellAccessStandardCount[spell]) / 16.0f;
        }

        for (int i = 0; i < elementListBeforeWaterBoss.Length; i++) {
            string element = elementListBeforeWaterBoss[i];
            smartOnTalisman += Math.Exp(spellAccessCount[element] - spellAccessStandardCount[element]) / 16.0f;
        }
        return smartOnTalisman;
    }

    public static double ItemPlacementSmartness() {
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
    

	public void ClickedWrongMirror()
	{
		incorrectMirrorCount++;
	}
	public void TryNonExistentRecipe()
	{
		nonExistentRecipeTries++;
	}
	public void DiscoverNewSpell(float time)
	{
		timeForSpellUnlock.Add(time);
	}
}

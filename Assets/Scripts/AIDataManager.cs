using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDataManager : MonoBehaviour
{
    private static Dictionary<string, int> spellAccessCount;
    private static Dictionary<string, int> spellAccessStandardCount;
    // Start is called before the first frame update
    void Start()
    {
        spellAccessCount = new Dictionary<string, int>();
        spellAccessCount.Add("thunder", 0);
        spellAccessCount.Add("sun", 0);
        spellAccessCount.Add("wind", 0);
        spellAccessCount.Add("moon", 0);
        spellAccessCount.Add("Taiji Key", 0);
        spellAccessCount.Add("Board", 0);
        spellAccessCount.Add("Life Water", 0);
        spellAccessCount.Add("Taoist Wind", 0);
        spellAccessCount.Add("Firewood", 0);
        spellAccessCount.Add("Glowing Sun", 0);
        spellAccessCount.Add("Dirt", 0);
        spellAccessCount.Add("water", 0);
        spellAccessCount.Add("fire", 0);
        spellAccessCount.Add("wood", 0);
        spellAccessCount.Add("earth", 0);
        spellAccessCount.Add("metal", 0);
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
        print(spell);
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

    public static string DecideTrigram() {
        int sum = 0;
        foreach(KeyValuePair<string, int> s in spellAccessCount) {
            sum = sum + (int)s.Value;
        }
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
    
}

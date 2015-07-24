using UnityEngine;
using System.Collections.Generic;

public class CBskills {
    public List<string> skillName { get; set; }
    public List<int> skillLevel { get; set; }
    public List<int> skillCosts { get; set; }
    public List<int> skillMods { get; set; }

    public CBskills() {
        skillName.Add("Axe");
        skillName.Add("Dagger");
        skillName.Add("Unarmed");
        skillName.Add("Hammer");
        skillName.Add("Polearm");
        skillName.Add("Spear");
        skillName.Add("Staff");
        skillName.Add("Sword");
        skillName.Add("Archery");
        skillName.Add("Crossbow");
        skillName.Add("Sling");
        skillName.Add("Thrown");
        skillName.Add("Armor");
        skillName.Add("Dual Weapon");
        skillName.Add("Shield");

        skillName.Add("Bardic");
        skillName.Add("Conjuring");
        skillName.Add("Druidic");
        skillName.Add("Illusion");
        skillName.Add("Necromancy");
        skillName.Add("Shamanic");
        skillName.Add("Sorcery");
        skillName.Add("Summoning");
        skillName.Add("Spellcraft");
        skillName.Add("Focus");

        skillName.Add("Armorsmithing");
        skillName.Add("Tailoring");
        skillName.Add("Fletching");
        skillName.Add("Weaponsmithing");
        skillName.Add("Lapidary");

        for (int i = 0; i < 30; i++) {
            skillLevel.Add(0);
            skillMods.Add(0);
        }
    }

    void SetSkillCosts(List<int> skillmods) {
        this.skillMods = skillmods;
        for (int i = 0; i < skillMods.Count; i++) {
            skillCosts[i] = 50 + skillMods[i];
        }
    }
}

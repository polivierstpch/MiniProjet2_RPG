using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Final_DD
{
    class Character
    {
        private string characterClass, weapon, armor;
        private int maxHp, currentHp, maxMp, currentMp, attack, defense, numOfPotions;
        private bool isDefending;

        //AXcceseur
        public string CharacterClass { get { return characterClass; } }
        public int MaxHP { get { return maxHp; } }
        public int CurrentHP { get { return currentHp; } set { currentHp = value; } }
        public int MaxMP { get { return maxMp; } }
        public int CurrentMP { get { return currentMp; } set { currentMp = value; } }
        public int NumOfPotions { get { return numOfPotions; } set { numOfPotions = value; } }
        public string Weapon { get { return weapon; } set { weapon = value; } }
        public int Attack { get { return attack; } set { attack = value; } }
        public string Armor { get { return armor; } set { armor = value; } }
        public int Defense { get { return defense; } set { defense = value; } }
        public bool IsDefending { get { return isDefending; } set { isDefending = value; } }

        public Character(string characterClass) // public Personnage (string typePerso)
        {
            switch (characterClass)
            {
                case "guerrier":
                    this.characterClass = characterClass;
                    weapon = "Epée à deux mains";
                    armor = "Armure en cuir";
                    maxHp = 200;
                    currentHp = 200;
                    maxMp = 100;
                    CurrentMP = 100;
                    Attack = 5;
                    Defense = 4;
                    
                    break;
                case "mage":;
                    this.characterClass = characterClass;
                    Weapon = "Dague";
                    Armor = "Cape elfique";
                    maxHp = 100;
                    CurrentHP = 100;
                    maxMp = 200;
                    CurrentMP = 200;
                    Attack = 3;
                    Defense = 6;        
                    break;
            }

            NumOfPotions = 2;
        }
        public void TakeDamage(int damage)
        {
            if (isDefending == true)
            {
                damage = Math.Max(0, damage - this.defense);
            }

            this.currentHp -= damage;

            GameManager.AddToGameLog($"Vous prenez {damage} points de dégâts.");
        }
        public void UsePotions(string lifeOrMana)
        {
            if (this.numOfPotions > 0)
            {
                numOfPotions--;
                switch (lifeOrMana)
                {
                    case "Vie":
                        if (this.currentHp + 50 < this.MaxHP)
                        {
                            this.currentHp += 50;
                        }
                        else
                        {
                            this.currentHp = this.maxHp;
                        }
                        break;
                    case "Mana":
                        if (this.currentMp + 50 < this.MaxMP)
                        {
                            this.currentMp += 50;
                        }
                        else
                        {
                            this.currentMp = this.maxMp;
                        }
                        break;
                }
            }
        }
        public int SpecialAttack()
        {
            switch (this.characterClass)
            {
                case "guerrier":
                    if (this.currentMp >= 20)
                    {
                        this.currentMp -= 20;

                        // Damage dealt is 10
                        return 10;
                    }
                    else
                    {
                        GameManager.AddToGameLog("Vous n'avez pas assez de mana pour lancer votre attaque spéciale!");
                        return 0;
                    }
                case "mage":
                    if (this.currentMp >= 25)
                    {
                        this.currentMp -= 25;
                        
                        return 15;
                    }
                    else
                    {
                        GameManager.AddToGameLog("Vous n'avez pas assez de mana pour lancer votre attaque spéciale!");
                        return 0;
                    }
                default:
                    return 0;
            }
        } 
    }
}

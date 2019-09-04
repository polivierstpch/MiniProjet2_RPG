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

        public Character(string characterClass)
        {
            switch (characterClass)
            {
                case "guerrier":
                case "1":
                    this.characterClass = "guerrier";
                    weapon = "Epée à deux mains";
                    armor = "Armure en cuir";
                    maxHp = 200;
                    currentHp = 200;
                    maxMp = 100;
                    CurrentMP = 100;
                    Attack = 10;
                    Defense = 7;
                    
                    break;
                case "mage":
                case "2":
                    this.characterClass = "mage";
                    Weapon = "Dague";
                    Armor = "Cape elfique";
                    maxHp = 100;
                    CurrentHP = 100;
                    maxMp = 200;
                    CurrentMP = 200;
                    Attack = 7;
                    Defense = 10;        
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

            // If health goes under 0 from damage, set it to 0;
            this.currentHp = this.currentHp - damage >= 0 ? this.currentHp - damage : 0;

            if (damage > 0)
                GameManager.AddToGameLog($"Vous prenez {damage} points de dégâts.");
            else
                GameManager.AddToGameLog($"Vous ne prenez aucun dégâts.");
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
                            GameManager.AddToGameLog($"Vous vous soignez de 50 points de vie.");
                        }
                        else if (this.currentHp == this.maxHp)
                        {
                            GameManager.AddToGameLog("En prenant la potion, vous constatez votre parfaite santé et remettez celle-ci dans votre sac.");
                            numOfPotions++;
                        }
                        else
                        {
                            GameManager.AddToGameLog($"Vous vous soignez de {this.maxHp - this.currentHp} points de vie.");
                            this.currentHp = this.maxHp;
                        }
                        
                        break;
                    case "Mana":

                        GameManager.AddToGameLog("Vous prenez la potion avec l'intention de la boire...");

                        if (this.currentMp + 50 < this.MaxMP)
                        {
                            GameManager.AddToGameLog($"Vous récupérez 50 points de mana.");
                            this.currentMp += 50;
                        }
                        else if(this.currentMp == this.maxMp)
                        {
                            GameManager.AddToGameLog("En prenant la potion, vous constatez que votre mental est à son apogée. Vous rangez la potion dans votre sac.");
                        }
                        else
                        {
                            GameManager.AddToGameLog($"Vous récupérez {this.maxMp - this.currentMp} points de mana.");
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
                        return this.attack + 10;
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
                        
                        return this.attack + 15;
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

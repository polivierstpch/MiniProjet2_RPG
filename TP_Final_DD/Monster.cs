using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Final_DD
{
    class Monster
    {
        private int id, attack, defense, maxHp, currentHp;
        private string name;
        private bool isDefending;

        //AXcceseur
        public int ID { get { return id; } }
        public string Name { get { return name; } }
        public int MaxHP { get { return maxHp; } }
        public int CurrentHP { get { return currentHp; } set { currentHp = value; } }
        public int Attack { get { return attack; } }
        public int Defense { get { return defense; } }
        public bool IsDefending { get { return isDefending; } set { isDefending = value; } }

        public Monster(int id, string name, int attack, int defense, int maxHp)
        {
            this.id = id;
            this.attack = attack;
            this.defense = defense;
            this.maxHp = maxHp;
            this.currentHp = maxHp;
            this.name = name;
        }
        public void TakeDamage(int damage)
        {
            if (isDefending == true)
            {
                damage = Math.Max(0, damage - this.defense);
         
            }

            // If damage goes over current HP, set health to 0
            this.currentHp = this.currentHp - damage >= 0 ? this.currentHp - damage : 0;
            if (this.currentHp == 0)
            {
                GameManager.AddToGameLog($"Le {this.name} est mort.");
            }

            if (damage > 0)
                GameManager.AddToGameLog($"Le {this.name} prend {damage} points de dégâts.");
            else
                GameManager.AddToGameLog($"Le {this.name} ne prend aucun dégâts.");

            
        }
    }
}

namespace TP_Final_DD
{
    class Equipment
    {
        private int id, modifier;
        private string itemType, name, classRequired;

        //AXcceseur
        public int ID { get { return id; } }
        public int Modifier { get { return modifier; } }
        public string ItemType { get { return itemType; } }
        public string Name { get { return name; } }
        public string ClassRequired { get { return classRequired; } }

        public Equipment(int id, string itemType, string name, string classRequired, int modifier)
        {
            this.id = id;
            this.modifier = modifier;
            this.itemType = itemType;
            this.name = name;
            this.classRequired = classRequired;
        }
    }
}

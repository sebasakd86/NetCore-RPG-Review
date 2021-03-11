namespace Net_RPG.Models
{
    //to create the many to many relation.
    public class CharacterSkill
    {
        public int CharacterId { get; set; }
        public Character Character { get; set; }
        public int SkillsId { get; set; }
        public Skills Skills { get; set; }


    }
}
namespace Net_RPG.DTOs.Fight
{
    public class AttackResultDTO
    {
        public string Attacker { get; set; }
        public string Enemy { get; set; }
        public int AttackerHP { get; set; }
        public int EnemyHp { get; set; }
        public int Damage { get; set; }
    }
}
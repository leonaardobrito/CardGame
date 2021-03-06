using System;
using System.Collections.Generic;
using System.Linq;
using Vue.Domain.Champions;

namespace Vue.Domain.Cards
{
    public class ArcherCard : Card
    {
        public override string Name => "Archer";
        public override Row Row => Row.Back;
        public override Row Targets => Row.Back;
        public override Rarity Rarity => Rarity.Uncommon;
        public override int Damage { get; set; } = 3;
        public override int Healing => 0;
        public override int Speed => 4;
        public override int MaxHealth => 6;
        public override string Description => "Attacks random card in back row";

        public override void ApplyMove(List<Card> enemyCards, List<Card> friendlyCards, Champion enemyChamp, List<GameAction> actions)
        {
            var cardsToAttack = TargetedCards(enemyCards);

            if (cardsToAttack.Any())
            {
                var rand = new Random();
                var index = rand.Next(cardsToAttack.Count);                
                var first = cardsToAttack[index];
                Attack(first);            
                actions.Add(new GameAction(this, new List<Character> { first }, null));
            }
            else
            {
                Attack(enemyChamp);
                actions.Add(new GameAction(this, new List<Character> { enemyChamp }, null));
            }
        }
    }
}
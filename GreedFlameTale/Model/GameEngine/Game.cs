using GreedFlameTale.Model.Attribute;
using System.Collections.Generic;

namespace GreedFlameTale.Model.GameEngine
{
    public enum Act { ATTACK, SPECIAL_ATTACK, REST }
    public enum Player { P1, P2 }

    public class GameAction
    {
        public Act Type { get; init; }
        public bool IsAvailable { get; init; }
    }

    class Game
    {

        public List<Character> PlayerTeam { get; private set; }
        public List<Character> EnemyTeam { get; private set; }
        private GameView MainObserver { get; init; }

        private EnemyAI Enemy { get; init; }

        public Game()
        {
            PlayerTeam = new List<Character>();
            EnemyTeam = new List<Character>();
        }

        private List<GameAction> getAvailableActions(Character target)
        {
            var result = new List<GameAction>();
            result.Add(new GameAction()
            {
                Type = Act.ATTACK,
                IsAvailable = target.Habilities.Attack.CanCast,
            });
            result.Add(new GameAction()
            {
                Type = Act.SPECIAL_ATTACK,
                IsAvailable = target.Habilities.SpecialAttack.CanCast,
            });
            result.Add(new GameAction()
            {
                Type = Act.REST,
                IsAvailable = true,
            });
            return result;
        }

        /// TODO: USE EVENTS TO TRACK WHICH ATTRIBUTE WAS MODIFIED
        private void ExecuteAttack(Player player, Character attacker)
        {
            var target = MainObserver.AskForTarget(player, EnemyTeam);
            attacker.Habilities.Attack.Apply(target);
            target.Habilities.AttackReaction.Apply(attacker);
            MainObserver.AttackFeedback(attacker, target);
        }

        /// TODO: USE EVENTS TO TRACK WHICH ATTRIBUTE WAS MODIFIED
        private void ExecuteSpecialAttack(Player player, Character attacker)
        {
            var target = MainObserver.AskForTarget(player, EnemyTeam);
            attacker.Habilities.SpecialAttack.Apply(target);
            target.Habilities.AttackReaction.Apply(attacker);
            MainObserver.AttackFeedback(attacker, target);
        }

        /// TODO: USE EVENTS TO TRACK WHICH ATTRIBUTE WAS MODIFIED
        private void OnTurnForAll(List<Character> team)
        {
            // team.ForEach(target => target.Habilities.AtTurn.Apply(target)); // Placing target, but no need for an argument
            team.ForEach(target =>
            {
                target.Habilities.AtTurn.Apply(target);
                MainObserver.TurnStartFeedback(target);
            });
        }

        private void Rest(Character target)
        {
            // target.Habilities.Rest.Apply(target); // Placing target, but no need for an argument
            EnemyTeam.ForEach(target =>
            {
                target.Habilities.Rest.Apply(target);
                var heal = target.Attributes.HealPoints.Value;
                var rest = target.Attributes.RestPoints.Value;
                MainObserver.RestFeedback(target.Name, heal, rest);
            });
        }

        public void Turn(Player player, List<Character> teamChars)
        {
            var team = new List<Character>(teamChars);
            OnTurnForAll(team);
            while (team.Count != 0)
            {
                var attacker = MainObserver.AskForPlayerSelect(player, team);
                team.Remove(attacker);
                var action = MainObserver.AskAction(player, getAvailableActions(attacker));
                switch (action.Type)
                {
                    case (Act.ATTACK):
                        ExecuteAttack(player, attacker);
                        break;
                    case (Act.SPECIAL_ATTACK):
                        ExecuteSpecialAttack(player, attacker);
                        break;
                    default:
                        Rest(attacker);
                        break;
                }
            }

        }


        public void Battle()
        {
            PlayerTeam = MainObserver.AskForPlayerSelect(Player.P1);
            EnemyTeam = MainObserver.AskForPlayerSelect(Player.P2);

            while (true)
            {
                if(PlayerTeam.TrueForAll(c => c.Attributes.IsDead))
                {
                    MainObserver.P2Won();
                    break;
                }
                else if (EnemyTeam.TrueForAll(c => c.Attributes.IsDead))
                {
                    MainObserver.P1Won();
                    break;
                }
                else
                {
                    Turn(Player.P1, PlayerTeam);
                    Turn(Player.P2, EnemyTeam);
                }
            }
        }

    }
}

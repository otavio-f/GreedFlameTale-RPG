using System.Collections.Generic;
using static GreedFlameTale.Model.GameAction;

namespace GreedFlameTale.Model
{
    public class GameAction
    {
        public enum ActionType { ATTACK, SPECIAL_ATTACK, REST }
        public ActionType Type { get; init; }
        public bool Available { get; init; }
    }

    class Game
    {

        public List<CharacterBase> PlayerTeam { get; init; }
        public List<CharacterBase> EnemyTeam { get; init; }
        private GameView MainObserver { get; init; }

        private AIEnemy Enemy { get; init; }

        public Game()
        {
            PlayerTeam = new List<CharacterBase>();
            EnemyTeam = new List<CharacterBase>();
        }

        private List<GameAction> getAvailableActions(CharacterBase target)
        {
            var result = new List<GameAction>();
            result.Add(new GameAction()
            {
                Type = ActionType.ATTACK,
                Available = target.Attributes.CanAttack,
            });
            result.Add(new GameAction()
            {
                Type = ActionType.SPECIAL_ATTACK,
                Available = target.Attributes.CanSpecialAttack,
            });
            result.Add(new GameAction()
            {
                Type = ActionType.REST,
                Available = true,
            });
            return result;
        }

        public void PlayerTurn()
        {
            var players = new List<CharacterBase>(PlayerTeam);
            while (players.Count != 0)
            {
                var attacker = MainObserver.AskForPlayerSelect(players);
                players.Remove(attacker);
                var action = MainObserver.AskPlayerAction(getAvailableActions(attacker));
                CharacterBase target;
                switch (action.Type)
                {
                    case (ActionType.ATTACK):
                        target = MainObserver.AskForPlayerTarget(EnemyTeam);
                        attacker.Attack(target);
                        attacker.ApplyCost();
                        target.GotAttacked(attacker);
                        break;
                    case (ActionType.SPECIAL_ATTACK):
                        target = MainObserver.AskForPlayerTarget(EnemyTeam);
                        attacker.SpecialAttack(target);
                        attacker.ApplySpecialCost();
                        target.GotAttacked(attacker);
                        break;
                    default:
                        attacker.Rest();
                        break;
                }
            }

        }


        public void EnemyTurn()
        {
            var players = new List<CharacterBase>(EnemyTeam);
            while (players.Count != 0)
            {
                var attacker = MainObserver.AskForPlayerSelect(players);
                players.Remove(attacker);
                var action = MainObserver.AskPlayerAction(getAvailableActions(attacker));
                CharacterBase target;
                switch (action.Type)
                {
                    case (ActionType.ATTACK):
                        target = MainObserver.AskForEnemyTarget(EnemyTeam);
                        attacker.Attack(target);
                        attacker.ApplyCost();
                        target.GotAttacked(attacker);
                        break;
                    case (ActionType.SPECIAL_ATTACK):
                        target = MainObserver.AskForEnemyTarget(EnemyTeam);
                        attacker.SpecialAttack(target);
                        attacker.ApplySpecialCost();
                        target.GotAttacked(attacker);
                        break;
                    default:
                        attacker.Rest();
                        break;
                }
            }

        }

        public void Battle()
        {
            while(true)
            {
            }
        }

    }
}

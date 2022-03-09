using System;
using System.Collections.Generic;

namespace GreedFlameTale.Model
{
    internal class GameView
    {
        public CharacterBase AskForPlayerSelect(List<CharacterBase> players)
        {
            throw new NotImplementedException();
        }

        public GameAction AskPlayerAction(List<GameAction> actions)
        {
            throw new NotImplementedException();
        }

        public CharacterBase AskForPlayerTarget(List<CharacterBase> enemyTeam)
        {
            throw new NotImplementedException();
        }

        public CharacterBase AskForEnemyTarget(List<CharacterBase> enemyTeam)
        {
            throw new NotImplementedException();
        }

        public CharacterBase AskForEnemySelect(List<CharacterBase> players)
        {
            throw new NotImplementedException();
        }

        public GameAction AskEnemyAction(List<GameAction> gameActions)
        {
            throw new NotImplementedException();
        }

    }
}
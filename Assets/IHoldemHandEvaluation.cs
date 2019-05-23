using System.Collections.Generic;

namespace Assets
{
    public interface IHoldemHandEvaluation
    {
        int EvaluateHand(LinkedList<Card> Hand, LinkedList<Card> BoardCards);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class VillainPlayer : Player
    {

        public VillainPlayer()
        {

        }
        public override void Bet()
        {
            throw new NotImplementedException();
        }

        public override void Check()
        {
            Debug.Log("Villain Checks");
        }

        public override void Fold()
        {
            throw new NotImplementedException();
        }

        public override void Raise()
        {
            throw new NotImplementedException();
        }
    }
}

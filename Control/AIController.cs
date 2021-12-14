using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Control
{
    public class AIController : Controller
    {
        public AIController(Player player, PlayGrid playGrid, GameState gameState) : base(player, playGrid, gameState)
        {
            ControllerInput = ControllerInput.AI;
        }

        public override void Activate()
        {
            int countTry = 0;

            while (countTry < 10)
            {
                bool isSucces = MakerandomDecision();
                
                if(isSucces)
                    break;

                countTry++;
            }
        }

        private bool MakerandomDecision()
        {
            int actionNumber = Random.Range(0, 5);

            bool isSuccesMove = false;

            switch (actionNumber)
            {
                case 0:
                    isSuccesMove = PlayerMoveUp();
                    break;
                case 1:
                    isSuccesMove = PlayerMoveDown();
                    break;
                case 2:
                    isSuccesMove = PlayerMoveLeft();
                    break;
                case 3:
                    isSuccesMove = PlayerMoveRight();
                    break;
                case 4:
                    int x = Random.Range(0, 8);

                    int y = Random.Range(0, 8);

                    isSuccesMove = PlayerSetObstacle(x, y);
                    break;
                default:
                    break;
            }

            return isSuccesMove;
        }
    }
}
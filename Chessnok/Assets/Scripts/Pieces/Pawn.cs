using Assets.Scripts.Pieces.Abilities.PieceTypeAbilities;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Pieces
{
    public class Pawn : Piece
    {


        public override void InstanciateAbilities()
        {
            PawnTypeAbility pawnTypeAbility = new PawnTypeAbility();
            pawnTypeAbility.Activate();
            pawnTypeAbility.SetEventControl(eventControl);
            pawnTypeAbility.SetCard(this);
            abilities.Add(pawnTypeAbility);
        }
    }
}
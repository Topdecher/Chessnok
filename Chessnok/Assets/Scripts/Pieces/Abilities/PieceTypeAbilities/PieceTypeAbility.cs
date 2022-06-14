using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Pieces.Abilities.PieceTypeAbilities
{
    public class PieceTypeAbility : Ability
    {
        protected System.Type realType;

        public System.Type GetRealType() => realType;
    }
}
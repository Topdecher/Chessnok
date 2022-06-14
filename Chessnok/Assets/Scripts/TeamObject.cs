using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class TeamObject : SolidObject
    { 
        protected int team;

        public int GetTeam() => team;
        public void SetTeam(int team) => this.team = team;
    }
}
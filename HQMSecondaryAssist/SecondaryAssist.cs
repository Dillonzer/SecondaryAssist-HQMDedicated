using HQMEditorDedicated;

namespace HQMSecondaryAssist
{
    class SecondaryAssist
    {
        const float BLUE_GOALLINE_Z = 4.15f;
        const float RED_GOALLINE_Z = 56.85f;
        const float LEFT_GOALPOST = 13.75f;
        const float RIGHT_GOALPOST = 16.25f;
        const float TOP_GOALPOST = 0.83f;

        string theHeroOfThePlay;
        HQMTeam lastTouchedPuck = HQMTeam.NoTeam;

        float currentPuckX, currentPuckY, currentPuckZ;
        bool puckTouched;

        public void checkForAssists()
        {
            currentPuckZ = Puck.Position.Z;
            currentPuckY = Puck.Position.Y;
            currentPuckX = Puck.Position.X;
            puckTouched = true;

            if (teamTouchedPuck() == HQMTeam.NoTeam)
               puckTouched = false;

            Player[] players = PlayerManager.Players;

            if (puckTouched)
                lastTouchedPuck = teamTouchedPuck();

            if(lastTouchedPuck == HQMTeam.Red)
            {

            }
            else if (lastTouchedPuck == HQMTeam.Blue)
            {

            }
        }

        public string blueTeamHero()
        {
            //TODO: Cycle through the players who last touched the puck
        }
        public string redTeamHero()
        {
            //TODO: Cycle through the players who last touched the puck
        }

        public bool blueGoalScored()
        {
            currentPuckY = Puck.Position.Y;
            currentPuckX = Puck.Position.X;
            int tempScore = GameInfo.BlueScore;

            if(puckOnNet(currentPuckX, currentPuckY))
            {
                return(tempScore > GameInfo.BlueScore);
            }

            return(false);
        }

        public bool redGoalScored()
        {
            currentPuckY = Puck.Position.Y;
            currentPuckX = Puck.Position.X;
            int tempScore = GameInfo.RedScore;

            if(puckOnNet(currentPuckX, currentPuckY))
            {
                return(tempScore > GameInfo.RedScore);
            }

            return(false);
        }
    
        /*
         * Taken from xparabolax icing mod
         */
        HQMTeam teamTouchedPuck()
        {
            foreach (Player p in PlayerManager.Players)
            {
                if ((p.StickPosition - Puck.Position).Magnitude < 0.25f)
                {
                    return p.Team;
                }
            }
            return HQMTeam.NoTeam;
        }

         bool puckOnNet(float x, float y)
        {
            return !(x < LEFT_GOALPOST || x > RIGHT_GOALPOST || y > TOP_GOALPOST);
        }
    }
}


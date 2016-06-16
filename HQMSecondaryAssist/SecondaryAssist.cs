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
        Player[] players = PlayerManager.Players;
        string[] assisters = new string[3];
        float currentPuckX, currentPuckY, currentPuckZ;
        int blueCount = 0;
        int redCount = 0;
        bool puckTouched;

        public void checkForAssists()
        {
            currentPuckZ = Puck.Position.Z;
            currentPuckY = Puck.Position.Y;
            currentPuckX = Puck.Position.X;
            puckTouched = true;

            if (teamTouchedPuck() == HQMTeam.NoTeam)
               puckTouched = false;

            if (puckTouched)
            {
                lastTouchedPuck = teamTouchedPuck();
            }

            if(lastTouchedPuck == HQMTeam.Red)
            {
                blueCount = 0; //resets the other teams counter, since they have no more possesion

                if (redCount == 4)
                    redCount = 0;

                assisters[redCount] = playerTouchedPuck();

                redCount++;

                if (redGoalScored())
                {
                    if (redCount == 4) //uses 4 then we know 3 red touched it last, which means there is a secondary assist.
                    {
                        theHeroOfThePlay = assisters[0];
                        //TODO: Add a function to add an assist to the player who got 3rd assist
                    }
                }
            }
            else if (lastTouchedPuck == HQMTeam.Blue)
            {
                redCount = 0; //resets the other teams counter, since they have no more possesion

                if (blueCount == 4)
                    blueCount = 0;

                assisters[blueCount] = playerTouchedPuck();

                blueCount++;

                if (blueGoalScored())
                {
                    if (blueCount == 4) //uses 4 then we know 3 red touched it last, which means there is a secondary assist.
                    {
                        theHeroOfThePlay = assisters[0];
                        //TODO: Add a function to add an assist to the player who got 3rd assist
                    }
                }
            }
        }

        /*IDEA
         * Track all players on blue team and red team
         * whenever one of them touch the puck, add them to an array
         * if the team switches, reset array
         */
        
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

        string playerTouchedPuck()
        {
            foreach (Player p in PlayerManager.Players)
            {
                if ((p.StickPosition - Puck.Position).Magnitude < 0.25f)
                {
                    return p.Name;
                }
            }

            return "";
        }
    
        /*
         * purpose: returns which colour team touched the puck last
         * taken from xParabolax's Icing Mod on github. (link in readme)
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

        /*
         * purpose: returns if the puck is on net
         * taken from xParabolax's Icing Mod on github. (link in readme)
         */
         bool puckOnNet(float x, float y)
        {
            return !(x < LEFT_GOALPOST || x > RIGHT_GOALPOST || y > TOP_GOALPOST);
        }
    }
}


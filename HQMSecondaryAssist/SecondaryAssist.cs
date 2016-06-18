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
        string[] assisters = new string[4];
        string tempAssis = "";
        string swap = "";
        float currentPuckX, currentPuckY, currentPuckZ;
        int blueCount = 0;
        int redCount = 0;
        bool puckTouched;
        int blueScore = GameInfo.BlueScore;
        int redScore = GameInfo.RedScore;

        public void checkForAssists()
        {
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

                tempAssis = playerTouchedPuck();


                if (tempAssis != "")
                {
                    if (redCount == 0)
                    {
                        assisters[redCount] = tempAssis;
                        redCount++;
                    }
                    else if (assisters[redCount - 1] != tempAssis)
                    {
                        if (redCount == 2 && assisters[redCount - 2] != tempAssis)
                        {
                            assisters[redCount] = tempAssis;
                            redCount++;
                        }
                    }
                }

                if (GameInfo.AfterGoalFaceoffTime != 0)
                {
                    if (redCount == 3)
                    {
                        theHeroOfThePlay = assisters[0];
                        Chat.SendMessage("THIRD ASSIST: " + theHeroOfThePlay);
                        redCount = 0;
                        //TODO: Add a function to add an assist to the player who got 3rd assist. Currently only GET available in HockeyDedicated

                    }
                }
            }
            else if (lastTouchedPuck == HQMTeam.Blue)
            {
                redCount = 0; //resets the other teams counter, since they have no more possesion

                if (blueCount == 4)
                    blueCount = 0;

                tempAssis = playerTouchedPuck();


                if (tempAssis != "")
                {
                    if (blueCount == 0)
                    {
                        assisters[blueCount] = tempAssis;
                        blueCount++;
                    }
                    else if (assisters[blueCount - 1] != tempAssis)
                    {
                        if (blueCount == 2 && assisters[blueCount - 2] != tempAssis)
                        {
                            assisters[blueCount] = tempAssis;
                            blueCount++;
                        }
                        else if (blueCount != 2)
                        {
                            assisters[blueCount] = tempAssis;
                            blueCount++;
                        }
                        
                    }
                }

                System.Console.WriteLine(blueCount);

                if (GameInfo.AfterGoalFaceoffTime != 0)
                {
                    if (blueCount == 3) 
                    {
                        theHeroOfThePlay = assisters[0];
                        Chat.SendMessage("THIRD ASSIST: " + theHeroOfThePlay);
                        blueCount = 0;
                        //TODO: Add a function to add an assist to the player who got 3rd assist. Currently only GET available in HockeyDedicated

                    }
                }
            }
        }

        /*IDEA
         * Track all players on blue team and red team
         * whenever one of them touch the puck, add them to an array
         * if the team switches, reset array
         */

        public bool blueGoalScored(int tempScore)
        {
            return (tempScore > GameInfo.BlueScore);
        }

        public bool redGoalScored(int tempScore)
        {
            return(tempScore > GameInfo.RedScore);
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

       

         
    }
}


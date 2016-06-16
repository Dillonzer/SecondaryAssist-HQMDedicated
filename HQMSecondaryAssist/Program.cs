using HQMEditorDedicated;

namespace HQMSecondaryAssist
{
    class Program
    {
        static void Main(string[] args)
        {
            MemoryEditor.Init();
            
            while (true) 
            {
                //Stole this from xParabolaX's icing MOD
                if (GameInfo.Period > 0 && GameInfo.IntermissionTime == 0 &&
                    (GameInfo.AfterGoalFaceoffTime == 0 || GameInfo.AfterGoalFaceoffTime >= 649))
                {
                    
                }

            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EdgeSim.Rules.Enum;

namespace EdgeSim.Rules.Core
{
    public static class CheckRoll
    {
        private static Random _random = new Random();

        public static byte DiceRoll(CheckDice pDice)
        {
            byte lMax = 0;

            switch (pDice)
            {
                case CheckDice.D4: lMax = 4; break;
                case CheckDice.D6: lMax =  6; break;
                case CheckDice.D8: lMax =  8; break;
                case CheckDice.D10: lMax = 10; break;
                case CheckDice.D12: lMax = 12; break;
                case CheckDice.D20: lMax = 20; break;
                default: return 0;
            }

            return (byte)_random.Next(1, lMax + 1);
        }

        public static CheckDice SkillDiceType(Byte pSkill)
        {
            if (pSkill == 0)
                return CheckDice.D4;
            else if (pSkill < 6)
                return CheckDice.D6;
            else if (pSkill < 11)
                return CheckDice.D8;
            else if (pSkill < 16)
                return CheckDice.D10;
            else
                return CheckDice.D12;
        }

        public static Byte SkillDiceCount(Byte pSkill)
        {
            if (pSkill == 0)
                return 4;
            else if (pSkill < 6)
                return 5;
            else if (pSkill < 11)
                return 6;
            else if (pSkill < 16)
                return 7;
            else
                return 8;
        }

        public static CheckResult ActionCheck(Byte pAttribute, Byte pSkill, Byte pCaution, Byte pTarget, Byte pRounds)
        {
            if ((pAttribute < 1) || (pAttribute > 10))
                throw new Exception("Attribute must be 1 to 10");

            if ((pSkill > 20))
                throw new Exception("Skill must be 0 to 20");

            Byte lTarget = pTarget;
            Byte lTotalDice = SkillDiceCount(pSkill);

            CheckDice lDice = SkillDiceType(pSkill);

            for (int lRound = 0; lRound < pRounds; lRound++)
            {
                Byte lSuccesses = 0;
                Byte lFails = 0;
                ////
                Byte lRndSaveDice = (byte)Math.Min(pCaution, lTotalDice - 1); //(byte)Math.Round((lTotalDice * pCaution / 5.0), MidpointRounding.AwayFromZero);
                Byte lRndRollDice = (byte)(lTotalDice - lRndSaveDice);

                for (byte i = 0; i < lRndRollDice; i++)
                {
                    Byte pRoll = DiceRoll(lDice);

                    if ((pRoll != 1) && (pRoll + pAttribute >= lTarget))
                    {
                        if (lFails > 0)
                            lFails--;
                        else
                            lSuccesses++;
                    }
                    else
                    {
                        if (lSuccesses > 0)
                            lSuccesses--;
                        else
                            lFails++;
                    }
                }

                if (lFails > lRndSaveDice)
                    return CheckResult.Fail;
                else if (lFails > 0)
                    lTotalDice -= lFails;

                if (lSuccesses > lTarget)
                    return CheckResult.Success;
                else if (lSuccesses > 0)
                    lTarget -= lSuccesses;
            }

            if (lTarget < pTarget)
                return CheckResult.Partial;
            else
                return CheckResult.NoResult;
        }

        /*public static byte DiceRoll(CheckDice pDice)
        {
            byte lMax = 0;

            switch (pDice)
            {
                case CheckDice.D4: lMax = 4; break;
                case CheckDice.D6: lMax =  6; break;
                case CheckDice.D8: lMax =  8; break;
                case CheckDice.D10: lMax = 10; break;
                case CheckDice.D12: lMax = 12; break;
                case CheckDice.D20: lMax = 20; break;
                default: return 0;
            }

            return (byte)_random.Next(1, lMax + 1);
        }

        public static CheckDice SkillDiceType(Byte pTraining)
        {
            switch (pTraining / 5)
            {
                case 0: return CheckDice.D4;
                case 1: return CheckDice.D6;
                case 2: return CheckDice.D8;
                case 3: return CheckDice.D10;
                case 4: return CheckDice.D12;
                default: return CheckDice.D20;
            }
        }

        public static CheckResult ActionCheck(Byte pAttribute, Byte pTraining, Byte pCaution, Byte pTarget, Byte pRounds)
        {
            if (pAttribute < 1)
                throw new Exception("Attribute must be > 0");

            Byte lTarget = pTarget;
            Byte lTotalDice = pAttribute;

            CheckDice lDice = SkillDiceType(pTraining);
            

            for (int lRound = 0; lRound < pRounds; lRound++)
            {
                Byte lSuccesses = 0;
                Byte lFails = 0;

                Byte lRndSaveDice = (byte)Math.Round((lTotalDice * pCaution / 5.0), MidpointRounding.AwayFromZero);
                Byte lRndRollDice = (byte)(lTotalDice - lRndSaveDice);

                for (byte i = 0; i < lRndRollDice; i++)
                {
                    Byte pRoll = DiceRoll(lDice);

                    if ((pRoll != 1) && (pRoll >= Math.Max(lTarget - pTraining, 0)))
                    {
                        if (lFails > 0)
                            lFails--;
                        else
                            lSuccesses++;
                    }
                    else
                    {
                        if (lSuccesses > 0)
                            lSuccesses--;
                        else
                            lFails++;
                    }
                }

                if (lFails > lRndSaveDice)
                    return CheckResult.Fail;
                else if (lFails > 0)
                    lTotalDice -= lFails;
                else if (lSuccesses > lTarget)
                    return CheckResult.Success;
                else if (lSuccesses > 0)
                    lTarget -= lSuccesses;
            }

            if (lTarget < pTarget)
                return CheckResult.Partial;
            else
                return CheckResult.NoResult;
        }*/
    }
}

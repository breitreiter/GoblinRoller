using GoblinRoller.Bots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoblinRoller
{
    internal class GameTurn
    {
        internal int GoldAtStake = 0;
        internal List<BotBase> Players = new();
        internal BotBase? Winner = null;

        private d6 _d6 = new d6();

        internal void ResolveTurn()
        {
            GoldAtStake = _d6.Roll();

            Console.WriteLine("🪙Gold is " + GoldAtStake);

            foreach (BotBase bot in Players)
                bot.PlanTurn(this);

            // Mining
            int bigMiner = 0;
            foreach (BotBase bot in Players)
            {
                if (bot.MyHand.MinePower > bigMiner)
                    bigMiner = bot.MyHand.MinePower;
            }

            List<BotBase> topMiners = new();
            if (bigMiner > 0)
                foreach (BotBase bot in Players)
                    if (bot.MyHand.MinePower == bigMiner)
                        topMiners.Add(bot);

            if (topMiners.Count == 1)
            {
                Console.WriteLine(topMiners[0].Name + " mines " + GoldAtStake);
                topMiners[0].Gold += GoldAtStake;
            }
            else if (topMiners.Count > 0)
            {
                foreach (BotBase bot in topMiners)
                {
                    Console.WriteLine(bot.Name + " mines 2");
                    bot.Gold += 2;
                }
            }

            // Stealing
            int bigStealy = 0;
            int bigVictim = int.MaxValue;
            foreach(BotBase bot in Players)
            {
                if (bot.MyHand.StealPower > bigStealy)
                    bigStealy = bot.MyHand.StealPower;
                if (bot.MyHand.DefensePower < bigVictim)
                    bigVictim = bot.MyHand.DefensePower;
            }

            List<BotBase> stealers = new();
            if (bigStealy > 0)
                foreach (BotBase bot in Players)
                    if (bot.MyHand.StealPower == bigStealy)
                        stealers.Add(bot);

            List<BotBase> victims = new();
            foreach (BotBase bot in Players)
                if (bot.MyHand.DefensePower == bigVictim)
                    victims.Add(bot);

            int goldPot = 0;
            if (victims.Count == 1)
            {
                goldPot = victims[0].Gold / 2 + victims[0].Gold % 2;
                Console.WriteLine(victims[0].Name + " loses " + goldPot + " to theft!");                
                victims[0].Gold -= goldPot;
            }
            else
            {
                foreach (BotBase bot in victims)
                {
                    if (bot.Gold >= 2)
                    {
                        Console.WriteLine(bot.Name + " loses 2 to theft!");
                        bot.Gold -= 2;
                        goldPot += 2;
                    }
                    else
                    {
                        Console.WriteLine(bot.Name + " loses " + bot.Gold + " to theft!");
                        goldPot += bot.Gold;
                        bot.Gold = 0;
                    }
                }
            }

            Console.WriteLine("Spoils " + goldPot);

            if (stealers.Count == 1)
            {
                Console.WriteLine(" " + stealers[0].Name + " steals " + goldPot);
                stealers[0].Gold += goldPot;
            }
            else if (stealers.Count > 0)
            {
                foreach (BotBase bot in stealers)
                {
                    int stealAmount = goldPot / stealers.Count;
                    Console.WriteLine(" " + bot.Name + " steals " + stealAmount);
                    bot.Gold += stealAmount;
                }
            }

            // Win check
            foreach(BotBase bot in Players)
                if (bot.Gold >= 10)
                {
                    int bigMoney = bot.Gold;
                    int bigMoneyHaverMans = 0;
                    BotBase winnerMan = bot;

                    foreach(BotBase winnerMaybe in Players)
                    {
                        if (winnerMaybe.Gold == bigMoney)
                            bigMoneyHaverMans++;
                        else if (winnerMaybe.Gold > bigMoney)
                        {
                            bigMoney = winnerMaybe.Gold;
                            bigMoneyHaverMans = 1;
                            winnerMan = bot;
                        }
                    }

                    if (bigMoneyHaverMans == 1)
                    {
                        this.Winner = winnerMan;
                        return;
                    }

                }

            foreach(BotBase bot in Players)
            {
                Console.WriteLine(bot.Name + " hand:" + bot.MyHand.ToString());
                bot.MyHand.Discard();
                bot.MyHand.DrawUp();
            }
        }

    }
}

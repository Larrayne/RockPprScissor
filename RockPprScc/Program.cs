using System;

namespace ROCKPAPERSCISSORS
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputPlayer, inputCPU = "";
            int randomInt;
            
            bool PlayAgain = true;

            while (PlayAgain){

                int scorePlayer = 0;
                int scoreCPU = 0;

            while (scorePlayer < 3 && scoreCPU < 3 )
            {
                
            

            Console.WriteLine("Choose between ROCK, PAPER or SCISSORS: ");
            inputPlayer = Console.ReadLine()?.ToUpper();

            if (inputPlayer == null)
            {
                Console.WriteLine("Invalid input. Please enter ROCK, PAPER, or SCISSORS.");
                return;
            }

            Random rand = new Random();
            randomInt = rand.Next(1, 4);

            switch (randomInt)
            {
                case 1:
                    inputCPU = "ROCK";
                    Console.WriteLine("Computer chose ROCK");
                    if (inputPlayer == "ROCK")
                    {
                        Console.WriteLine("DRAW!! \n\n");
                    }
                    else if (inputPlayer == "PAPER")
                    {
                        Console.WriteLine("PLAYER WINS\n\n");
                        scorePlayer++;
                    }
                    else if (inputPlayer == "SCISSORS")
                    {
                        Console.WriteLine("Computer wins!!\n\n");
                        scoreCPU++;
                    }
                    break;
                case 2:
                    inputCPU = "PAPER";
                    Console.WriteLine("Computer chose PAPER");
                    if (inputPlayer == "ROCK")
                    {
                        Console.WriteLine("Computer wins!! \n\n");
                        scoreCPU++;
                    }
                    else if (inputPlayer == "PAPER")
                    {
                        Console.WriteLine("DRAW\n\n");
                    }
                    else if (inputPlayer == "SCISSORS")
                    {
                        Console.WriteLine("Player wins!!\n\n");
                        scorePlayer++;
                    }
                    break;
                case 3:
                    inputCPU = "SCISSORS";
                    Console.WriteLine("Computer chose SCISSORS");
                    if (inputPlayer == "ROCK")
                    {
                        Console.WriteLine("Player wins!! \n\n");
                        scorePlayer++;
                    }
                    else if (inputPlayer == "PAPER")
                    {
                        Console.WriteLine("Computer wins\n\n");
                        scoreCPU++;
                    }
                    else if (inputPlayer == "SCISSORS")
                    {
                        Console.WriteLine("DRAW!!\n\n");
                    }
                    break;
                default:
                    break;
            }
            Console.WriteLine("\n\nScores:\tPlayer:\t{0}\tCPU:\t{1}", scorePlayer, scoreCPU);
        }

        if(scorePlayer == 3){
            Console.WriteLine("Player wins!!");
        }
        else if (scoreCPU == 3){
        Console.WriteLine("CPU wins!!");
        }else {

        }
        Console.WriteLine("Doyou want to play again?(y/n)");
        string loop = Console.ReadLine()?.ToLower() ?? "n";
        if(loop == "y"){
            PlayAgain = true;
            Console.Clear();
        }
        else if (loop == "n"){
            PlayAgain = false;
        }
        else{

        }
        }

        }


    }
}

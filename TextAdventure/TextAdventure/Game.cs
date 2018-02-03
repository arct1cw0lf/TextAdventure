using System;
using System.Collections.Generic;
using System.Threading;

namespace TextAdventure {
	
	public class Game {

		public Game () {
		}

        List<string> Inventory = new List<string>();
		public string input;
		public string[] prompts = {"You are in a small room. You look around, and see that there is a staircase to your right, and a door to your left." +
			"\n" + "Which do you enter?" + "\n" + "1) Right 2) Left","You walk up the staircase and get tired about 50 steps up. You stop to take a break, and notice a candle." + 
			"\n" + "You grab it and keep on climbing until you reach the 100th step, and you see a chest.","You open the door and see a monster!",
			"Do you attack or flee?" + "\n" + "1) attack 2) flee", "The Monster overpowers you and you perish. The end.",
			"You escape the monster and climb up the stairs in a hurry, until you notice a chest.","Please enter attack or flee","Please enter right or left", 
			"The chest glows, and you feel drawn to it. But you need a key first." 
			+"\n"+ "Do you go searching for the key or keep moving?" +"\n"+"1)search 2)move",
			"You search and find a trap door. It is also locked. You then open another door. There is a monster!", 
			"Enter any key to quit..." };


        public void StartGame(){
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine ("___________              __       _____       .___                    __                        \n\\__    ___/___ ___  ____/  |_    /  _  \\    __| _/__  __ ____   _____/  |_ __ _________   ____  \n  |    |_/ __ \\\\  \\/  /\\   __\\  /  /_\\  \\  / __ |\\  \\/ // __ \\ /    \\   __\\  |  \\_  __ \\_/ __ \\ \n  |    |\\  ___/ >    <  |  |   /    |    \\/ /_/ | \\   /\\  ___/|   |  \\  | |  |  /|  | \\/\\  ___/ \n  |____| \\___  >__/\\_ \\ |__|   \\____|__  /\\____ |  \\_/  \\___  >___|  /__| |____/ |__|    \\___  >\n             \\/      \\/                \\/      \\/           \\/     \\/                        \\/ ");
			Console.WriteLine ("Welcome to the castle. You must escape before you die.");
			NamePlayer ();

		}


		//name player prompt
		void NamePlayer(){
			string playerName;
			A ("What is your name noble one?");
			playerName = Console.ReadLine();
			A ("Hello noble "+playerName +". Let's get started.");

		}
			

		public void Choice(){


            
            int scenarios = 4;


		


			//0 is right or left
			//1 is unfinished right, ending at you see a chest
			//2 is left (then add enemy dialogue), and 3 is attack or flee
			//4 is if attack monster, you lose
			//5 is if flee, and you meet back with the chest one
			//6 is please enter attack or flee
			//7 is please enter right or left
			//8 is search or move on
			//9 is trap door and monster
			//10 Enter any key to quit
            

			for (int scenario = 1; scenario <= scenarios; scenario++) {
				
				switch (scenario) {

				case 1:
					
					A (prompts [0]);

				
					mistype:
					input = Console.ReadLine ();
					input = input.ToLower ();

					if (input == "right" || input == "1") {
						A (prompts [1]);
						Inventory.Add ("candle");
						printInventory ();

					} else if (input == "left" || input == "2") {
						A (prompts [2]);
						EnemyDialogue ("Stop right there! You shall perish under my might!");
						A (prompts [3]);

						mistype1:

						input = Console.ReadLine ();
						input = input.ToLower ();

						if (input == "attack" || input =="1") {
							A (prompts [4]);
							Death ();



						} else if (input == "flee" || input == "2") {
							
							A (prompts [5]);



						}  else {
							A (prompts [6]);
							goto mistype1;
						}

							
					}

					//to get to case 4 instantly, for testing
					else if (input == "3") {

						A ("Bypassing...");
						goto case 4;


					}

					else {
						A (prompts [7]);
						goto mistype;
					}

					break;
			//end of case 1
				case 2:
					A (" ");
					A (prompts [8]);
						
					mistype2:
                        
                    input = Console.ReadLine ();
					input = input.ToLower ();

					if (input == "search" || input == "1") {
						A (prompts[9]);
                            EnemyDialogue("BAHAHA, You are no match for me! I am a water monster and only a flame can destroy me!");
                            printInventory();
                            A(prompts[3]);
                       mistype3:
                            input = Console.ReadLine();
                            input = input.ToLower();

						if ((input == "attack" || input == "1") && (Inventory.Contains ("candle"))) {
							A ("You use your candle to defeat the monster! You collect a sword, armor, and a water potion.");
                                
							Inventory.Add ("sword");
							Inventory.Add ("armor");
							Inventory.Add ("water potion");
							Inventory.Remove ("candle");
							printInventory ();

							goto case 3;

						} else if ((input == "attack" || input == "1") && !(Inventory.Contains ("candle"))) { //DEATH
							
							A ("You do not have a candle, so the monster kills you!");
							Death ();

						} else if (input == "flee" || input == "2") {
							goto case 3;
						}
                            else
                            {
                                A(prompts[6]);
                                goto mistype3;
                            }
                          

					} else if (input == "move" || input == "2") {
						A ("You chose move");
						goto case 3;
			
					} else {
						A ("Please enter search or move");
						goto mistype2;
					}


					break;
			//end of case 2
				case 3:

					A("You keep moving, and trip on something shiny. Its a golden key! " +
						"You can now open the chest or the trap door. " + "\n" +
						"Which do you choose?");
					A ("1) chest 2) trap door");
					Inventory.Add ("key");
					mistype5:
					input = Console.ReadLine();
					input = input.ToLower();

					if (input == "chest" || input == "1") {
						A ("Upon opening the chest, you see mass amounts of gold! You also grab a map!");
						Inventory.Add ("Lots o' gold");
						Inventory.Add ("map");
						printInventory();
						//continue here
						goto case 4;
					


					} else if (input == "trap door" || input == "2") {
						A ("A massive dragon flies out!");
						EnemyDialogue ("Puny one, you shall be engulfed in my flames!");
						A ("Only a water potion can save you!");
						A ("");
						A ("");
						if (Inventory.Contains ("water potion")) {
							A ("You are protected from the flames, but you still need a sword!");
							Inventory.Remove ("water potion");
							A ("");
							if (Inventory.Contains ("sword")) {
								A ("You slay the dragon! You grabbed a fire potion and a map!");
								Inventory.Add ("fire potion");
								Inventory.Add ("map");
								printInventory ();
								goto case 4;
							


							} else {
								A ("You have nothing to defend yourself with! You die...");
								Death ();
							}
						} else {
							A ("You have nothing to defend yourself with! You die...");
							Death ();
						}

					} else {
						A ("Please enter chest or trap door");
						goto mistype5;
					}



					break;


				case 4:
					A ("");
					A ("");

					if (Inventory.Contains("map")){
						A ("You look at your map, and discover the exit out of the castle!!" + "\n"
						+ "You run towards the exit and see an armed prison guard!");
						A ("He stops you, and threatens you! Do you bribe him or use force!");
						A ("1)bribe 2)attack");

						mistype6:
						input = Console.ReadLine();
						input = input.ToLower();

						if (input == "bribe" || input == "1") {

							if (Inventory.Contains ("Lots o' gold")) {
								A ("The guard takes the money and opens the door out of the castle");
								A ("Bright light hits your eyes, you are free!!!");
								A ("Congratulations on making it out of the castle alive!");
								//win
								Death ();
							} else {
								A ("The guard does not accept your bribe of nothing, he has a family to support." + "\n" +
								"He kills you with a spear");
								Death ();

							}

						} else if (input == "attack" || input == "2") {
							if (Inventory.Contains ("sword") || Inventory.Contains ("fire potion") || Inventory.Contains ("water potion")) {
								A ("You use your items to overpower the prison guard, and steal his key.");
								A ("Bright light hits your eyes, you are free!!!");
								A ("Congratulations on making it out of the castle alive!");
								//win
								Death ();
							} else {
								A ("The prison guard stabs you with his spear! You die!");
								Death ();
							}

						} else {
							A ("Please enter bribe or attack");
							goto mistype6;
						}



					} else{
						A ("You keep looking around the castle hopelessly. You are totally lost and have no way out." +
							"\n"+"You die of starvation.");
						Death ();
					}



					break;
				
				default:
					
					break;
				}//end switch scenario
			}//end scenario loop
		}//end Choice method



		void Dialogue(string message){
			Console.ForegroundColor = ConsoleColor.Cyan;
			A (message);
			Console.ForegroundColor = ConsoleColor.Green;
		}//end Dialogue

		void EnemyDialogue(string message){
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine (message);
			Console.ForegroundColor = ConsoleColor.Green;
		}//end EnemyDialogue

        void printInventory() {
            Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("Inventory:");
            Console.ForegroundColor = ConsoleColor.Cyan;
			Inventory.ForEach(Console.WriteLine);
            Console.ForegroundColor = ConsoleColor.Green;
        } //end printInventory

		void Death(){
			A (prompts [10]);
			input = Console.ReadLine ();
			input = input.ToLower ();
			if (input == "q") {
				Environment.Exit (0);
			} else {
				Environment.Exit (0);
			}
		}//end death

		public void A(string message){
			Console.ForegroundColor = ConsoleColor.Magenta;
			foreach (Char c in message)
			{
				Console.Write(c);
				Thread.Sleep(15);
			}
			Console.WriteLine ("");
			Console.ForegroundColor = ConsoleColor.Green;
		}
        

    }
}
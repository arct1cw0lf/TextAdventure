using System;

namespace TextAdventure {
	
	class MainClass {
		
		public static void Main (string[] args) {
			Game mainGame = new Game ();
			mainGame.StartGame ();
			mainGame.Choice ();
			Console.Read ();
		}
	}
}

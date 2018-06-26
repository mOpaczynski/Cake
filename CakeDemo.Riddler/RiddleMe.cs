namespace CakeDemo.Riddler
{
    public class RiddleMe
    {
        public string WhatIsLove(string answer)
        {
            if (answer == "Baby, don't hurn me")
            {
                return "Don't hurt me, no more";
            }

            return "You know nothing, Jon Snow";
        }

        public string WhatIsTheAnswerToLifeUniverseAndEverything(string answer)
        {
            if (answer == "42")
            {
                return "Indeed";
            }

            return "It is too complicated";
        }

        public string Why(string answer)
        {
            switch (answer)
            {
                case "For fun":
                    return "Yolo";
                case "For honor":
                    return "M'lady";
                case "For money":
                    return "Get riches";
                default:
                    return "Nope, guess again";
            }
        }
    }
}

using System;
// You didn't ask for extras but I made them anyway :P
// You can Add replies to videos, it has a likes and dislikes counter, length converts to minutes and seconds in the Print method, handles videos with lengths under 60 seconds.
class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();
        Video video1 = new Video("Hello World!", "This is my first video on YouTube", 39, "John Doe");
        Video video2 = new Video("The in's and outs of molecular degregadation in necrotic tissue", "ZOMBIE DEATH!", 1290, "Mr. Z");
        Video video3 = new Video("All I want for christmas is World Peace", "A Desperate attempt to bring people together.", 631, "Beastly_Kindness");
        Video video4 = new Video("Jumping off the roof before Mom gets home", "I jump off my roof into a trashcan in the driveway.", 239, "KAStunts");

        video1.AddComment("ldsBoi12", "Wow! This looks really good, keep going!");
        video1.AddComment("bravoBaby39", "Pssh, Keep trying Fanboy.");
        video1.AddReply("Andy458", "bravoBaby39", "Grow up, He's trying his best and this isn't bad.");
        video1.AddReply("bravoBaby39", "bravoBaby39", "Like I care, you're what's wrong with this country; always defending bad ideas.");

        video2.AddComment("HamsterBoiz", "Where the girls at!?");
        video2.AddComment("DrManhatten34", "I came here to see real science, not someones lecture on their fruitless imagination.");
        video2.AddComment("JohnnyBravo_123", "First!");
        video2.AddReply("AllGoodThings978", "DrManhatten34", "This is revolutionary in modern science. After discerning the proper purpose of these microbes think of the revelations that could be uncovered with a deeper study. If they can be harrnessed to be attracted towards living tissue the discoverer could unleash a biological terror matching a devestation akin to Chernobyl; the difference being that it leaves behind furtile and rich unspoiled riches to those who can seize it.");
        video2.AddReply("unwelcomeRealist", "DrManhatten34", "Dude... You sound crazy. Who hurt you?");

        video3.AddComment("ConservativeDemocrat", "If only it were so simple.");
        video3.AddComment("PeaceBoys", "I hear you.");
        video3.AddComment("MedowMoon78", "My brother was fired because he spoke out about how unfair the business was. It's only a handful of people that are the problem.");
        video3.AddReply("LoginBonus", "MedowMoon78", "What are you implying?");
        video3.AddReply("MedowMoon78", "MedowMoon78", "I'm just telling you the truth, so many people want to generalize people but it's really only a small group that should just leave and we would all be happier for it.");

        video4.AddComment("RealMoms", "You're an idiot");
        video4.AddComment("KnoksvilleFansUnite", "HECK YEAH! Totally Nailed it!");
        video4.AddComment("karateKidz45", "That was Sick! Can't believe you made it.");
        video4.AddComment("Fire_AirBenders", "Pssh, this is obviously edited, you can see the frame skip from the two shots at 1:24");
        video4.AddReply("loliCatsPlus", "Fire_AirBenders", "You must be so much fun at parties.");

        videos.Add(video1);
        videos.Add(video2);
        videos.Add(video3);
        videos.Add(video4);

        foreach (Video video in videos)
        {
            Console.WriteLine("------------------------------------------");
            video.PrintVideoInfo();
            Console.WriteLine("------------------------------------------");
        }
    }
}
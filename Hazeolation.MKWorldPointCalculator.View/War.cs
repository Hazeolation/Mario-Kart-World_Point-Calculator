using System.Collections.Generic;

namespace Hazeolation.MKWorldPointCalculator.View;

/// <summary>
/// Represents a Mario Kart World match, often known as a war.
/// </summary>
internal class War
{
    /// <summary>
    /// A list of all races that occurred during this war.
    /// </summary>
    private readonly List<Race> races;

    /// <summary>
    /// The home team's name.
    /// </summary>
    private readonly string home;

    /// <summary>
    /// The guest team's name.
    /// </summary>
    private readonly string guest;

    /// <summary>
    /// The name of team 3.
    /// </summary>
    private readonly string team3;

    /// <summary>
    /// The name of team 4.
    /// </summary>
    private readonly string team4;

    /// <summary>
    /// The name of team 5.
    /// </summary>
    private readonly string team5;

    /// <summary>
    /// The name of team 6.
    /// </summary>
    private readonly string team6;

    /// <summary>
    /// The name of team 7.
    /// </summary>
    private readonly string team7;

    /// <summary>
    /// The name of team 8.
    /// </summary>
    private readonly string team8;

    /// <summary>
    /// The name of team 9.
    /// </summary>
    private readonly string team9;

    /// <summary>
    /// The name of team 10.
    /// </summary>
    private readonly string team10;

    /// <summary>
    /// The name of team 11.
    /// </summary>
    private readonly string team11;

    /// <summary>
    /// The name of team 12.
    /// </summary>
    private readonly string team12;

    /// <summary>
    /// The format of the war.
    /// </summary>
    private readonly string format;

    /// <summary>
    /// The amount of players in the war.
    /// </summary>
    private readonly string players;

    /// <summary>
    /// Gets the name of the home team.
    /// </summary>
    public string Home { get => home; }

    /// <summary>
    /// Gets the name of the guest team.
    /// </summary>
    public string Guest { get => guest; }

    /// <summary>
    /// Gets the name of team 3.
    /// </summary>
    public string Team3 { get => team3; }

    /// <summary>
    /// Gets the name of team 4.
    /// </summary>
    public string Team4 { get => team4; }

    /// <summary>
    /// Gets the name of team 5.
    /// </summary>
    public string Team5 { get => team5; }

    /// <summary>
    /// Gets the name of team 6.
    /// </summary>
    public string Team6 { get => team6; }

    /// <summary>
    /// Gets the name of team 7.
    /// </summary>
    public string Team7 { get => team7; }

    /// <summary>
    /// Gets the name of team 8.
    /// </summary>
    public string Team8 { get => team8; }

    /// <summary>
    /// Gets the name of team 9.
    /// </summary>
    public string Team9 { get => team9; }

    /// <summary>
    /// Gets the name of team 10.
    /// </summary>
    public string Team10 { get => team10; }

    /// <summary>
    /// Gets the name of team 11.
    /// </summary>
    public string Team11 { get => team11; }

    /// <summary>
    /// Gets the name of team 12.
    /// </summary>
    public string Team12 { get => team12; }

    /// <summary>
    /// Initializes a new instance of the <see cref="War"/> class. <br />
    /// This constructor is to be used for 12 player 6v6 or 24 player 12v12 wars.
    /// </summary>
    /// <param name="home">The home team name</param>
    /// <param name="guest">The guest team name</param>
    /// <param name="format">The war format</param>
    /// <param name="players">The war player count</param>
    public War(string home, string guest, string format, string players)
    {
        races = [];
        this.home = home;
        this.guest = guest;
        this.format = format;
        this.players = players;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="War"/> class. <br />
    /// This constructor is to be used for 12 player 4v4... wars.
    /// </summary>
    /// <param name="home">The home team name</param>
    /// <param name="guest">The guest team name</param>
    /// <param name="team3">The name of team 3</param>
    /// <param name="format">The war format</param>
    /// <param name="players">The war player count</param>
    public War(string home, string guest, string team3, string format, string players)
    {
        races = [];
        this.home = home;
        this.guest = guest;
        this.team3 = team3;
        this.format = format;
        this.players = players;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="War"/> class. <br />
    /// This constructor is to be used for 12 player 3v3... or 24 player 6v6... wars.
    /// </summary>
    /// <param name="home">The home team name</param>
    /// <param name="guest">The guest team name</param>
    /// <param name="team3">The name of team 3</param>
    /// <param name="team4">The name of team 4</param>
    /// <param name="format">The war format</param>
    /// <param name="players">The war player count</param>
    public War(string home, string guest, string team3, string team4, string format, string players)
    {
        races = [];
        this.home = home;
        this.guest = guest;
        this.team3 = team3;
        this.team4 = team4;
        this.format = format;
        this.players = players;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="War"/> class. <br />
    /// This constructor is to be used for 12 player 2v2... or 24 player 4v4... wars.
    /// </summary>
    /// <param name="home">The home team name</param>
    /// <param name="guest">The guest team name</param>
    /// <param name="team3">The name of team 3</param>
    /// <param name="team4">The name of team 4</param>
    /// <param name="team5">The name of team 5</param>
    /// <param name="team6">The name of team 6</param>
    /// <param name="format">The war format</param>
    /// <param name="players">The war player count</param>
    public War(string home, string guest, string team3, string team4, string team5, string team6, string format, string players)
    {
        races = [];
        this.home = home;
        this.guest = guest;
        this.team3 = team3;
        this.team4 = team4;
        this.team5 = team5;
        this.team6 = team6;
        this.format = format;
        this.players = players;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="War"/> class. <br />
    /// This constructor is to be used for 24 player 3v3... wars.
    /// </summary>
    /// <param name="home">The home team name</param>
    /// <param name="guest">The guest team name</param>
    /// <param name="team3">The name of team 3</param>
    /// <param name="team4">The name of team 4</param>
    /// <param name="team5">The name of team 5</param>
    /// <param name="team6">The name of team 6</param>
    /// <param name="team7">The name of team 7</param>
    /// <param name="team8">The name of team 8</param>
    /// <param name="format">The war format</param>
    /// <param name="players">The war player count</param>
    public War(string home, string guest, string team3, string team4, string team5, string team6, string team7, string team8, string format, string players)
    {
        races = [];
        this.home = home;
        this.guest = guest;
        this.team3 = team3;
        this.team4 = team4;
        this.team5 = team5;
        this.team6 = team6;
        this.team7 = team7;
        this.team8 = team8;
        this.format = format;
        this.players = players;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="War"/> class. <br />
    /// This constructor is to be used for 24 player 2v2... wars.
    /// </summary>
    /// <param name="home">The home team name</param>
    /// <param name="guest">The guest team name</param>
    /// <param name="team3">The name of team 3</param>
    /// <param name="team4">The name of team 4</param>
    /// <param name="team5">The name of team 5</param>
    /// <param name="team6">The name of team 6</param>
    /// <param name="team7">The name of team 7</param>
    /// <param name="team8">The name of team 8</param>
    /// <param name="team9">The name of team 9</param>
    /// <param name="team10">The name of team 10</param>
    /// <param name="team11">The name of team 11</param>
    /// <param name="team12">The name of team 12</param>
    /// <param name="format">The war format</param>
    /// <param name="players">The war player count</param>
    public War(string home, string guest, string team3, string team4, string team5, string team6, string team7, string team8, string team9, string team10, string team11, string team12, string format, string players)
    {
        races = [];
        this.home = home;
        this.guest = guest;
        this.team3 = team3;
        this.team4 = team4;
        this.team5 = team5;
        this.team6 = team6;
        this.team7 = team7;
        this.team8 = team8;
        this.team9 = team9;
        this.team10 = team10;
        this.team11 = team11;
        this.team12 = team12;
        this.format = format;
        this.players = players;
    }

    /// <summary>
    /// Adds a race to the war.
    /// </summary>
    /// <param name="race">The race</param>
    public void AddRace(Race race) => races.Add(race);

    /// <summary>
    /// Calculates the difference of a given team.
    /// </summary>
    /// <param name="team">The team number</param>
    /// <returns>Returns the difference of a team.</returns>
    public int GetDifference(int team)
    {
        int total = 0;
        foreach (Race r in races) total += r.GetDifference(team);
        return total;
    }

    /// <summary>
    /// Gets the full score of the home team this war.
    /// </summary>
    /// <returns>Returns the full score of the war.</returns>
    public int GetFullScore()
    {
        int total = 0;
        foreach (Race r in races) total += r.HomeTeamScore;
        return total;
    }

    /// <summary>
    /// Gets the full score of an enemy team.
    /// </summary>
    /// <param name="team">The enemy team number</param>
    /// <returns>Returns the full enemy team's score of this war.</returns>
    public int GetFullEnemyScore(int team)
    {
        switch (team)
        {
            case 1:
                int total = 0;
                foreach (Race r in races) total += r.GuestTeamScore;
                return total;
            case 2:
                total = 0;
                foreach (Race r in races) total += r.Team3Score;
                return total;
            case 3:
                total = 0;
                foreach (Race r in races) total += r.Team4Score;
                return total;
            case 4:
                total = 0;
                foreach (Race r in races) total += r.Team5Score;
                return total;
            case 5:
                total = 0;
                foreach (Race r in races) total += r.Team6Score;
                return total;
            case 6:
                total = 0;
                foreach (Race r in races) total += r.Team7Score;
                return total;
            case 7:
                total = 0;
                foreach (Race r in races) total += r.Team8Score;
                return total;
            case 8:
                total = 0;
                foreach (Race r in races) total += r.Team9Score;
                return total;
            case 9:
                total = 0;
                foreach (Race r in races) total += r.Team10Score;
                return total;
            case 10:
                total = 0;
                foreach (Race r in races) total += r.Team11Score;
                return total;
            case 11:
                total = 0;
                foreach (Race r in races) total += r.Team12Score;
                return total;
            default:
                return 0;
        }
    }
}

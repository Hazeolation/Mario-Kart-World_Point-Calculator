using System.Diagnostics.Contracts;
using System.Windows.Media.Imaging;

namespace Hazeolation.MKWorldPointCalculator.View;

/// <summary>
/// Represents a race that took place within a war.
/// </summary>
internal class Race
{
    /// <summary>
    /// A list of possible placements.
    /// </summary>
    private readonly string[] spots = ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24"];

    /// <summary>
    /// The placements of the home team.
    /// </summary>
    private readonly string homeTeamPlacements;

    /// <summary>
    /// The placements of the guest team.
    /// </summary>
    private readonly string guestTeamPlacements;

    /// <summary>
    /// The placements of team 3.
    /// </summary>
    private readonly string team3Placements;

    /// <summary>
    /// The placements of team 4.
    /// </summary>
    private readonly string team4Placements;

    /// <summary>
    /// The placements of team 5.
    /// </summary>
    private readonly string team5Placements;

    /// <summary>
    /// The placements of team 6.
    /// </summary>
    private readonly string team6Placements;

    /// <summary>
    /// The placements of team 7.
    /// </summary>
    private readonly string team7Placements;

    /// <summary>
    /// The placements of team 8.
    /// </summary>
    private readonly string team8Placements;

    /// <summary>
    /// The placements of team 9.
    /// </summary>
    private readonly string team9Placements;

    /// <summary>
    /// The placements of team 10.
    /// </summary>
    private readonly string team10Placements;

    /// <summary>
    /// The placements of team 11.
    /// </summary>
    private readonly string team11Placements;

    /// <summary>
    /// The placements of team 12.
    /// </summary>
    private readonly string team12Placements;

    /// <summary>
    /// The race format.
    /// </summary>
    private readonly string format;

    /// <summary>
    /// The amount of players this race.
    /// </summary>
    private readonly string players;

    /// <summary>
    /// The track the race was on.
    /// </summary>
    private string track;

    /// <summary>
    /// The score of the home team this race.
    /// </summary>
    private int homeTeamScore;

    /// <summary>
    /// The score of the guest team this race.
    /// </summary>
    private int guestTeamScore;

    /// <summary>
    /// The score of team 3.
    /// </summary>
    private int team3Score;

    /// <summary>
    /// The score of team 4.
    /// </summary>
    private int team4Score;

    /// <summary>
    /// The score of team 5.
    /// </summary>
    private int team5Score;

    /// <summary>
    /// The score of team 6.
    /// </summary>
    private int team6Score;

    /// <summary>
    /// The score of team 7.
    /// </summary>
    private int team7Score;

    /// <summary>
    /// The score of team 8.
    /// </summary>
    private int team8Score;

    /// <summary>
    /// The score of team 9.
    /// </summary>
    private int team9Score;

    /// <summary>
    /// The score of team 10.
    /// </summary>
    private int team10Score;

    /// <summary>
    /// The score of team 11.
    /// </summary>
    private int team11Score;

    /// <summary>
    /// The score of team 12.
    /// </summary>
    private int team12Score;

    /// <summary>
    /// The URL of the track image.
    /// </summary>
    private string trackImageUrl;

    /// <summary>
    /// Gets the track of the race.
    /// </summary>
    public string Track { get => track; }

    /// <summary>
    /// Gets the score of the home team.
    /// </summary>
    public int HomeTeamScore { get => homeTeamScore; }

    /// <summary>
    /// Gets the score of the guest team.
    /// </summary>
    public int GuestTeamScore { get => guestTeamScore; }

    /// <summary>
    /// Gets the score of team 3.
    /// </summary>
    public int Team3Score { get => team3Score; }

    /// <summary>
    /// Gets the score of team 4.
    /// </summary>
    public int Team4Score { get => team4Score; }

    /// <summary>
    /// Gets the score of team 5.
    /// </summary>
    public int Team5Score { get => team5Score; }

    /// <summary>
    /// Gets the score of team 6.
    /// </summary>
    public int Team6Score { get => team6Score; }

    /// <summary>
    /// Gets the score of team 7.
    /// </summary>
    public int Team7Score { get => team7Score; }

    /// <summary>
    /// Gets the score of team 8.
    /// </summary>
    public int Team8Score { get => team8Score; }

    /// <summary>
    /// Gets the score of team 9.
    /// </summary>
    public int Team9Score { get => team9Score; }

    /// <summary>
    /// Gets the score of team 10.
    /// </summary>
    public int Team10Score { get => team10Score; }

    /// <summary>
    /// Gets the score of team 11.
    /// </summary>
    public int Team11Score { get => team11Score; }

    /// <summary>
    /// Gets the score of team 12.
    /// </summary>
    public int Team12Score { get => team12Score; }

    /// <summary>
    /// Gets the placements of the home team.
    /// </summary>
    public string HomeTeamPlacements { get => homeTeamPlacements; }

    /// <summary>
    /// Gets the placements of the guest team.
    /// </summary>
    public string GuestTeamPlacements { get => guestTeamPlacements; }

    /// <summary>
    /// Gets the placements of team 3.
    /// </summary>
    public string Team3Placements { get => team3Placements; }

    /// <summary>
    /// Gets the placements of team 4.
    /// </summary>
    public string Team4Placements { get => team4Placements; }

    /// <summary>
    /// Gets the placements of team 5.
    /// </summary>
    public string Team5Placements { get => team5Placements; }

    /// <summary>
    /// Gets the placements of team 6.
    /// </summary>
    public string Team6Placements { get => team6Placements; }

    /// <summary>
    /// Gets the placements of team 7.
    /// </summary>
    public string Team7Placements { get => team7Placements; }

    /// <summary>
    /// Gets the placements of team 8.
    /// </summary>
    public string Team8Placements { get => team8Placements; }

    /// <summary>
    /// Gets the placements of team 9.
    /// </summary>
    public string Team9Placements { get => team9Placements; }

    /// <summary>
    /// Gets the placements of team 10.
    /// </summary>
    public string Team10Placements { get => team10Placements; }

    /// <summary>
    /// Gets the placements of team 11.
    /// </summary>
    public string Team11Placements { get => team11Placements; }

    /// <summary>
    /// Gets the placements of team 12.
    /// </summary>
    public string Team12Placements { get => team12Placements; }

    /// <summary>
    /// Gets the track image URL.
    /// </summary>
    public string TrackImageUrl { get => trackImageUrl; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Race"/> class.
    /// <param name="homeTeamPlacements">The placements of the home team</param>
    /// <param name="format">The format of the race</param>
    /// <param name="players">The amount of players in the race</param>
    public Race(string homeTeamPlacements, string format, string players)
    {
        this.homeTeamPlacements = homeTeamPlacements;
        this.format = format;
        this.players = players;
        this.trackImageUrl = string.Empty;
        InitScore();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Race"/> class. <br />
    /// This constructor is to be used for 12 player 6v6 or 24 player 12v12 races.
    /// <param name="homeTeamPlacements">The placements of the home team</param>
    /// <param name="guestTeamPlacements">The placements of the guest team</param>
    /// <param name="format">The format of the race</param>
    /// <param name="players">The amount of players in the race</param>
    public Race(string homeTeamPlacements, string guestTeamPlacements, string format, string players)
    {
        this.homeTeamPlacements = homeTeamPlacements;
        this.guestTeamPlacements = guestTeamPlacements;
        this.format = format;
        this.players = players;
        this.trackImageUrl = string.Empty;
        InitScore();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Race"/> class. <br />
    /// This constructor is to be used for 12 player 4v4... races.
    /// <param name="homeTeamPlacements">The placements of the home team</param>
    /// <param name="guestTeamPlacements">The placements of the guest team</param>
    /// <param name="team3Placements">The placements of team 3</param>
    /// <param name="format">The format of the race</param>
    /// <param name="players">The amount of players in the race</param>
    public Race(string homeTeamPlacements, string guestTeamPlacements, string team3Placements, string format, string players)
    {
        this.homeTeamPlacements = homeTeamPlacements;
        this.guestTeamPlacements = guestTeamPlacements;
        this.team3Placements = team3Placements;
        this.format = format;
        this.players = players;
        this.trackImageUrl = string.Empty;
        InitScore();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Race"/> class. <br />
    /// This constructor is to be used for 12 player 3v3... or 24 player 6v6... races.
    /// <param name="homeTeamPlacements">The placements of the home team</param>
    /// <param name="guestTeamPlacements">The placements of the guest team</param>
    /// <param name="team3Placements">The placements of team 3</param>
    /// <param name="team4Placements">The placements of team 4</param>
    /// <param name="format">The format of the race</param>
    /// <param name="players">The amount of players in the race</param>
    public Race(string homeTeamPlacements, string guestTeamPlacements, string team3Placements, string team4Placements, string format, string players)
    {
        this.homeTeamPlacements = homeTeamPlacements;
        this.guestTeamPlacements = guestTeamPlacements;
        this.team3Placements = team3Placements;
        this.team4Placements = team4Placements;
        this.format = format;
        this.players = players;
        this.trackImageUrl = string.Empty;
        InitScore();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Race"/> class. <br />
    /// This constructor is to be used for 12 player 2v2... or 24 player 4v4... races.
    /// <param name="homeTeamPlacements">The placements of the home team</param>
    /// <param name="guestTeamPlacements">The placements of the guest team</param>
    /// <param name="team3Placements">The placements of team 3</param>
    /// <param name="team4Placements">The placements of team 4</param>
    /// <param name="team5Placements">The placements of team 5</param>
    /// <param name="team6Placements">The placements of team 6</param>
    /// <param name="format">The format of the race</param>
    /// <param name="players">The amount of players in the race</param>
    public Race(string homeTeamPlacements, string guestTeamPlacements, string team3Placements, string team4Placements, string team5Placements, string team6Placements, string format, string players)
    {
        this.homeTeamPlacements = homeTeamPlacements;
        this.guestTeamPlacements = guestTeamPlacements;
        this.team3Placements = team3Placements;
        this.team4Placements = team4Placements;
        this.team5Placements = team5Placements;
        this.team6Placements = team6Placements;
        this.format = format;
        this.players = players;
        this.trackImageUrl = string.Empty;
        InitScore();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Race"/> class. <br />
    /// This constructor is to be used for 24 player 3v3... races.
    /// <param name="homeTeamPlacements">The placements of the home team</param>
    /// <param name="guestTeamPlacements">The placements of the guest team</param>
    /// <param name="team3Placements">The placements of team 3</param>
    /// <param name="team4Placements">The placements of team 4</param>
    /// <param name="team5Placements">The placements of team 5</param>
    /// <param name="team6Placements">The placements of team 6</param>
    /// <param name="team7Placements">The placements of team 7</param>
    /// <param name="team8Placements">The placements of team 8</param>
    /// <param name="format">The format of the race</param>
    /// <param name="players">The amount of players in the race</param>
    public Race(string homeTeamPlacements, string guestTeamPlacements, string team3Placements, string team4Placements, string team5Placements, string team6Placements, string team7Placements, string team8Placements, string format, string players)
    {
        this.homeTeamPlacements = homeTeamPlacements;
        this.guestTeamPlacements = guestTeamPlacements;
        this.team3Placements = team3Placements;
        this.team4Placements = team4Placements;
        this.team5Placements = team5Placements;
        this.team6Placements = team6Placements;
        this.team7Placements = team7Placements;
        this.team8Placements = team8Placements;
        this.format = format;
        this.players = players;
        this.trackImageUrl = string.Empty;
        InitScore();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Race"/> class. <br />
    /// This constructor is to be used for 24 player 2v2... races.
    /// <param name="homeTeamPlacements">The placements of the home team</param>
    /// <param name="guestTeamPlacements">The placements of the guest team</param>
    /// <param name="team3Placements">The placements of team 3</param>
    /// <param name="team4Placements">The placements of team 4</param>
    /// <param name="team5Placements">The placements of team 5</param>
    /// <param name="team6Placements">The placements of team 6</param>
    /// <param name="team7Placements">The placements of team 7</param>
    /// <param name="team8Placements">The placements of team 8</param>
    /// <param name="team9Placements">The placements of team 9</param>
    /// <param name="team10Placements">The placements of team 10</param>
    /// <param name="team11Placements">The placements of team 11</param>
    /// <param name="team12Placements">The placements of team 12</param>
    /// <param name="format">The format of the race</param>
    /// <param name="players">The amount of players in the race</param>
    public Race(string homeTeamPlacements, string guestTeamPlacements, string team3Placements, string team4Placements, string team5Placements, string team6Placements, string team7Placements, string team8Placements, string team9Placements, string team10Placements, string team11Placements, string team12Placements, string format, string players)
    {
        this.homeTeamPlacements = homeTeamPlacements;
        this.guestTeamPlacements = guestTeamPlacements;
        this.team3Placements = team3Placements;
        this.team4Placements = team4Placements;
        this.team5Placements = team5Placements;
        this.team6Placements = team6Placements;
        this.team7Placements = team7Placements;
        this.team8Placements = team8Placements;
        this.team9Placements = team9Placements;
        this.team10Placements = team10Placements;
        this.team11Placements = team11Placements;
        this.team12Placements = team12Placements;
        this.format = format;
        this.players = players;
        this.trackImageUrl = string.Empty;
        InitScore();
    }

    /// <summary>
    /// Gets the placements of the race.
    /// </summary>
    /// <param name="teamPlacementsNeeded">The team of which to get the placements</param>
    /// <param name="mirror">The mirror of the placements</param>
    /// <returns>Returns the placements of the requested team.</returns>
    public string[] GetPlacements(int teamPlacementsNeeded, out string[] mirror)
    {
        bool is24PlayerRace = players == "24";

        int maxTeamIndex = is24PlayerRace ? 11 : 5;

        if (teamPlacementsNeeded < 0 || teamPlacementsNeeded > maxTeamIndex) throw new Exception("Invalid team index for the selected race type.");

        string placementsSource = teamPlacementsNeeded switch
        {
            0 => homeTeamPlacements,
            1 => guestTeamPlacements,
            2 => team3Placements,
            3 => team4Placements,
            4 => team5Placements,
            5 => team6Placements,
            6 when is24PlayerRace => team7Placements,
            7 when is24PlayerRace => team8Placements,
            8 when is24PlayerRace => team9Placements,
            9 when is24PlayerRace => team10Placements,
            10 when is24PlayerRace => team11Placements,
            11 when is24PlayerRace => team12Placements,
            _ => throw new Exception("An unexpected error has occurred whilst getting placements for a race.")
        };

        string[] placements = placementsSource.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        mirror = new string[placements.Length];
        int mirrorIndex = 0;

        foreach (string spot in spots)
        {
            if (!placements.Any(p => p.Contains(spot)))
            {
                mirror[mirrorIndex++] = spot;

                if (mirrorIndex == mirror.Length) break;
            }
        }

        return placements;
    }

    /// <summary>
    /// Gets the difference between the home team and the provided opponent team.
    /// </summary>
    /// <param name="team">The opposing team. 0 equals team 2</param>
    /// <returns>Returns the difference between the home team and the provided opponent team.</returns>
    public int GetDifference(int team) => team switch
    {
        0 => homeTeamScore - guestTeamScore,
        1 => homeTeamScore - team3Score,
        2 => homeTeamScore - team4Score,
        3 => homeTeamScore - team5Score,
        4 => homeTeamScore - team6Score,
        5 => homeTeamScore - team7Score,
        6 => homeTeamScore - team8Score,
        7 => homeTeamScore - team9Score,
        8 => homeTeamScore - team10Score,
        9 => homeTeamScore - team11Score,
        10 => homeTeamScore - team12Score,
        _ => -255,
    };

    /// <summary>
    /// Sets the track for the race.
    /// </summary>
    /// <param name="track">The track the race was on</param>
    public void SetTrack(string track)
    {
        this.track = track;
    }

    /// <summary>
    /// Initiates the score for the race.
    /// </summary>
    public void InitScore()
    {
        int maxScore = players == "24" ? 144 : 82;

        int GetPoints(int placement)
        {
            if (players == "12")
            {
                // 12-player scoring
                int[] points12 = [15, 12, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1];

                if (placement < 1 || placement > 12) throw new Exception("Invalid placement for 12-player race.");

                return points12[placement - 1];
            }
            else if (players == "24")
            {
                // 24-player scoring
                int[] points24 = [15, 12, 10, 9, 9, 8, 8, 7, 7, 6, 6, 6, 5, 5, 5, 4, 4, 4, 3, 3, 3, 2, 2, 1];

                if (placement < 1 || placement > 24) throw new Exception("Invalid placement for 24-player race.");

                return points24[placement - 1];
            }
            else
            {
                throw new Exception("Unexpected player count.");
            }
        }


        int ScoreTeam(int teamIndex, int expectedPlacements)
        {
            string[] placements = GetPlacements(teamIndex, out _);

            if (placements.Length != expectedPlacements)
                throw new Exception($"Invalid amount of placements for team {teamIndex + 1}.");

            int score = 0;
            foreach (string p in placements)
            {
                score += GetPoints(int.Parse(p));
            }

            return score;
        }

        // ---------- 24 PLAYER RACES ----------
        if (players == "24")
        {
            switch (format)
            {
                case "12v12":
                    homeTeamScore = ScoreTeam(0, 12);
                    guestTeamScore = maxScore - homeTeamScore;
                    break;
                case "6v6...":
                    homeTeamScore = ScoreTeam(0, 6);
                    guestTeamScore = ScoreTeam(1, 6);
                    team3Score = maxScore - homeTeamScore - guestTeamScore;
                    break;
                case "4v4...":
                    homeTeamScore = ScoreTeam(0, 4);
                    guestTeamScore = ScoreTeam(1, 4);
                    team3Score = ScoreTeam(2, 4);
                    team4Score = ScoreTeam(3, 4);
                    team5Score = ScoreTeam(4, 4);
                    team6Score = maxScore - homeTeamScore - guestTeamScore - team3Score - team4Score - team5Score;
                    break;
                case "3v3...":
                    homeTeamScore = ScoreTeam(0, 3);
                    guestTeamScore = ScoreTeam(1, 3);
                    team3Score = ScoreTeam(2, 3);
                    team4Score = ScoreTeam(3, 3);
                    team5Score = ScoreTeam(4, 3);
                    team6Score = ScoreTeam(5, 3);
                    team7Score = ScoreTeam(6, 3);
                    team8Score = maxScore - homeTeamScore - guestTeamScore - team3Score - team4Score - team5Score - team6Score - team7Score;
                    break;
                case "2v2...":
                    homeTeamScore = ScoreTeam(0, 2);
                    guestTeamScore = ScoreTeam(1, 2);
                    team3Score = ScoreTeam(2, 2);
                    team4Score = ScoreTeam(3, 2);
                    team5Score = ScoreTeam(4, 2);
                    team6Score = ScoreTeam(5, 2);
                    team7Score = ScoreTeam(6, 2);
                    team8Score = ScoreTeam(7, 2);
                    team9Score = ScoreTeam(8, 2);
                    team10Score = ScoreTeam(9, 2);
                    team11Score = ScoreTeam(10, 2);
                    team12Score = maxScore - homeTeamScore - guestTeamScore - team3Score - team4Score - team5Score - team6Score - team7Score - team8Score - team9Score - team10Score - team11Score;
                    break;
                default:
                    throw new Exception("Unsupported format for 24-player race.");
            }
        }

        // ---------- 12 PLAYER RACES ----------
        else if (players == "12")
        {
            switch (format)
            {
                case "6v6":
                    homeTeamScore = ScoreTeam(0, 6);
                    guestTeamScore = maxScore - homeTeamScore;
                    break;
                case "4v4...":
                    homeTeamScore = ScoreTeam(0, 4);
                    guestTeamScore = ScoreTeam(1, 4);
                    team3Score = maxScore - homeTeamScore - guestTeamScore;
                    break;
                case "3v3...":
                    homeTeamScore = ScoreTeam(0, 3);
                    guestTeamScore = ScoreTeam(1, 3);
                    team3Score = ScoreTeam(2, 3);
                    team4Score = maxScore - homeTeamScore - guestTeamScore - team3Score;
                    break;
                case "2v2...":
                    homeTeamScore = ScoreTeam(0, 2);
                    guestTeamScore = ScoreTeam(1, 2);
                    team3Score = ScoreTeam(2, 2);
                    team4Score = ScoreTeam(3, 2);
                    team5Score = ScoreTeam(4, 2);
                    team6Score = maxScore - homeTeamScore - guestTeamScore - team3Score - team4Score - team5Score;
                    break;
                default:
                    throw new Exception("Unsupported format for 12-player race.");
            }
        }
        else
        {
            throw new Exception("Invalid player count.");
        }
    }
}

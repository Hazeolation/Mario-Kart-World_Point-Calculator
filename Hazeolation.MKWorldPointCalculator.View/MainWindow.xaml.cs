using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Hazeolation.MKWorldPointCalculator.View;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    /// <summary>
    /// Holds all track images and their file paths.
    /// </summary>
    private readonly Dictionary<string, BitmapImage> trackImages = [];

    /// <summary>
    /// Shows whether the program initialisation is done.
    /// </summary>
    private readonly bool isInitialized = false;

    /// <summary>
    /// Represents the currently ongoing war.
    /// </summary>
    private War war;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
        LoadAllTrackImages();
        isInitialized = true;
    }

    /// <summary>
    /// Loads all track images from the files into cache.
    /// </summary>
    private void LoadAllTrackImages()
    {
        string srcFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "src");

        if (!Directory.Exists(srcFolder))
        {
            MessageBox.Show("Image Folder was not found: \"src\"", "Error: Image Folder Not Found", MessageBoxButton.OK);
            return;
        }

        foreach (string file in Directory.GetFiles(srcFolder))
        {
            string code = System.IO.Path.GetFileNameWithoutExtension(file).ToLower();
            BitmapImage bmp = new();
            bmp.BeginInit();
            bmp.UriSource = new Uri(file, UriKind.Absolute);
            bmp.CacheOption = BitmapCacheOption.OnLoad;
            bmp.EndInit();

            trackImages[code] = bmp;
        }
    }

    /// <summary>
    /// Validates the entered race data.
    /// </summary>
    /// <param name="valid">Indicates whether the race data is valid.</param>
    /// <returns>A Race object if the race data is valid, otherwise null.</returns>
    /// <exception cref="Exception">Throws an <see cref="Exception"/> if unexpected data was entered.</exception>
    private Race RaceValidation(out bool valid)
    {
        valid = false;

        if (string.IsNullOrEmpty(tbSpotsTeam1.Text))
        {
            MessageBox.Show("No placements were entered for the home team.", "Error: Missing Home Team Placements", MessageBoxButton.OK);
            return null!;
        }

        if (lbxFullWar.Items.Count == 0)
        {
            MessageBox.Show("Please start a war before adding a race.", "Error: No War Started", MessageBoxButton.OK);
            return null!;
        }

        string playerCount = cbPlayers.SelectedIndex == 0 ? "12" : "24";
        string? format = cbPlayers.SelectedIndex == 0 ? (cbFormat12.SelectedItem as ComboBoxItem)!.Content.ToString() : (cbFormat24.SelectedItem as ComboBoxItem)!.Content.ToString();

        static bool ValidateTeam(string placements, string teamName)
        {
            if (string.IsNullOrEmpty(placements))
            {
                MessageBox.Show($"No placements were entered for {teamName}.", "Error: Missing Team Placements", MessageBoxButton.OK);
                return false;
            }
            return true;
        }

        string[] teamPlacements = [
            tbSpotsTeam1.Text,
            tbSpotsTeam2.Text,
            tbSpotsTeam3.Text,
            tbSpotsTeam4.Text,
            tbSpotsTeam5.Text,
            tbSpotsTeam6.Text,
            tbSpotsTeam7.Text,
            tbSpotsTeam8.Text,
            tbSpotsTeam9.Text,
            tbSpotsTeam10.Text,
            tbSpotsTeam11.Text,
            tbSpotsTeam12.Text
        ];

        int totalTeams = playerCount switch
        {
            "12" => cbFormat12.SelectedIndex switch
            {
                0 => 2,  // 6v6
                1 => 3,  // 4v4v4
                2 => 4,  // 3v3v3v3
                3 => 6,  // 2v2v2v2v2v2
                _ => throw new Exception("Unknown 12-player format")
            },
            "24" => cbFormat24.SelectedIndex switch
            {
                0 => 2,   // 12v12
                1 => 4,   // 6v6v6v6
                2 => 6,   // 4v4v4v4v4v4
                3 => 8,   // 3v3v3v3v3v3v3v3
                4 => 12,  // 2v2x12
                _ => throw new Exception("Unknown 24-player format")
            },
            _ => throw new Exception("Unknown player count")
        };

        for (int i = 0; i < totalTeams - 1; i++)
            if (!ValidateTeam(teamPlacements[i], $"Team {i + 1}")) return null!;

        Race race = totalTeams switch
        {
            1 => new Race(teamPlacements[0], format!, playerCount),
            2 => new Race(teamPlacements[0], teamPlacements[1], format!, playerCount),
            3 => new Race(teamPlacements[0], teamPlacements[1], teamPlacements[2], format!, playerCount),
            4 => new Race(teamPlacements[0], teamPlacements[1], teamPlacements[2], teamPlacements[3], format!, playerCount),
            6 => new Race(teamPlacements[0], teamPlacements[1], teamPlacements[2], teamPlacements[3], teamPlacements[4], teamPlacements[5], format!, playerCount),
            8 => new Race(teamPlacements[0], teamPlacements[1], teamPlacements[2], teamPlacements[3], teamPlacements[4], teamPlacements[5], teamPlacements[6], teamPlacements[7], format!, playerCount),
            12 => new Race(teamPlacements[0], teamPlacements[1], teamPlacements[2], teamPlacements[3], teamPlacements[4], teamPlacements[5], teamPlacements[6], teamPlacements[7], teamPlacements[8], teamPlacements[9], teamPlacements[10], teamPlacements[11], format!, playerCount),
            _ => throw new Exception("Unsupported number of teams")
        };

        race.SetTrack(tbTrack.Text);

        valid = true;
        return race;
    }

    /// <summary>
    /// Handles the logic upon pressing the button "Start War".
    /// </summary>
    /// <param name="sender">The object sending the event</param>
    /// <param name="e">The event</param>
    private void OnStartWarClick(object sender, RoutedEventArgs e)
    {
        static bool Missing(TextBox tb, int team)
        {
            if (!string.IsNullOrWhiteSpace(tb.Text)) return false;
            MessageBox.Show($"Missing the name for team {team}.", $"Error: Missing Team {team} Name", MessageBoxButton.OK);
            return true;
        }

        #region Team Name Validation
        if (string.IsNullOrWhiteSpace(tbHomeTeam.Text))
        {
            MessageBox.Show("Missing the name for the home team.", "Error: Missing Home Team Name", MessageBoxButton.OK);
            return;
        }

        if (string.IsNullOrWhiteSpace(tbGuestTeam.Text))
        {
            MessageBox.Show("Missing the name for the guest team.", "Error: Missing Guest Team Name", MessageBoxButton.OK);
            return;
        }

        var requiredTeams = cbPlayers.SelectedIndex switch
        {
            // 12 player wars
            0 => cbFormat12.SelectedIndex switch
            {
                0 => Array.Empty<(TextBox, int)>(), // 6v6
                1 => new[] { (tbTeam3, 3) },
                2 => new[] { (tbTeam4, 4) },
                3 => new[] { (tbTeam5, 5), (tbTeam6, 6) },
                _ => null
            },

            // 24 player wars
            1 => cbFormat24.SelectedIndex switch
            {
                0 => Array.Empty<(TextBox, int)>(), // 12v12
                1 => new[] { (tbTeam3, 3), (tbTeam4, 4) },
                2 => new[] { (tbTeam3, 3), (tbTeam4, 4), (tbTeam5, 5), (tbTeam6, 6) },
                3 => new[] { (tbTeam7, 7), (tbTeam8, 8) },
                4 => new[] { (tbTeam3, 3), (tbTeam4, 4), (tbTeam5, 5), (tbTeam6, 6), (tbTeam7, 7), (tbTeam8, 8), (tbTeam9, 9), (tbTeam10, 10), (tbTeam11, 11), (tbTeam12, 12) },
                _ => null
            },

            _ => null
        };

        if (requiredTeams == null)
        {
            MessageBox.Show("An error occurred while checking team name fields. Please open a GitHub issue describing your steps.", "Unknown Error!", MessageBoxButton.OK);
            return;
        }

        foreach (var (tb, team) in requiredTeams)
        {
            if (Missing(tb, team)) return;
        }
        #endregion Team Name Validation

        try
        {
            string[] teamTags = [
                tbHomeTeam.Text,
                tbGuestTeam.Text,
                tbTeam3.Text,
                tbTeam4.Text,
                tbTeam5.Text,
                tbTeam6.Text,
                tbTeam7.Text,
                tbTeam8.Text,
                tbTeam9.Text,
                tbTeam10.Text,
                tbTeam11.Text,
                tbTeam12.Text
            ];

            string playerCount = cbPlayers.SelectedIndex == 0 ? "12" : "24";
            string? format = cbPlayers.SelectedIndex == 0 ? (cbFormat12.SelectedItem as ComboBoxItem)!.Content.ToString() : (cbFormat24.SelectedItem as ComboBoxItem)!.Content.ToString();

            int totalTeams = playerCount switch
            {
                "12" => cbFormat12.SelectedIndex switch
                {
                    0 => 2,  // 6v6
                    1 => 3,  // 4v4v4
                    2 => 4,  // 3v3v3v3
                    3 => 6,  // 2v2v2v2v2v2
                    _ => throw new Exception("Unknown 12-player format")
                },
                "24" => cbFormat24.SelectedIndex switch
                {
                    0 => 2,   // 12v12
                    1 => 4,   // 6v6v6v6
                    2 => 6,   // 4v4v4v4v4v4
                    3 => 8,   // 3v3v3v3v3v3v3v3
                    4 => 12,  // 2v2v2v2v2v2v2v2v2v2v2v2
                    _ => throw new Exception("Unknown 24-player format")
                },
                _ => throw new Exception("Unknown player count")
            };

            war = totalTeams switch
            {
                2 => new War(tbHomeTeam.Text, tbGuestTeam.Text, format, playerCount),
                3 => new War(tbHomeTeam.Text, tbGuestTeam.Text, tbTeam3.Text, format, playerCount),
                4 => new War(tbHomeTeam.Text, tbGuestTeam.Text, tbTeam3.Text, tbTeam4.Text, format, playerCount),
                6 => new War(tbHomeTeam.Text, tbGuestTeam.Text, tbTeam3.Text, tbTeam4.Text, tbTeam5.Text, tbTeam6.Text, format, playerCount),
                8 => new War(tbHomeTeam.Text, tbGuestTeam.Text, tbTeam3.Text, tbTeam4.Text, tbTeam5.Text, tbTeam6.Text, tbTeam7.Text, tbTeam8.Text, format, playerCount),
                12 => new War(tbHomeTeam.Text, tbGuestTeam.Text, tbTeam3.Text, tbTeam4.Text, tbTeam5.Text, tbTeam6.Text, tbTeam7.Text, tbTeam8.Text, tbTeam9.Text, tbTeam10.Text, tbTeam11.Text, tbTeam12.Text, format, playerCount),
                _ => throw new Exception("Unsupported number of teams")
            };

            // Clear previous scores and races
            lbxTotalScore.Items.Clear();
            lbxLastRace.Items.Clear();
            lbxFullWar.Items.Clear();

            // Build war description dynamically
            string warDescription = totalTeams == 2 ? $"War: {string.Join(" vs. ", teamTags.Take(totalTeams))}" : $"Mogi: {string.Join(" / ", teamTags.Take(totalTeams))}";

            lbxFullWar.Items.Add(warDescription);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "An error occurred while trying to start a war");
        }
    }

    /// <summary>
    /// Handles the logic upon changing the 12 player format.
    /// </summary>
    /// <param name="sender">The object sending the event</param>
    /// <param name="e">The event</param>
    private void On12PlayerFormatChange(object sender, SelectionChangedEventArgs e)
    {
        if (!isInitialized) return;

        switch (cbFormat12.SelectedIndex)
        {
            // 6v6 selected
            case 0:
                spTeam3.IsEnabled = false;
                spTeam3.Visibility = Visibility.Hidden;

                spTeam4.IsEnabled = false;
                spTeam4.Visibility = Visibility.Hidden;

                spTeam5.IsEnabled = false;
                spTeam5.Visibility = Visibility.Hidden;

                spTeam6.IsEnabled = false;
                spTeam6.Visibility = Visibility.Hidden;

                spTeam7.IsEnabled = false;
                spTeam7.Visibility = Visibility.Hidden;

                spTeam8.IsEnabled = false;
                spTeam8.Visibility = Visibility.Hidden;

                spTeam9.IsEnabled = false;
                spTeam9.Visibility = Visibility.Hidden;

                spTeam10.IsEnabled = false;
                spTeam10.Visibility = Visibility.Hidden;

                spTeam11.IsEnabled = false;
                spTeam11.Visibility = Visibility.Hidden;

                spTeam12.IsEnabled = false;
                spTeam12.Visibility = Visibility.Hidden;

                spSpotsTeam2.IsEnabled = false;
                spSpotsTeam2.Visibility = Visibility.Hidden;

                spSpotsTeam3.IsEnabled = false;
                spSpotsTeam3.Visibility = Visibility.Hidden;

                spSpotsTeam4.IsEnabled = false;
                spSpotsTeam4.Visibility = Visibility.Hidden;

                spSpotsTeam5.IsEnabled = false;
                spSpotsTeam5.Visibility = Visibility.Hidden;

                spSpotsTeam6.IsEnabled = false;
                spSpotsTeam6.Visibility = Visibility.Hidden;

                spSpotsTeam7.IsEnabled = false;
                spSpotsTeam7.Visibility = Visibility.Hidden;

                spSpotsTeam8.IsEnabled = false;
                spSpotsTeam8.Visibility = Visibility.Hidden;

                spSpotsTeam9.IsEnabled = false;
                spSpotsTeam9.Visibility = Visibility.Hidden;

                spSpotsTeam10.IsEnabled = false;
                spSpotsTeam10.Visibility = Visibility.Hidden;

                spSpotsTeam11.IsEnabled = false;
                spSpotsTeam11.Visibility = Visibility.Hidden;

                spSpotsTeam12.IsEnabled = false;
                spSpotsTeam12.Visibility = Visibility.Hidden;
                break;
            // 4v4v4 selected
            case 1:
                spTeam3.IsEnabled = true;
                spTeam3.Visibility = Visibility.Visible;

                spTeam4.IsEnabled = false;
                spTeam4.Visibility = Visibility.Hidden;

                spTeam5.IsEnabled = false;
                spTeam5.Visibility = Visibility.Hidden;

                spTeam6.IsEnabled = false;
                spTeam6.Visibility = Visibility.Hidden;

                spTeam7.IsEnabled = false;
                spTeam7.Visibility = Visibility.Hidden;

                spTeam8.IsEnabled = false;
                spTeam8.Visibility = Visibility.Hidden;

                spTeam9.IsEnabled = false;
                spTeam9.Visibility = Visibility.Hidden;

                spTeam10.IsEnabled = false;
                spTeam10.Visibility = Visibility.Hidden;

                spTeam11.IsEnabled = false;
                spTeam11.Visibility = Visibility.Hidden;

                spTeam12.IsEnabled = false;
                spTeam12.Visibility = Visibility.Hidden;

                spSpotsTeam2.IsEnabled = true;
                spSpotsTeam2.Visibility = Visibility.Visible;

                spSpotsTeam3.IsEnabled = false;
                spSpotsTeam3.Visibility = Visibility.Hidden;

                spSpotsTeam4.IsEnabled = false;
                spSpotsTeam4.Visibility = Visibility.Hidden;

                spSpotsTeam5.IsEnabled = false;
                spSpotsTeam5.Visibility = Visibility.Hidden;

                spSpotsTeam6.IsEnabled = false;
                spSpotsTeam6.Visibility = Visibility.Hidden;

                spSpotsTeam7.IsEnabled = false;
                spSpotsTeam7.Visibility = Visibility.Hidden;

                spSpotsTeam8.IsEnabled = false;
                spSpotsTeam8.Visibility = Visibility.Hidden;

                spSpotsTeam9.IsEnabled = false;
                spSpotsTeam9.Visibility = Visibility.Hidden;

                spSpotsTeam10.IsEnabled = false;
                spSpotsTeam10.Visibility = Visibility.Hidden;

                spSpotsTeam11.IsEnabled = false;
                spSpotsTeam11.Visibility = Visibility.Hidden;

                spSpotsTeam12.IsEnabled = false;
                spSpotsTeam12.Visibility = Visibility.Hidden;
                break;
            // 3v3v3v3 selected
            case 2:
                spTeam3.IsEnabled = true;
                spTeam3.Visibility = Visibility.Visible;

                spTeam4.IsEnabled = true;
                spTeam4.Visibility = Visibility.Visible;

                spTeam5.IsEnabled = false;
                spTeam5.Visibility = Visibility.Hidden;

                spTeam6.IsEnabled = false;
                spTeam6.Visibility = Visibility.Hidden;

                spTeam7.IsEnabled = false;
                spTeam7.Visibility = Visibility.Hidden;

                spTeam8.IsEnabled = false;
                spTeam8.Visibility = Visibility.Hidden;

                spTeam9.IsEnabled = false;
                spTeam9.Visibility = Visibility.Hidden;

                spTeam10.IsEnabled = false;
                spTeam10.Visibility = Visibility.Hidden;

                spTeam11.IsEnabled = false;
                spTeam11.Visibility = Visibility.Hidden;

                spTeam12.IsEnabled = false;
                spTeam12.Visibility = Visibility.Hidden;

                spSpotsTeam2.IsEnabled = true;
                spSpotsTeam2.Visibility = Visibility.Visible;

                spSpotsTeam3.IsEnabled = true;
                spSpotsTeam3.Visibility = Visibility.Visible;

                spSpotsTeam4.IsEnabled = false;
                spSpotsTeam4.Visibility = Visibility.Hidden;

                spSpotsTeam5.IsEnabled = false;
                spSpotsTeam5.Visibility = Visibility.Hidden;

                spSpotsTeam6.IsEnabled = false;
                spSpotsTeam6.Visibility = Visibility.Hidden;

                spSpotsTeam7.IsEnabled = false;
                spSpotsTeam7.Visibility = Visibility.Hidden;

                spSpotsTeam8.IsEnabled = false;
                spSpotsTeam8.Visibility = Visibility.Hidden;

                spSpotsTeam9.IsEnabled = false;
                spSpotsTeam9.Visibility = Visibility.Hidden;

                spSpotsTeam10.IsEnabled = false;
                spSpotsTeam10.Visibility = Visibility.Hidden;

                spSpotsTeam11.IsEnabled = false;
                spSpotsTeam11.Visibility = Visibility.Hidden;

                spSpotsTeam12.IsEnabled = false;
                spSpotsTeam12.Visibility = Visibility.Hidden;
                break;
            // 2v2v2v2v2v2 selected
            case 3:
                spTeam3.IsEnabled = true;
                spTeam3.Visibility = Visibility.Visible;

                spTeam4.IsEnabled = true;
                spTeam4.Visibility = Visibility.Visible;

                spTeam5.IsEnabled = true;
                spTeam5.Visibility = Visibility.Visible;

                spTeam6.IsEnabled = true;
                spTeam6.Visibility = Visibility.Visible;

                spTeam7.IsEnabled = false;
                spTeam7.Visibility = Visibility.Hidden;

                spTeam8.IsEnabled = false;
                spTeam8.Visibility = Visibility.Hidden;

                spTeam9.IsEnabled = false;
                spTeam9.Visibility = Visibility.Hidden;

                spTeam10.IsEnabled = false;
                spTeam10.Visibility = Visibility.Hidden;

                spTeam11.IsEnabled = false;
                spTeam11.Visibility = Visibility.Hidden;

                spTeam12.IsEnabled = false;
                spTeam12.Visibility = Visibility.Hidden;

                spSpotsTeam2.IsEnabled = true;
                spSpotsTeam2.Visibility = Visibility.Visible;

                spSpotsTeam3.IsEnabled = true;
                spSpotsTeam3.Visibility = Visibility.Visible;

                spSpotsTeam4.IsEnabled = true;
                spSpotsTeam4.Visibility = Visibility.Visible;

                spSpotsTeam5.IsEnabled = true;
                spSpotsTeam5.Visibility = Visibility.Visible;

                spSpotsTeam6.IsEnabled = false;
                spSpotsTeam6.Visibility = Visibility.Hidden;

                spSpotsTeam7.IsEnabled = false;
                spSpotsTeam7.Visibility = Visibility.Hidden;

                spSpotsTeam8.IsEnabled = false;
                spSpotsTeam8.Visibility = Visibility.Hidden;

                spSpotsTeam9.IsEnabled = false;
                spSpotsTeam9.Visibility = Visibility.Hidden;

                spSpotsTeam10.IsEnabled = false;
                spSpotsTeam10.Visibility = Visibility.Hidden;

                spSpotsTeam11.IsEnabled = false;
                spSpotsTeam11.Visibility = Visibility.Hidden;

                spSpotsTeam12.IsEnabled = false;
                spSpotsTeam12.Visibility = Visibility.Hidden;
                break;
            default:
                MessageBox.Show("An error with the 12 player format dropdown has occurred. Please open a GitHub issue describing your steps.", "Unknown Error!", MessageBoxButton.OK);
                break;
        }
    }

    /// <summary>
    /// Handles the logic upon changing the 24 player format.
    /// </summary>
    /// <param name="sender">The object sending the event</param>
    /// <param name="e">The event</param>
    private void On24PlayerFormatChange(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        if (!isInitialized) return;

        switch (cbFormat24.SelectedIndex)
        {
            // 12v12 selected
            case 0:
                spTeam3.IsEnabled = false;
                spTeam3.Visibility = Visibility.Hidden;

                spTeam4.IsEnabled = false;
                spTeam4.Visibility = Visibility.Hidden;

                spTeam5.IsEnabled = false;
                spTeam5.Visibility = Visibility.Hidden;

                spTeam6.IsEnabled = false;
                spTeam6.Visibility = Visibility.Hidden;

                spTeam7.IsEnabled = false;
                spTeam7.Visibility = Visibility.Hidden;

                spTeam8.IsEnabled = false;
                spTeam8.Visibility = Visibility.Hidden;

                spTeam9.IsEnabled = false;
                spTeam9.Visibility = Visibility.Hidden;

                spTeam10.IsEnabled = false;
                spTeam10.Visibility = Visibility.Hidden;

                spTeam11.IsEnabled = false;
                spTeam11.Visibility = Visibility.Hidden;

                spTeam12.IsEnabled = false;
                spTeam12.Visibility = Visibility.Hidden;

                spSpotsTeam2.IsEnabled = false;
                spSpotsTeam2.Visibility = Visibility.Hidden;

                spSpotsTeam3.IsEnabled = false;
                spSpotsTeam3.Visibility = Visibility.Hidden;

                spSpotsTeam4.IsEnabled = false;
                spSpotsTeam4.Visibility = Visibility.Hidden;

                spSpotsTeam5.IsEnabled = false;
                spSpotsTeam5.Visibility = Visibility.Hidden;

                spSpotsTeam6.IsEnabled = false;
                spSpotsTeam6.Visibility = Visibility.Hidden;

                spSpotsTeam7.IsEnabled = false;
                spSpotsTeam7.Visibility = Visibility.Hidden;

                spSpotsTeam8.IsEnabled = false;
                spSpotsTeam8.Visibility = Visibility.Hidden;

                spSpotsTeam9.IsEnabled = false;
                spSpotsTeam9.Visibility = Visibility.Hidden;

                spSpotsTeam10.IsEnabled = false;
                spSpotsTeam10.Visibility = Visibility.Hidden;

                spSpotsTeam11.IsEnabled = false;
                spSpotsTeam11.Visibility = Visibility.Hidden;

                spSpotsTeam12.IsEnabled = false;
                spSpotsTeam12.Visibility = Visibility.Hidden;
                break;
            // 6v6v6v6 selected
            case 1:
                spTeam3.IsEnabled = true;
                spTeam3.Visibility = Visibility.Visible;

                spTeam4.IsEnabled = true;
                spTeam4.Visibility = Visibility.Visible;

                spTeam5.IsEnabled = false;
                spTeam5.Visibility = Visibility.Hidden;

                spTeam6.IsEnabled = false;
                spTeam6.Visibility = Visibility.Hidden;

                spTeam7.IsEnabled = false;
                spTeam7.Visibility = Visibility.Hidden;

                spTeam8.IsEnabled = false;
                spTeam8.Visibility = Visibility.Hidden;

                spTeam9.IsEnabled = false;
                spTeam9.Visibility = Visibility.Hidden;

                spTeam10.IsEnabled = false;
                spTeam10.Visibility = Visibility.Hidden;

                spTeam11.IsEnabled = false;
                spTeam11.Visibility = Visibility.Hidden;

                spTeam12.IsEnabled = false;
                spTeam12.Visibility = Visibility.Hidden;

                spSpotsTeam2.IsEnabled = true;
                spSpotsTeam2.Visibility = Visibility.Visible;

                spSpotsTeam3.IsEnabled = true;
                spSpotsTeam3.Visibility = Visibility.Visible;

                spSpotsTeam4.IsEnabled = false;
                spSpotsTeam4.Visibility = Visibility.Hidden;

                spSpotsTeam5.IsEnabled = false;
                spSpotsTeam5.Visibility = Visibility.Hidden;

                spSpotsTeam6.IsEnabled = false;
                spSpotsTeam6.Visibility = Visibility.Hidden;

                spSpotsTeam7.IsEnabled = false;
                spSpotsTeam7.Visibility = Visibility.Hidden;

                spSpotsTeam8.IsEnabled = false;
                spSpotsTeam8.Visibility = Visibility.Hidden;

                spSpotsTeam9.IsEnabled = false;
                spSpotsTeam9.Visibility = Visibility.Hidden;

                spSpotsTeam10.IsEnabled = false;
                spSpotsTeam10.Visibility = Visibility.Hidden;

                spSpotsTeam11.IsEnabled = false;
                spSpotsTeam11.Visibility = Visibility.Hidden;

                spSpotsTeam12.IsEnabled = false;
                spSpotsTeam12.Visibility = Visibility.Hidden;
                break;
            // 4v4v4v4v4v4 selected
            case 2:
                spTeam3.IsEnabled = true;
                spTeam3.Visibility = Visibility.Visible;

                spTeam4.IsEnabled = true;
                spTeam4.Visibility = Visibility.Visible;

                spTeam5.IsEnabled = true;
                spTeam5.Visibility = Visibility.Visible;

                spTeam6.IsEnabled = true;
                spTeam6.Visibility = Visibility.Visible;

                spTeam7.IsEnabled = false;
                spTeam7.Visibility = Visibility.Hidden;

                spTeam8.IsEnabled = false;
                spTeam8.Visibility = Visibility.Hidden;

                spTeam9.IsEnabled = false;
                spTeam9.Visibility = Visibility.Hidden;

                spTeam10.IsEnabled = false;
                spTeam10.Visibility = Visibility.Hidden;

                spTeam11.IsEnabled = false;
                spTeam11.Visibility = Visibility.Hidden;

                spTeam12.IsEnabled = false;
                spTeam12.Visibility = Visibility.Hidden;

                spSpotsTeam2.IsEnabled = true;
                spSpotsTeam2.Visibility = Visibility.Visible;

                spSpotsTeam3.IsEnabled = true;
                spSpotsTeam3.Visibility = Visibility.Visible;

                spSpotsTeam4.IsEnabled = true;
                spSpotsTeam4.Visibility = Visibility.Visible;

                spSpotsTeam5.IsEnabled = true;
                spSpotsTeam5.Visibility = Visibility.Visible;

                spSpotsTeam6.IsEnabled = false;
                spSpotsTeam6.Visibility = Visibility.Hidden;

                spSpotsTeam7.IsEnabled = false;
                spSpotsTeam7.Visibility = Visibility.Hidden;

                spSpotsTeam8.IsEnabled = false;
                spSpotsTeam8.Visibility = Visibility.Hidden;

                spSpotsTeam9.IsEnabled = false;
                spSpotsTeam9.Visibility = Visibility.Hidden;

                spSpotsTeam10.IsEnabled = false;
                spSpotsTeam10.Visibility = Visibility.Hidden;

                spSpotsTeam11.IsEnabled = false;
                spSpotsTeam11.Visibility = Visibility.Hidden;

                spSpotsTeam12.IsEnabled = false;
                spSpotsTeam12.Visibility = Visibility.Hidden;
                break;
            // 3v3v3v3v3v3v3v3 selected
            case 3:
                spTeam3.IsEnabled = true;
                spTeam3.Visibility = Visibility.Visible;

                spTeam4.IsEnabled = true;
                spTeam4.Visibility = Visibility.Visible;

                spTeam5.IsEnabled = true;
                spTeam5.Visibility = Visibility.Visible;

                spTeam6.IsEnabled = true;
                spTeam6.Visibility = Visibility.Visible;

                spTeam7.IsEnabled = true;
                spTeam7.Visibility = Visibility.Visible;

                spTeam8.IsEnabled = true;
                spTeam8.Visibility = Visibility.Visible;

                spTeam9.IsEnabled = false;
                spTeam9.Visibility = Visibility.Hidden;

                spTeam10.IsEnabled = false;
                spTeam10.Visibility = Visibility.Hidden;

                spTeam11.IsEnabled = false;
                spTeam11.Visibility = Visibility.Hidden;

                spTeam12.IsEnabled = false;
                spTeam12.Visibility = Visibility.Hidden;

                spSpotsTeam2.IsEnabled = true;
                spSpotsTeam2.Visibility = Visibility.Visible;

                spSpotsTeam3.IsEnabled = true;
                spSpotsTeam3.Visibility = Visibility.Visible;

                spSpotsTeam4.IsEnabled = true;
                spSpotsTeam4.Visibility = Visibility.Visible;

                spSpotsTeam5.IsEnabled = true;
                spSpotsTeam5.Visibility = Visibility.Visible;

                spSpotsTeam6.IsEnabled = true;
                spSpotsTeam6.Visibility = Visibility.Visible;

                spSpotsTeam7.IsEnabled = true;
                spSpotsTeam7.Visibility = Visibility.Visible;

                spSpotsTeam8.IsEnabled = false;
                spSpotsTeam8.Visibility = Visibility.Hidden;

                spSpotsTeam9.IsEnabled = false;
                spSpotsTeam9.Visibility = Visibility.Hidden;

                spSpotsTeam10.IsEnabled = false;
                spSpotsTeam10.Visibility = Visibility.Hidden;

                spSpotsTeam11.IsEnabled = false;
                spSpotsTeam11.Visibility = Visibility.Hidden;

                spSpotsTeam12.IsEnabled = false;
                spSpotsTeam12.Visibility = Visibility.Hidden;
                break;
            // 2v2v2v2v2v2v2v2v2v2v2v2 selected
            case 4:
                spTeam3.IsEnabled = true;
                spTeam3.Visibility = Visibility.Visible;

                spTeam4.IsEnabled = true;
                spTeam4.Visibility = Visibility.Visible;

                spTeam5.IsEnabled = true;
                spTeam5.Visibility = Visibility.Visible;

                spTeam6.IsEnabled = true;
                spTeam6.Visibility = Visibility.Visible;

                spTeam7.IsEnabled = true;
                spTeam7.Visibility = Visibility.Visible;

                spTeam8.IsEnabled = true;
                spTeam8.Visibility = Visibility.Visible;

                spTeam9.IsEnabled = true;
                spTeam9.Visibility = Visibility.Visible;

                spTeam10.IsEnabled = true;
                spTeam10.Visibility = Visibility.Visible;

                spTeam11.IsEnabled = true;
                spTeam11.Visibility = Visibility.Visible;

                spTeam12.IsEnabled = true;
                spTeam12.Visibility = Visibility.Visible;

                spSpotsTeam2.IsEnabled = true;
                spSpotsTeam2.Visibility = Visibility.Visible;

                spSpotsTeam3.IsEnabled = true;
                spSpotsTeam3.Visibility = Visibility.Visible;

                spSpotsTeam4.IsEnabled = true;
                spSpotsTeam4.Visibility = Visibility.Visible;

                spSpotsTeam5.IsEnabled = true;
                spSpotsTeam5.Visibility = Visibility.Visible;

                spSpotsTeam6.IsEnabled = true;
                spSpotsTeam6.Visibility = Visibility.Visible;

                spSpotsTeam7.IsEnabled = true;
                spSpotsTeam7.Visibility = Visibility.Visible;

                spSpotsTeam8.IsEnabled = true;
                spSpotsTeam8.Visibility = Visibility.Visible;

                spSpotsTeam9.IsEnabled = true;
                spSpotsTeam9.Visibility = Visibility.Visible;

                spSpotsTeam10.IsEnabled = true;
                spSpotsTeam10.Visibility = Visibility.Visible;

                spSpotsTeam11.IsEnabled = true;
                spSpotsTeam11.Visibility = Visibility.Visible;

                spSpotsTeam12.IsEnabled = false;
                spSpotsTeam12.Visibility = Visibility.Hidden;
                break;
            default:
                MessageBox.Show("An error with the 24 player format dropdown has occurred. Please open a GitHub issue describing your steps.", "Unknown Error!", MessageBoxButton.OK);
                break;
        }
    }

    /// <summary>
    /// Handles the logic upon changing the player count.
    /// </summary>
    /// <param name="sender">The object sending the event</param>
    /// <param name="e">The event</param>
    private void OnPlayerCountChange(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        if (!isInitialized) return;

        switch (cbPlayers.SelectedIndex)
        {
            // 12 players selected
            case 0:
                spFormat12.IsEnabled = true;
                spFormat12.Visibility = Visibility.Visible;

                spFormat24.IsEnabled = false;
                spFormat24.Visibility = Visibility.Hidden;

                On12PlayerFormatChange(sender, e);
                break;
            // 24 players selected
            case 1:
                spFormat24.IsEnabled = true;
                spFormat24.Visibility = Visibility.Visible;

                spFormat12.IsEnabled = false;
                spFormat12.Visibility = Visibility.Hidden;

                On24PlayerFormatChange(sender, e);
                break;
            default:
                MessageBox.Show("An error with the player count dropdown has occurred. Please open a GitHub issue describing your steps.", "Unknown Error!", MessageBoxButton.OK);
                break;
        }
    }

    /// <summary>
    /// Handles the logic upon clicking the "Add Score" button.
    /// </summary>
    /// <param name="sender">The object sending the event</param>
    /// <param name="e">The event</param>
    private void OnAddScoreClick(object sender, RoutedEventArgs e)
    {
        try
        {
            Race race = RaceValidation(out bool valid);
            if (!valid) return;

            war.AddRace(race);

            // Determine player count and format
            string playerCount = cbPlayers.SelectedIndex == 0 ? "12" : "24";
            string format = cbPlayers.SelectedIndex == 0
                ? cbFormat12.SelectedValue.ToString()!
                : cbFormat24.SelectedValue.ToString()!;

            // Determine number of teams based on format
            int totalTeams = playerCount switch
            {
                "12" => cbFormat12.SelectedIndex switch
                {
                    0 => 2, // 6v6
                    1 => 3, // 4v4v4
                    2 => 4, // 3v3v3v3
                    3 => 6, // 2v2v2v2v2v2
                    _ => throw new Exception("Unknown 12-player format")
                },
                "24" => cbFormat24.SelectedIndex switch
                {
                    0 => 2,  // 12v12
                    1 => 4,  // 6v6v6v6
                    2 => 6,  // 4v4v4v4v4v4
                    3 => 8,  // 3v3v3v3v3v3v3v3
                    4 => 12, // 2v2v2v2v2v2v2v2v2v2v2v2
                    _ => throw new Exception("Unknown 24-player format")
                },
                _ => throw new Exception("Unknown player count")
            };

            // Collect team names dynamically
            string[] teamNames = new string[] { war.Home, war.Guest, war.Team3, war.Team4, war.Team5, war.Team6, war.Team7, war.Team8, war.Team9, war.Team10, war.Team11, war.Team12 }
                .Take(totalTeams).ToArray();

            // Collect race scores dynamically
            int[] teamScores = new int[] { race.HomeTeamScore, race.GuestTeamScore, race.Team3Score, race.Team4Score, race.Team5Score, race.Team6Score, race.Team7Score, race.Team8Score, race.Team9Score, race.Team10Score, race.Team11Score, race.Team12Score }
                .Take(totalTeams).ToArray();

            // Calculate differences relative to home team using War logic
            int[] diffs = new int[teamScores.Length];
            for (int i = 0; i < teamScores.Length; i++) diffs[i] = war.GetDifference(i);

            // Update Last Race ListBox
            lbxLastRace.Items.Clear();
            lbxLastRace.Items.Add($"Score of race {lbxFullWar.Items.Count}: {race.HomeTeamPlacements}");
            lbxLastRace.Items.Add(string.Empty);
            lbxLastRace.Items.Add(string.Join("\t", teamNames));
            lbxLastRace.Items.Add(string.Join("\t", teamScores));
            lbxLastRace.Items.Add(string.Empty);
            for (int i = 1; i < totalTeams; i++) lbxLastRace.Items.Add($"Difference {teamNames[0]} - {teamNames[i]}: {teamScores[0] - teamScores[i]}");

            // Update Track Image
            DisplayImage(race.Track);

            // Update Previous Races ListBox
            string text = totalTeams == 2 ? $"{lbxFullWar.Items.Count} | {teamScores[0]} - {teamScores[1]} ({teamScores[0] - teamScores[1]})" + (!string.IsNullOrEmpty(race.Track) ? $" ({race.Track})" : string.Empty)
                : $"{lbxFullWar.Items.Count} | {string.Join(" / ", teamScores)} " + $"({string.Join(" / ", diffs.Skip(1))})" + (!string.IsNullOrEmpty(race.Track) ? $" ({race.Track})" : string.Empty);
            lbxFullWar.Items.Add(text);

            // Update Total Scores
            lbxTotalScore.Items.Clear();
            lbxTotalScore.Items.Add("Total score:");
            lbxTotalScore.Items.Add(string.Empty);
            lbxTotalScore.Items.Add(string.Join("\t", teamNames));
            lbxTotalScore.Items.Add(string.Join("\t", teamScores));
            lbxTotalScore.Items.Add(string.Empty);
            lbxTotalScore.Items.Add("Difference: " + string.Join(" / ", diffs.Take(totalTeams - 1)));

            tbSpotsTeam1.Text = string.Empty;
            tbSpotsTeam2.Text = string.Empty;
            tbSpotsTeam3.Text = string.Empty;
            tbSpotsTeam4.Text = string.Empty;
            tbSpotsTeam5.Text = string.Empty;
            tbSpotsTeam6.Text = string.Empty;
            tbSpotsTeam7.Text = string.Empty;
            tbSpotsTeam8.Text = string.Empty;
            tbSpotsTeam9.Text = string.Empty;
            tbSpotsTeam10.Text = string.Empty;
            tbSpotsTeam11.Text = string.Empty;
            tbSpotsTeam12.Text = string.Empty;
            tbTrack.Text = string.Empty;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "An error occurred while trying to add the race");
        }
    }

    /// <summary>
    /// Displays the given image.
    /// </summary>
    /// <param name="trackCode">The track code</param>
    private void DisplayImage(string trackCode)
    {
        trackCode = trackCode.ToLower();

        if (!trackImages.TryGetValue(trackCode, out BitmapImage bmp)) bmp = trackImages["blank"];

        imgTrack.Source = bmp;
    }
}
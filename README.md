# 📊 Mario Kart World Point Calculator

A WPF application to **track and calculate scores** for *Mario Kart World* competitive matches, scrimmages, and lounge style mogis. This tool lets you input race placements, automatically compute team points based on the official MKW scoring rules, and maintain ongoing war scoreboards.

---

## 🚀 Features

- Supports competitive formats for **12-player** and **24-player** rooms  
- Automatically calculates race scores based on placement  
- Handles scoring differences between all competing teams  
- Displays cumulative total scores and score differences  
- Loads track images dynamically (fully portable)  
- Designed for competitive, scrimmage, and lounge play  

---

## 📦 Supported Race Formats

### **12-Player Rooms**
- 6v6  
- 4v4v4  
- 3v3v3v3  
- 2v2v2v2v2v2  

### **24-Player Rooms**
- 12v12  
- 6v6v6v6  
- 4v4v4v4v4v4  
- 3v3v3v3v3v3v3v3  
- 2v2v2v2v2v2v2v2v2v2v2v2  

---

## 📥 Installation (To Be Changed)

1. Clone the repository:

    ```sh
    git clone https://github.com/Hazeolation/Mario-Kart-World_Point-Calculator.git
    ```

2. Open the solution file (`.sln`) in **Visual Studio**.

3. Ensure the `src` folder (containing all track images) exists in the project root.

   - **Build Action:** Content  
   - **Copy to Output Directory:** Copy if newer  

4. Build and run the application.

---

## How It Works

1. Select the player count (**12** or **24**) using the format combo boxes.
2. Select the race format and enter names of all participating teams for the war and click **Start War**.
3. Enter placements for all participating teams.
4. Click **Add Race** to:
   - Calculate race scores
   - Show score differences relative to the home team
   - Update cumulative totals
5. Track images are automatically displayed using track shorthand codes.

---

## 🖼️ Portable Image Handling

Track images are stored in the `src/` folder and loaded dynamically at runtime.  
As long as the `src` folder is next to the executable, images will load correctly.  
This keeps the application fully portable.

---

## Scoring Logic

Scoring follows **Mario Kart World competitive rules**, with separate logic for:
- 12-player rooms
- 24-player rooms

Differences are calculated relative to the home team and displayed clearly.

---

## Validation

The application validates:
- All required team placements are entered
- The selected format matches the number of teams
- Invalid or missing input is handled gracefully

---

## Contributing

Contributions are welcome!

You can help by:
- Adding additional formats
- Improving UI/UX
- Enhancing scoring logic
- Fixing bugs or edge cases

Feel free to open an issue or submit a pull request.

---

## 🏁 Credits

Created by **Hazeolation**  
Built to support *Mario Kart World* competitive and lounge play.

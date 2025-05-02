import json
import pandas as pd
import matplotlib.pyplot as plt
import seaborn as sns
from collections import defaultdict

# Load your JSON data
with open("invertilt-default-rtdb-export-April-8.json", "r") as f:
    data = json.load(f)

sessions = data["game_sessions"]

# Normalize into a flat DataFrame
records = []
for session_key, session_data in sessions.items():
    session_id = session_data.get("sessionID")
    highest_level = session_data.get("highestLevel", 0)

    if isinstance(session_data.get("levelAttempts"), dict) and "entries" in session_data["levelAttempts"]:
        attempts_data = {entry["key"]: entry["value"] for entry in session_data["levelAttempts"]["entries"]}
    elif isinstance(session_data.get("levelAttempts"), list):
        attempts_data = {i: att for i, att in enumerate(session_data["levelAttempts"]) if att is not None}
    else:
        attempts_data = {}

    for level, attempts in attempts_data.items():
        records.append({
            "sessionID": session_id,
            "level": int(level),
            "attempts": int(attempts),
            "highestLevel": highest_level
        })

df = pd.DataFrame(records)

# ========== 1. Retry Heatmap ==========
# Trim session IDs for better readability
df['short_sessionID'] = df['sessionID'].apply(lambda x: str(x)[-6:])

heatmap_data = df.pivot_table(index="short_sessionID", columns="level", values="attempts", fill_value=0)

plt.figure(figsize=(12, 8))
sns.heatmap(heatmap_data, cmap="YlOrRd", linewidths=0.5, linecolor='gray')
plt.title("Retry Heatmap: Attempts per Level per Session")
plt.xlabel("Level")
plt.ylabel("Session ID (Last 6 Chars)")
plt.tight_layout()
plt.savefig("retry_heatmap.png")
plt.close()


# ========== 2. Falloff/Drop-off Curve ==========
falloff_counts = df.groupby("sessionID")["highestLevel"].max().value_counts().sort_index()
plt.figure(figsize=(10, 6))
sns.lineplot(x=falloff_counts.index, y=falloff_counts.values, marker="o")
plt.title("Player Drop-off Curve: Highest Level Reached")
plt.xlabel("Highest Level Reached")
plt.ylabel("Number of Players")
plt.grid(True)
plt.tight_layout()
plt.savefig("dropoff_curve.png")
plt.close()

# ========== 3. Top 3 Most Failed Levels ==========
level_failures = df.groupby("level")["attempts"].sum().sort_values(ascending=False)
top_3_failed = level_failures.head(3)
plt.figure(figsize=(8, 6))
sns.barplot(x=top_3_failed.index, y=top_3_failed.values, palette="Reds_r")
plt.title("Top 3 Most Failed Levels (by Total Attempts)")
plt.xlabel("Level")
plt.ylabel("Total Attempts")
plt.tight_layout()
plt.savefig("top_3_failed_levels.png")
plt.close()

# ========== 4. Percent of Players Reaching Final Level ==========
final_level = df["level"].max()
total_players = df["sessionID"].nunique()
players_reaching_final = df[df["level"] == final_level]["sessionID"].nunique()
percent_reached = (players_reaching_final / total_players) * 100
print(f"✅ Percent of players reaching final level ({final_level}): {percent_reached:.2f}%")
# ========== 4b. Visualization ==========
# Count unique players that reached each level
level_completion = df.groupby("level")["sessionID"].nunique()

plt.figure(figsize=(8, 6))
sns.barplot(x=level_completion.index, y=level_completion.values, palette="Blues_d")
plt.title("Level Completion Rate")
plt.xlabel("Level")
plt.ylabel("Number of Players Who Reached Level")
plt.tight_layout()
plt.savefig("level_completion_rate.png")
plt.close()
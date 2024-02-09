using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Survival2D.Tools.Score
{
	internal class ScoreFileProvider
	{
        //ToDo: add to config
        private readonly string pathFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+@"\score.json";

		public void WriteScore(ScoreItem newScore)
		{
			Godot.GD.Print(pathFile);

			var scores = GetScores();
			scores.Add(newScore);

			var json = JsonSerializer.Serialize(scores);
			File.WriteAllText(pathFile, json);
		}

		public ICollection<ScoreItem> GetScores()
		{
			if(!File.Exists(pathFile))
				return new List<ScoreItem>();
			var json = File.ReadAllText(pathFile);
			return JsonSerializer.Deserialize<List<ScoreItem>>(json);
		}
	}
}

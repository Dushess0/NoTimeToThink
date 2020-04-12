using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public static class MultiplayersBuildAndRun {

	[MenuItem("File/Run Multiplayer")]
	static void PerformWin64Build2 (){
		PerformWin64Build (2);
	}

	

	static void PerformWin64Build (int playerCount)
	{
		EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone,BuildTarget.StandaloneWindows);

		BuildPipeline.BuildPlayer (GetScenePaths (), "Builds/Win64/" + GetProjectName () +".exe", BuildTarget.StandaloneWindows64, BuildOptions.AutoRunPlayer);
		
	
	}	

	

	static string GetProjectName()
	{
		string[] s = Application.dataPath.Split('/');
		return s[s.Length - 2];
	}

	static string[] GetScenePaths()
	{
		string[] scenes = new string[EditorBuildSettings.scenes.Length];

		for(int i = 0; i < scenes.Length; i++)
		{
			scenes[i] = EditorBuildSettings.scenes[i].path;
		}

		return scenes;
	}

}
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Editor window to get all "TODO" tasks from scripts
/// </summary>
class ToDoList : EditorWindow
{
	GUIStyle boldCenteredTitleStyle, centeredStyle, boldTitleStyle, frameStyle, boldButtonStyle;
	TextAsset[] scripts = new TextAsset[0];
	TextAsset[] texts = new TextAsset[0];
	Vector2 scrollPos;
	int toDoCount;

	static ToDoList window;

	[MenuItem("Tools/ToDoList")]
	static void ShowWindow()
	{
		window = GetWindow<ToDoList>();
		window.titleContent = new GUIContent("ToDoList");
		window.Show();
	}

	void OnGUI()
	{
		GenerateRequirement();

		EditorGUILayout.BeginVertical();

		EditorGUILayout.LabelField("ToDo list", boldCenteredTitleStyle);

		EditorGUILayout.Space();

		DisplayList();

		EditorGUILayout.Space();

		EditorGUILayout.LabelField("Task count : " + toDoCount, centeredStyle);

		EditorGUILayout.Space();

		if(GUILayout.Button("Refresh"))
			GetAllProjectScriptsWithToDos();

		EditorGUILayout.EndVertical();
	}

	void DisplayList()
	{
		if(scripts.Length == 0 && texts.Length == 0)
			return;

		scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUIStyle.none, GUI.skin.verticalScrollbar);

		EditorGUILayout.BeginVertical(new GUIStyle(GUI.skin.textArea));

		toDoCount = 0;

		foreach (TextAsset text in texts)
		{
			try
			{
				string name = text.name;
			}
			catch (SystemException exception)
			{
				continue;
			}

			EditorGUILayout.TextArea(text.name, boldButtonStyle);

			string[] toDos = GetTextToDo(text);
			string content = string.Empty;

			foreach (string toDo in toDos)
			{
				toDoCount++;
				content += toDo + "\n";
			}

			if(content.Length > 0)
				content = content.TrimEnd('\n');

			EditorGUILayout.TextArea(content, frameStyle, GUILayout.MaxWidth(window.position.width));
			EditorGUILayout.Space();
		}

		foreach (TextAsset script in scripts)
		{
			if(GUILayout.Button(script.name + ".cs", boldButtonStyle))
				AssetDatabase.OpenAsset(script);

			string[] toDos = GetScriptToDo(script);
			string content = string.Empty;

			foreach (string toDo in toDos)
			{
				toDoCount++;
				content += "- " + toDo + "\n";
			}

			if(content.Length > 0)
				content = content.TrimEnd('\n');

			EditorGUILayout.TextArea(content, frameStyle, GUILayout.MaxWidth(window.position.width));
			EditorGUILayout.Space();
		}

		EditorGUILayout.EndVertical();

		EditorGUILayout.EndScrollView();
	}

	void GenerateRequirement()
	{
		boldCenteredTitleStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold };
		centeredStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };
		boldTitleStyle = new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold };
		frameStyle = new GUIStyle(GUI.skin.label) { wordWrap = true, richText = true };
		boldButtonStyle = new GUIStyle(GUI.skin.box) { fontStyle = FontStyle.Bold, stretchWidth = true, alignment = TextAnchor.MiddleLeft };

		if(window == null)
			window = GetWindow<ToDoList>();
	}

	void GetAllProjectScriptsWithToDos()
	{
		string[] assetsPaths = AssetDatabase.GetAllAssetPaths();
		List<TextAsset> loadedScripts = new List<TextAsset>();
		List<TextAsset> loadedTexts = new List<TextAsset>();

		foreach (string assetPath in assetsPaths)
		{
			if(!assetPath.Contains("Packages"))
			{
				if(assetPath.EndsWith(".cs") && !assetPath.Contains(GetType().ToString()))
				{
					TextAsset script = AssetDatabase.LoadAssetAtPath<TextAsset>(assetPath);

					if(ScriptContainsToDo(script.text))
						loadedScripts.Add(script);
				}

				if(assetPath.EndsWith(".todo"))
				{
					TextAsset textFile = new TextAsset(File.ReadAllText(assetPath));

					string[] lines = assetPath.Split('/');
					textFile.name = lines[lines.Length - 1];

					if(TextContainsToDo(textFile.text))
						loadedTexts.Add(textFile);
				}
			}
		}

		scripts = loadedScripts.ToArray();
		texts = loadedTexts.ToArray();
	}

	string[] GetScriptToDo(TextAsset script)
	{
		string[] lines = script.text.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
		List<string> toDo = new List<string>();

		for (int i = 0; i < lines.Length; i++)
		{
			if(ScriptContainsToDo(lines[i]))
			{
				string[] words = lines[i].Split(new string[] { "TODO", ":" }, System.StringSplitOptions.RemoveEmptyEntries);

				if(words[1] == " ")
					toDo.Add(words[2].TrimStart(' '));
				else
					toDo.Add(words[1].Trim(' '));
			}
		}

		return toDo.ToArray();
	}

	string[] GetTextToDo(TextAsset text)
	{
		string[] lines = text.text.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
		List<string> toDo = new List<string>();

		for (int i = 0; i < lines.Length; i++)
		{
			if(lines[i][0] == '-')
				toDo.Add(lines[i]);
		}

		return toDo.ToArray();
	}

	bool ScriptContainsToDo(string text)
	{
		return text.Replace(" ", "").Contains("//TODO:");
	}

	bool TextContainsToDo(string text)
	{
		return text.Contains("\n- ");
	}
}
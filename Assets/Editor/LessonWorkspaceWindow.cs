using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.IO;
using System.Reflection;

public class LessonWorkspaceWindow : EditorWindow
{
    private string lessonsPath = "Assets/Lessons";

    [MenuItem("Tools/Lessons/Workspace")]
    public static void ShowWindow()
    {
        GetWindow<LessonWorkspaceWindow>("Lesson Workspace");
    }

    private void OnGUI()
    {
        GUILayout.Label("Lesson Workspace", EditorStyles.boldLabel);

        GUILayout.Space(10);

        if (!Directory.Exists(lessonsPath))
        {
            EditorGUILayout.HelpBox(
                $"Pasta não encontrada:\n{lessonsPath}",
                MessageType.Error
            );

            return;
        }

        string[] lessons = Directory.GetDirectories(lessonsPath);

        foreach (string lessonPath in lessons)
        {
            string lessonName = Path.GetFileName(lessonPath);

            if (GUILayout.Button(lessonName))
            {
                EnterLesson(lessonPath);
            }
        }
    }

    private void EnterLesson(string lessonPath)
    {

        OpenFolderInProjectWindow(lessonPath);
        string scenesPath = Path.Combine(lessonPath, "Scenes");

        if (!Directory.Exists(scenesPath))
        {
            Debug.LogWarning(
                $"A aula não possui uma pasta Scenes: {lessonPath}"
            );

            return;
        }

        string[] sceneFiles = Directory.GetFiles(
            scenesPath,
            "*.unity",
            SearchOption.AllDirectories
        );

        if (sceneFiles.Length == 0)
        {
            Debug.LogWarning(
                $"Nenhuma cena encontrada em: {scenesPath}"
            );

            return;
        }

        // Abre a primeira cena como cena principal
        EditorSceneManager.OpenScene(
            sceneFiles[0],
            OpenSceneMode.Single
        );

        // Abre as outras cenas de forma aditiva
        for (int i = 1; i < sceneFiles.Length; i++)
        {
            EditorSceneManager.OpenScene(
                sceneFiles[i],
                OpenSceneMode.Additive
            );
        }

        Debug.Log($"Entrou na aula: {Path.GetFileName(lessonPath)}");
    }

    private void OpenFolderInProjectWindow(string folderPath)
    {
        string assetPath = folderPath.Replace("\\", "/");

        Object folder = AssetDatabase.LoadAssetAtPath<Object>(assetPath);

        if (folder == null)
        {
            Debug.LogWarning($"Pasta não encontrada: {assetPath}");
            return;
        }

        // Garante que o Project Window esteja focado
        EditorUtility.FocusProjectWindow();

        // Obtém o tipo interno do ProjectBrowser
        System.Type projectBrowserType = System.Type.GetType(
            "UnityEditor.ProjectBrowser,UnityEditor"
        );

        // Obtém a última janela Project utilizada
        FieldInfo lastInteractedField =
            projectBrowserType.GetField(
                "s_LastInteractedProjectBrowser",
                BindingFlags.Static |
                BindingFlags.Public |
                BindingFlags.NonPublic
            );

        object projectBrowser = lastInteractedField.GetValue(null);

        // Obtém o método que realmente abre a pasta
        MethodInfo showFolderContents =
            projectBrowserType.GetMethod(
                "ShowFolderContents",
                BindingFlags.Instance |
                BindingFlags.NonPublic
            );

        // Entra na pasta
        showFolderContents.Invoke(
            projectBrowser,
            new object[]
            {
                folder.GetEntityId(),
                true
            }
        );
    }
}
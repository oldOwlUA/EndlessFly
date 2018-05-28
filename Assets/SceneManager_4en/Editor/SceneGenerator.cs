/*
 * этот скрипт предназначени для версий редактора от 5_5
 * 
*/
using System;
using UnityEditor;
using UnityEditor.SceneManagement;

using UnityEngine;
using UnityEngine.SceneManagement;
[InitializeOnLoad]
public class SceneGenerator
{
    static SceneGenerator()
    {
        EditorSceneManager.newSceneCreated += SceneCreated;
    }

    private static void SceneCreated(Scene scene, NewSceneSetup setup, NewSceneMode mode)
    {
        var camGO = Camera.main.transform;
        var lightGO = GameObject.Find("Directional Light").transform;

        var setupFolder = new GameObject("[SETUP]").transform;
        var lights = new GameObject("Lights").transform;

        lights.parent = setupFolder;
        lightGO.parent = lights;

        var cam = new GameObject("Cameras").transform;
        cam.parent = setupFolder;
        camGO.parent = cam;

        var world = new GameObject("[WORLD]").transform;
        new GameObject("Static").transform.parent = world;
        new GameObject("Dynamic").transform.parent = world;

        new GameObject("[UI]");

        Debug.Log("NEW SCENE CREATED");
    }
}


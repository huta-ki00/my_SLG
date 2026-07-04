using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class SceneStack : ScriptableObject
{
    private Stack<Scene> scenes = new Stack<Scene>();

    public Scene Pop()
    {
        return scenes.Pop();
    }

    public void Push(Scene scene)
    {
        scenes.Push(scene);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

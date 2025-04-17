using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

[XLua.LuaCallCSharp]
public class HelloLua : MonoBehaviour
{
    public float baseDamage = 50;

    // Start is called before the first frame update
    void Start()
    {
        LuaEnv lua = new LuaEnv();
        string luaPath = Application.dataPath + "/Scrpits/Lua/hello.lua.txt";
        string luacode = File.ReadAllText(luaPath);
        lua.DoString(luacode);
        lua.Global.Get<LuaFunction>("SayHello").Call();

        lua.Dispose();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

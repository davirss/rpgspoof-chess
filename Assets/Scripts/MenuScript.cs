using UnityEngine;
using UnityEditor;

public class MenuScript {

    private static int ROW_SIZE = 5;
    private static int COLUMN_SIZE = 5;
    
    [MenuItem("Tools/Generate Board")]
    public static void GenerateBoard() {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Cube");
        prefab.name = "Prefab";
        GameObject board = new GameObject();
        board.name = "Board";

        for(int i = 0; i < ROW_SIZE * COLUMN_SIZE; i++) {
            var column = i % COLUMN_SIZE;
            var row = i / ROW_SIZE;
            var tile = GameObject.Instantiate(prefab, new Vector3(column, 0, row), Quaternion.identity);
            tile.name = "Tile " + row + "/" + column;
            tile.transform.parent = board.transform;
        }
    } 
}
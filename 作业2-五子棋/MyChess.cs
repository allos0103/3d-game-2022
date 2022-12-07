using UnityEngine;
using System.Collections;
  
public class MyChess : MonoBehaviour {

    //四个锚点位置，用于计算棋子落点
    public GameObject LeftTop;
    public GameObject RightTop;
    public GameObject LeftBottom;
    public GameObject RightBottom;
    //主摄像机
    public Camera cam;
    //锚点在屏幕上的映射位置
    Vector3 LTPos;
    Vector3 RTPos;
    Vector3 LBPos;
    Vector3 RBPos;

    Vector3 PointPos;//当前点选的位置
    float gridWidth =1; //棋盘网格宽度
    float gridHeight=1; //棋盘网格高度
    float minGridDis; //网格宽和高中较小的一个

    int PointCoorx;
    int PointCoory;

    Vector2[,] chessPos; //存储棋盘上所有可以落子的位置
    int[,] chessState; //存储棋盘位置上的落子状态
    
    enum turn {black, white } ;
    turn chessTurn; //落子顺序
    public Texture2D white; //白棋子
    public Texture2D black; //黑棋子
    public Texture2D blackWin; //白子获胜提示图
    public Texture2D whiteWin; //黑子获胜提示图
    int winner = 0; //获胜方，1为黑子，-1为白子
    bool isPlaying = true; //是否处于对弈状态

    void Start () {
        chessPos = new Vector2[15, 15];
        chessState =new int[15,15];
        chessTurn = turn.black;
    }

    void Update () {

        //计算锚点位置
        LTPos = cam.WorldToScreenPoint(LeftTop.transform.position);
        RTPos = cam.WorldToScreenPoint(RightTop.transform.position);
        LBPos = cam.WorldToScreenPoint(LeftBottom.transform.position);
        RBPos = cam.WorldToScreenPoint(RightBottom.transform.position);
        //计算网格宽度
        gridWidth = (RTPos.x - LTPos.x) / 14;
        gridHeight = (LTPos.y - LBPos.y) / 14;
        minGridDis = gridWidth < gridHeight ? gridWidth : gridHeight;
        //计算落子点位置
        for (int i = 0; i < 15; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                chessPos[i, j] = new Vector2(LBPos.x + gridWidth * i, LBPos.y + gridHeight * j);
            }
        }
        //检测鼠标输入并确定落子状态
        if (isPlaying && Input.GetMouseButtonDown(0))
        {
            PointPos = Input.mousePosition;
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                { 
                    //找到最接近鼠标点击位置的落子点，如果空则落子
                    if (Dis(PointPos, chessPos[i, j]) < minGridDis / 2 && chessState[i,j]==0)
                    {
                        PointCoorx = i;
                        PointCoory = j;
                        //根据下棋顺序确定落子颜色
                        chessState[i, j] = chessTurn == turn.black ? 1 : -1;
                        //落子成功，更换下棋顺序
                        chessTurn = chessTurn == turn.black ? turn.white : turn.black; 
                    }
                }
            }


        //调用判断函数，确定是否有获胜方
        bool re = result();
        if (re==true){// 根据当前下棋方判断上一步是谁走的棋，判断输赢
            if (chessTurn == turn.white){
                Debug.Log("黑棋胜");
                winner = 1;
                isPlaying = false;
            }
            else{
                Debug.Log("白棋胜");
                winner = -1;
                isPlaying = false;
            }
        }
        }
        //按下空格重新开始游戏
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < 15; i++){
                for (int j = 0; j < 15; j++)
                {
                chessState[i, j] = 0;
                }
            }
            isPlaying = true;
            chessTurn = turn.black;
            winner = 0;
        } 
    }
    //计算平面距离函数
    float Dis(Vector3 mPos, Vector2 gridPos)
    {
        return Mathf.Sqrt(Mathf.Pow(mPos.x - gridPos.x, 2)+ Mathf.Pow(mPos.y - gridPos.y, 2));
    }

    void OnGUI()
    { 
        //绘制棋子
        for(int i=0;i<15;i++)
        {
            for (int j = 0; j < 15; j++)
            {
                if (chessState[i, j] == 1)
                {
                    GUI.DrawTexture(new Rect(chessPos[i,j].x-gridWidth/2, Screen.height-chessPos[i,j].y-gridHeight/2, gridWidth,gridHeight),black);
                }
                if (chessState[i, j] == -1)
                {
                    GUI.DrawTexture(new Rect(chessPos[i, j].x - gridWidth / 2, Screen.height - chessPos[i, j].y - gridHeight / 2, gridWidth, gridHeight), white);
                } 
            }
        }
        //根据获胜状态，弹出相应的胜利图片
        if (winner == 1)
            GUI.DrawTexture(new Rect(Screen.width * 0.25f, Screen.height * 0.25f, Screen.width * 0.5f, Screen.height * 0.25f), blackWin);
        if (winner == -1)
            GUI.DrawTexture(new Rect(Screen.width * 0.25f, Screen.height * 0.25f, Screen.width * 0.5f, Screen.height * 0.25f), whiteWin);

    }
    //检测是够获胜的函数，不含黑棋禁手检测
    bool result()
    {   int tx = PointCoorx;
        int ty = PointCoory;
        int state = chessState[tx,ty];
        int cnt = 1;
        // 从当前落子的位置开始，对4个方向上进行判断
        // 1 
        while(ty>0){
            ty--;
            if(chessState[tx,ty]!=state){
                ty++;
                break;// find the starting point tx,ty
            }
        }
        while(ty<14){
            ty++;
            if(chessState[tx,ty] == state){cnt++;}
            else{break;}
        }
        if(cnt>=5){
            return true;
        }
        else{
            cnt = 1;
            tx = PointCoorx;
            ty = PointCoory;
        }
        // 2 
        while(tx>0){
            tx--;
            if(chessState[tx,ty]!=state){
                tx++;
                break;// find the starting point tx,ty
            }
        }
        while(tx<14){
            tx++;
            if(chessState[tx,ty] == state){cnt++;}
            else{break;}
        }
        if(cnt>=5){
            return true;
        }
        else{
            cnt = 1;
            tx = PointCoorx;
            ty = PointCoory;
        }
        // 3 
        while(tx>0 && ty>0){
            tx--;
            ty--;
            if(chessState[tx,ty]!=state){
                tx++;
                ty++;
                break;// find the starting point tx,ty
            }
        }
        while(tx<14 && ty<14){
            tx++;
            ty++;
            if(chessState[tx,ty] == state){cnt++;}
            else{break;}
        }
        if(cnt>=5){
            return true;
        }
        else{
            cnt = 1;
            tx = PointCoorx;
            ty = PointCoory;
        }
        // 4
        while(tx>0 && ty<14){
            tx--;
            ty++;
            if(chessState[tx,ty]!=state){
                tx++;
                ty--;
                break;// find the starting point tx,ty
            }
        }
        while(tx<14 && ty>0){
            tx++;
            ty--;
            if(chessState[tx,ty] == state){cnt++;}
            else{break;}
        }
        if(cnt>=5){
            return true;
        }
        else{
            cnt = 1;
            tx = PointCoorx;
            ty = PointCoory;
        }
   
        return false;
    } 
}

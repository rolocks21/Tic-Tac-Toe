using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class PlayButton : MonoBehaviour
{
    public Button b0, b1, b2, b3, b4, b5, b6, b7, b8, restart, sign;
    public TMP_Text t0, t1, t2, t3, t4, t5, t6, t7, t8, te;
    bool isPlayer1 = true;
    int[] mainBoard = new int[9];

    void Start()
    {
        restart.gameObject.SetActive(false);
        sign.gameObject.SetActive(false);
    }
    public string check() {
        if (isPlayer1)
        {
            return "O";
        }
        return "X";
    }
    public int checkint()
    {
        if (isPlayer1)
        {
            return 1;
        }
        return -1;
    }
    public void bu0()
    {
        t0.text = check();
        mainBoard[0] = checkint();
        isPlayer1 = !isPlayer1;
        b0.enabled = false;
    }
    public void bu1()
    {
        t1.text = check();
        mainBoard[1] = checkint();
        isPlayer1 = !isPlayer1;
        b1.enabled = false;
    }
    public void bu2()
    {
        t2.text = check();
        mainBoard[2] = checkint();
        isPlayer1 = !isPlayer1;
        b2.enabled = false;
    }
    public void bu3()
    {
        t3.text = check();
        mainBoard[3] = checkint();
        isPlayer1 = !isPlayer1;
        b3.enabled = false;
    }
    public void bu4()
    {
        t4.text = check();
        mainBoard[4] = checkint();
        isPlayer1 = !isPlayer1;
        b4.enabled = false;
    }
    public void bu5()
    {
        t5.text = check();
        mainBoard[5] = checkint();
        isPlayer1 = !isPlayer1;
        b5.enabled = false;
    }
    public void bu6()
    {
        t6.text = check();
        mainBoard[6] = checkint();
        isPlayer1 = !isPlayer1;
        b6.enabled = false;
        eval(mainBoard);
    }
    public void bu7()
    {
        t7.text = check();
        mainBoard[7] = checkint();
        isPlayer1 = !isPlayer1;
        b7.enabled = false;
    }
    public void bu8()
    {
        t8.text = check();
        mainBoard[8] = checkint();
        isPlayer1 = !isPlayer1;
        b8.enabled = false;
    }
    public int eval(int [] board)
    {
        for (int i = 0; i <= 2; i++)
        {
            if (board[3 * i] == board[3 * i + 1] && board[3 * i] == board[3 * i + 2] && board[3 * i] != 0)
            {
                return board[3 * i];
            }
            if (board[i] == board[i + 3] && board[i] == board[i + 6] && board[i] != 0)
            {
                return board[i];
            }
            if (board[0] == board[4] && board[0] == board[8] && board[0] != 0)
            {
                return board[0];
            }
            if (board[2] == board[4] && board[2] == board[6] && board[2] != 0)
            {
                return board[2];
            }
        }
        return 0;
    }
                                            
    private ArrayList moves(int[] board)
    {
        ArrayList Moves = new ArrayList();
        for (int i = 0; i < 9; i++)
        {
            if (board[i] == 0)
            {
                Moves.Add(i);
            }
        }
        return Moves;
    }

    private int alphabeta(int[] node, int depth, int a, int b, int player)
    {
        if ((moves(node).Count == 0 || eval(node) != 0 || depth == 9))
        {
            return eval(node);
        }
        if (player == 1)
        {
            int value = -3;
            foreach (int i in moves(node))
            {
                int[] copyBoard = new int[9];
                node.CopyTo(copyBoard, 0);
                copyBoard[i] = player;
                value = Math.Max(value, alphabeta(copyBoard, depth + 1, a, b, player * -1));
                if (value > b) {
                    break;
                }
                a = Math.Max(a, value);
            }
            return value;
        }

        else
        {
            int value = 3;
            foreach (int i in moves(node))
            {
                int[] copyBoard = new int[9];
                node.CopyTo(copyBoard, 0);
                copyBoard[i] = player;
                value = Math.Min(value, alphabeta(copyBoard, depth + 1, a, b, player * -1));
                if (value < a) {
                    break;
                }
                b = Math.Min(b, value);
            }
            return value;
        }
    }
    public int abGetBestMove(int[] board, int player)
    {
        if(moves(board).Count == 0)
        {
            return 9;
        }
        int t_score = -1;
        int best_move = (int)(moves(board))[0];
        foreach (int i in moves(board))
        {
            int[] copyBoard = new int[9];
            board.CopyTo(copyBoard, 0);
            copyBoard[i] = player;
            int score = alphabeta(copyBoard, 0, -2, 2, player * -1) * player;
            if (score > t_score)
            {
                best_move = i;
                t_score = score;
            }
        }
        return best_move;
    }
    public void aiMove(int bestMove)
    {
        switch (bestMove)
        {
            case 0:
                bu0();
                break;
            case 1:
                bu1();
                break;
            case 2:
                bu2();
                break;
            case 3:
                bu3();
                break;
            case 4:
                bu4();
                break;
            case 5:
                bu5();
                break;
            case 6:
                bu6();
                break;
            case 7:
                bu7();
                break;
            case 8:
                bu8();
                break;
            case 9:
                Tie();
                break;
        }
    }
    public void win()
    {
        sign.gameObject.SetActive(true);
        te.text = "No Way Bud (W)";
        restart.gameObject.SetActive(true);
    }
    public void Tie()
    {
        sign.gameObject.SetActive(true);
        te.text = "Nice try (tie)";
        restart.gameObject.SetActive(true);
    }
    public void lose()
    {
        sign.gameObject.SetActive(true);
        te.text = "Get Better (L)";
        restart.gameObject.SetActive(true);
    }
    public void restarts()
    {
        SceneManager.LoadScene("SampleScene");
    }
    void Update()
    {
        int i = -1;
        if (isPlayer1 == false)
        {
            aiMove(abGetBestMove(mainBoard, i));
            i= i * -1;
        }
        if(eval(mainBoard)!= 0)
        {
            switch(eval(mainBoard)){
                case -1:
                    lose();
                    break;
                case 1:
                    win();
                    break;
            }
        }
    }
}
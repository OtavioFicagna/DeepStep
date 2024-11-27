using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private BoardManager m_Board;
    private Vector2Int m_CellPosition;
    private bool m_IsGameOver;

    public float MoveSpeed = 5.0f;

    private bool m_IsMoving;
    private Vector3 m_MoveTarget;
    private Animator m_Animator;
    private AudioSource audioSource;
    public AudioClip footstep1;

    public Vector2Int Cell
    {
        get { return m_CellPosition; }
    }

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Init()
    {
        m_IsMoving = false;
        m_IsGameOver = false;
    }

    public void Spawn(BoardManager boardManager, Vector2Int cell)
    {
        m_Board = boardManager;
        MoveTo(cell, true);
    }

    public void MoveTo(Vector2Int cell, bool immediate)
    {
        m_CellPosition = cell;

        if (immediate)
        {
            m_IsMoving = false;
            transform.position = m_Board.CellToWorld(m_CellPosition);
        }
        else
        {
            m_IsMoving = true;
            m_MoveTarget = m_Board.CellToWorld(m_CellPosition);
        }

        m_Animator.SetBool("Moving", m_IsMoving);
        PlayWalkingSound();
    }

    void Update()
    {
        if (m_IsGameOver)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
            }

            if (Keyboard.current.enterKey.wasPressedThisFrame)
            {
                GameManager.Instance.StartNewGame();
            }

            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                GameManager.Instance.ReturnMenu();
            }

            return;
        }

        if (m_IsMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_MoveTarget, MoveSpeed * Time.deltaTime);

            if (transform.position == m_MoveTarget)
            {
                m_IsMoving = false;
                m_Animator.SetBool("Moving", false);

                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }

                var cellData = m_Board.GetCellData(m_CellPosition);
                if (cellData.ContainedObject != null)
                    cellData.ContainedObject.PlayerEntered();
            }
            else
            {
                PlayWalkingSound();
            }

            return;
        }

        Vector2Int newCellTarget = m_CellPosition;
        bool hasMoved = false;

        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            newCellTarget.y += 1;
            hasMoved = true;
        }
        else if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            newCellTarget.y -= 1;
            hasMoved = true;
        }
        else if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            newCellTarget.x += 1;
            hasMoved = true;
        }
        else if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            newCellTarget.x -= 1;
            hasMoved = true;
        }
        else if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            GameManager.Instance.ReturnMenu();
        }

        if (hasMoved)
        {
            BoardManager.CellData cellData = m_Board.GetCellData(newCellTarget);

            if (cellData != null && cellData.Passable)
            {
                GameManager.Instance.TurnManager.Tick();

                if (cellData.ContainedObject == null)
                {
                    MoveTo(newCellTarget, false);
                }
                else if (cellData.ContainedObject.PlayerWantsToEnter())
                {
                    MoveTo(newCellTarget, false);
                }
            }
        }
    }

    void PlayWalkingSound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = footstep1;
            audioSource.Play();
        }
    }

    public void GameOver()
    {
        audioSource.Pause();
        m_IsGameOver = true;
    }

    public void HasAttack()
    {
        m_Animator.SetTrigger("Attack");
    }

}
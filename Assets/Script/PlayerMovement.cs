using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
#region Singleton class: PlayerMovement

public static PlayerMovement Instance;

void Awake()
{
    if(Instance == null)
       Instance = this;
}

#endregion
[SerializeField] CircleCollider2D RedBallCollider;
[SerializeField] CircleCollider2D BlueBallCollider;
    [SerializeField] float speed;
    [SerializeField] float rotatioSpeed;

    Rigidbody2D rb;
    Vector3 startPosition;
    Camera cam;
    float touchPosX;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        MoveUp();
    }

    // Update is called once per frame
    void Update()
    {
      if(!GameManager.Instance.isGameover)
      {
       
          
          if(Input.GetMouseButtonDown(0))
           touchPosX = cam.ScreenToWorldPoint(Input.mousePosition).x;
         if(Input.GetMouseButton(0))
         {
            
            if(touchPosX>0.01f)
              RotateRight();
            else
            RotateLeft();
         }
         else
         rb.angularVelocity = 0f;


          #if UNITY_EDITOR
         if(Input.GetKey(KeyCode.LeftArrow))
        
            RotateLeft();
        
        else if(Input.GetKey(KeyCode.RightArrow))
        
            RotateRight();
        

        if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        rb.angularVelocity = 0f;
        #endif
      }


       
    }

    void MoveUp()
    {
        rb.velocity = Vector2.up * speed;

    }

    void RotateLeft()
    {
        rb.angularVelocity = rotatioSpeed;
    }
    void RotateRight()
    {
        rb.angularVelocity = -rotatioSpeed;
    }

    public void Restart()
    {
        RedBallCollider.enabled = false;
        BlueBallCollider.enabled = false;
        rb.angularVelocity = 0f;
        rb.velocity = Vector2.zero;

        transform
        .DORotate(Vector3.zero,1f)
        .SetDelay(1f)
        .SetEase(Ease.InOutBack);

        transform
        .DOMove(startPosition,1f)
        .SetDelay(1f)
         .SetEase(Ease.OutFlash)

         .OnComplete(()=>{
            RedBallCollider.enabled = true;
            BlueBallCollider.enabled = true;

            GameManager.Instance.isGameover = false;

            MoveUp();
         });   
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("LevelEnd"))
        {
            Destroy(other.gameObject);

            int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
            if(currentLevelIndex <  SceneManager.sceneCount)
            SceneManager.LoadSceneAsync(currentLevelIndex);
        }
    }
}

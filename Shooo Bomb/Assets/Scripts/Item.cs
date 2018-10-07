using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

        public GameObject Fast_Item; //Fast_Item의 오브젝트
        public GameObject Big_Player_Item; //Big_Item의 오브젝트
        public GameObject Blind_Item; //Blind_Item의 오브젝트
        public GameObject Blind_Wall; //Blind_Wall의 오브젝트

        private Rigidbody rb; //Player의 Rigidbody를 담기 위한 변수
        private Transform tr; //Player의 Transform을 담기 위한 변수
        private float fast_timer; //fast 아이템을 먹은 후에 시간을 측정하기 위한 변수
        private int end_timer = 3; // 아이템의 적용시간을 나타내는 변수

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            tr = GetComponent<Transform>();
            Blind_Wall.SetActive(false); // Blind_Wall 비활성화
        }

        void FixedUpdate()
        {
            //속도를 빠르게 만들기
            if (Fast_Item.activeSelf == false)
            {
                fast_timer += Time.deltaTime;

                if (fast_timer < end_timer)
                {
                    float moveHorizontal = Input.GetAxis("Horizontal");
                    float moveVertical = Input.GetAxis("Vertical");

                    Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
                    
                    rb.AddForce(movement * 20);
                }
            }
        }

        //오브젝트가 충돌했을 때 -> 아이템을 만났을 때 [장애물을 만났을 때도 이 함수를 사용할 경우에는, 다음에 나올 switch문에 default로 빠져 나갈 수 있다.]
        void OnTriggerEnter(Collider other)
        {
            string Item_name = other.gameObject.name; //초기에 if문으로 작성했지만 switch문으로 바꾸기 위해서 변수 지정

            other.gameObject.SetActive(false); //아이템 삭제

            switch (Item_name)
            {
                case "item1(Speed)":
                    fast_timer = 0;
                    break;
                case "item1(bigger)":
                    Eat_Big_Item();
                    Invoke("Back_Big_Item", 3);
                    break;
                case "item1(blind)":
                    Blind_Wall.SetActive(true);
                    Invoke("Eat_Blind_Item", 3);
                    break;
                default:
                    break;
            }
        }

        void Eat_Big_Item()
        {
            tr.position = new Vector3(tr.position.x, tr.position.y * 2, tr.position.z);
            tr.localScale += tr.localScale;
        }

        void Back_Big_Item()
        {
            tr.position = new Vector3(tr.position.x, tr.position.y / 2, tr.position.z);
            tr.localScale -= tr.localScale / 2;
        }

        void Eat_Blind_Item()
        {
            Blind_Wall.SetActive(false);
        }
}

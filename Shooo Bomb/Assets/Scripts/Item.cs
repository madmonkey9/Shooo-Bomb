using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    //sboseong(서보성) 제작

        public GameObject Fast_Item; //Fast_Item의 오브젝트
        public GameObject Big_Player_Item; //Big_Item의 오브젝트
        public GameObject Blind_Item; //Blind_Item의 오브젝트
        public GameObject Blind_Wall; //Blind_Wall의 오브젝트

        private Rigidbody rb; //Player의 Rigidbody를 담기 위한 변수
        private Transform tr; //Player의 Transform을 담기 위한 변수
                              /*    private int Big_Player_use_cnt; // 아이템을 사용한 횟수
                                  private int Big_Player_cnt; //아이템을 먹은 횟수 */
        private

        void Start()
        {
            /*       Big_Player_cnt = 0;
                   Big_Player_use_cnt = 0; */
            rb = GetComponent<Rigidbody>();
            tr = GetComponent<Transform>();
            Blind_Wall.SetActive(false); // Blind_Wall 비활성화
        }

        void FixedUpdate()
        {
            //속도를 빠르게 만들기
            if (Fast_Item.activeSelf == false)
            {
                float moveHorizontal = Input.GetAxis("Horizontal");
                float moveVertical = Input.GetAxis("Vertical");

                Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

                rb.AddForce(movement * 75);
            }

            /*      //공의 크기가 2배로 증가
                    //(카운트를 사용하여 계속 커지는 것을 방지)
                    if ((Big_Player_cnt > Big_Player_use_cnt) && (Big_Player_Item.activeSelf == false))
                    {
                        tr.position = new Vector3(tr.position.x, tr.position.y * 2, tr.position.z);
                        tr.localScale += tr.localScale;

                        ++Big_Player_use_cnt; 
                    } */

        }

        //오브젝트가 충돌했을 때 -> 아이템을 만났을 때 [장애물을 만났을 때도 이 함수를 사용할 경우에는, 다음에 나올 switch문에 default로 빠져 나갈 수 있다.]
        void OnTriggerEnter(Collider other)
        {
            string Item_tag = other.gameObject.tag; //초기에 if문으로 작성했지만 switch문으로 바꾸기 위해서 변수 지정

            other.gameObject.SetActive(false); //아이템 삭제

            switch (Item_tag)
            {
                case "Fast_Item":
                    break;
                case "Big_Item":
                    Eat_Big_Item();
                    Invoke("Back_Big_Item", 3);
                    break;
                case "Blind_Item":
                    Blind_Wall.SetActive(true);
                    Invoke("Eat_Blind_Item", 3);
                    break;
                default:
                    break;
            }

            /*        //Fast_Item을 먹었을 때
                    if (other.gameObject.CompareTag("Fast_Item"))
                    {
                        other.gameObject.SetActive(false);

                    }

                    //Big_Item을 먹었을 때
                    if (other.gameObject.CompareTag("Big_Item"))
                    {
                        other.gameObject.SetActive(false);
                        ++Big_Player_cnt;
                    }

                    //Blind_Item을 먹었을 때
                    if (other.gameObject.CompareTag("Blind_Item"))
                    {
                        other.gameObject.SetActive(false);
                    }*/
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

using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.Scripts.Controller;
using UnityEngine;

namespace TWOPROLIB.Scripts.Interactables
{
    public class PickupCoin : Interactable
    {
        public RuntimeAnimatorController pickupAnim;
        Animator anim;
        public int coin;
        GameObject pickItemObject;

        public string itemname;
        public Vector3 dropOffset;
        public Vector3 dropVelocityPower;

        void Start()
        {
            Spawn(1000);
        }

        /// <summary>
        /// 드롭 아이템
        /// </summary>
        /// <param name="item"></param>
        public void Dropping(int coin, Vector3 location, Vector3 eulerAngles)
        {
            NonPickup = true;
            transform.eulerAngles = eulerAngles;
            transform.position = location;  // + dropOffset;  // new Vector3(0, 1, 0);

            Dropping(coin);
        }

        public void Dropping(int coin)
        {
            transform.position = transform.position + dropOffset;

            NonPickup = true;
            GetComponent<BoxCollider>().isTrigger = true;
            this.coin = coin;
            CreateObject();
            anim.SetBool("Drop", true);

            Invoke("EnableIsTrigger", 0.8f);

            Rigidbody rig = GetComponent<Rigidbody>();
            rig.useGravity = true;
            rig.AddForce((gameObject.transform.forward * dropVelocityPower.z) + (Vector3.up * dropVelocityPower.y));
            //rig.velocity = new Vector3(0, 2, 2);
        }

        protected bool NonPickup = false;
        protected void EnableIsTrigger()
        {
            GetComponent<BoxCollider>().isTrigger = true;
        }


        /// <summary>
        /// 제자리에서 픽업아이템 스폰
        /// </summary>
        /// <param name="item"></param>
        public void Spawn(int coin)
        {
            NonPickup = false;
            this.coin = coin;
            CreateObject();
            anim.SetBool("Drop", false);
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<BoxCollider>().enabled = true;
        }

        protected void CreateObject()
        {
            string str = "ObjectSlugs/Entity/Coin";
            pickItemObject = (GameObject)Instantiate(Resources.Load<GameObject>(str), transform.position, transform.rotation);
            //pickItemObject.transform.Rotate(Vector3.right * 90);
            pickItemObject.transform.SetParent(transform);
            anim = pickItemObject.GetComponent<Animator>();
            anim.runtimeAnimatorController = pickupAnim;
        }

        private void OnTriggerExit(Collider other)
        {
            //Inventory inv = other.GetComponent<Inventory>();
            //if (inv != null && NonPickup == false)
            //{

            //}
        }

        protected void OnTriggerEnter(Collider other)
        {
            StateController state = other.GetComponent<StateController>();
            //if (state != null && state.tag.Equals("player"))
            //{
            //    ChangeDataEventHandle.Instance.ChangeData(ChangeDataTypes.Coin, player.coin, player.coin + coin);
            //    player.coin += coin;
            //    GetComponent<BoxCollider>().enabled = false;
            //    anim.SetBool("Destroy", true);
            //    Destroy(this.gameObject, 1.0f);
            //    DebugX.Log("Pickup Item");

            //}
            //else if (other.tag.Equals("Ground") && anim != null && anim.GetBool("Drop"))
            //{
            //    NonPickup = false;
            //    GetComponent<Rigidbody>().useGravity = false;
            //    GetComponent<Rigidbody>().velocity = Vector3.zero;
            //    transform.position = new Vector3(transform.position.x, other.transform.position.y + 0.5f, transform.position.z);
            //    anim.SetBool("Drop", false);

            //}
        }


    }
}

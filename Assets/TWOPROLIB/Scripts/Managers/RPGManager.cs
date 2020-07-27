using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임에 필요한 각종 정보 등록
/// </summary>
namespace TWOPROLIB.Scripts.Managers
{
    /// <summary>
    /// RPG 개념의 게임 생성 시 바인딩 해야함
    /// </summary>
    public class RPGManager : MonoBehaviour
    {
        public bool isMultiTargetPlayer = false;
        public GameObject player;
        public List<GameObject> players;


        public static RPGManager Instance = null;
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    /// <summary>
    /// 인밴토리에 사용되는 슬롯 파입
    /// </summary>
    public enum SlotTypes
    {
        InventoryWithItem,
        ShotSlotWithItem,
        ShotSlotWithSkill

    }

    /// <summary>
    /// 드랍된 아이템의 종류
    /// </summary>
    public enum PickupTypes
    {
        PICKUP,
        DROPnPICKUP,
        DESTORYnPICKUP,
    }

    /// <summary>
    /// 물체의 종류
    /// </summary>
    public enum EntityTypes
    {
        /// <summary>
        /// 코인(화폐)
        /// </summary>
        COIN,
        /// <summary>
        /// 아이템
        /// </summary>
        ITEM,
        /// <summary>
        /// 속성 
        /// </summary>
        Attribute,
        /// <summary>
        /// 스킬
        /// </summary>
        SKILL,
        /// <summary>
        /// 플레이어
        /// </summary>
        PLAYER,
        /// <summary>
        /// 적
        /// </summary>
        ENEMY,
        /// <summary>
        /// 일반 물체
        /// </summary>
        Thing,
        /// <summary>
        /// 발사체
        /// </summary>
        Projectile,
    }

    /// <summary>
    /// 아이템 종류
    /// </summary>
    public enum ItemTypes
    {
        EQUIPMENT,
        WEAPON,
        SCROLL,
        POTION,
        CHEST,
    }

    /// <summary>
    /// 게임 세계의 상태
    /// </summary>
    public enum WorldStatus
    {
        None,
        Teleport,
        Dialogue,

    }

    /// <summary>
    /// 게임 화면 모드(2D, 3D)
    /// </summary>
    public enum GameDisplayMode
    {
        /// <summary>
        /// 일반 2D
        /// </summary>
        Mode_2D,
        /// <summary>
        /// 일반 3D
        /// </summary>
        Mode_3D,
        /// <summary>
        /// Nav기능 사용
        /// </summary>
        Mode_3D_MeashVan,
    }

    /// <summary>
    /// 방향
    /// </summary>
    public enum Direction
    {
        /// <summary>
        /// 직진
        /// </summary>
        Forward,
        /// <summary>
        /// 후진
        /// </summary>
        Backward,
        Left,
        Right,
    }

    /// <summary>
    /// 입력 방식
    /// </summary>
    public enum InputType
    {
        /// <summary>
        /// 가상 조이스틱
        /// </summary>
        VirtualJoystick,
        /// <summary>
        /// 조이스틱
        /// </summary>
        Joystick,
        /// <summary>
        /// 키(마우스)
        /// </summary>
        Key
    }

    /// <summary>
    /// 자동 종료 타입
    /// </summary>
    public enum DestroyType
    {
        /// <summary>
        /// 시간
        /// </summary>
        Time,
        /// <summary>
        /// 거리
        /// </summary>
        Distance
    }


}

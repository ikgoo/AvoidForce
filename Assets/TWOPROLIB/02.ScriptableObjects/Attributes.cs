using TWOPROLIB.ScriptableObjects.Entitys;
using UnityEngine;

//////////https://www.youtube.com/watch?v=6d7pmRE0T6c&t=12s

//https://www.youtube.com/watch?v=6d7pmRE0T6c&t=12s
//https://www.youtube.com/watch?v=0WPBm9nGHdE&t=443s
//https://www.youtube.com/watch?v=M7qxcCyZlJ4&t=272s
//https://www.youtube.com/watch?v=qP6BbUxFuRI&list=PLS6sInD7ThM2VO5pGgImyfjl5lNApip-7&index=29
//https://www.youtube.com/watch?v=B8tmUAOE278
//https://www.youtube.com/watch?v=x00IewDdrUA
//https://www.youtube.com/watch?v=madv_VkYQno&list=PL4vbr3u7UKWp0iM1WIfRjCDTI03u43Zfu&index=26
//https://www.youtube.com/watch?v=4JewzU_phTM
//https://www.youtube.com/watch?v=SNwPq01yHds
//https://www.youtube.com/watch?v=iXNwWpG7EhM.
namespace TWOPROLIB.ScriptableObjects.EntitysAttributes
{
    [CreateAssetMenu(menuName = "Stats/Attributes")]
    public class Attributes : Entity
    {
        public Attributes()
        {
            EntityType = Scripts.Managers.EntityTypes.Attribute;
        }
    }
}

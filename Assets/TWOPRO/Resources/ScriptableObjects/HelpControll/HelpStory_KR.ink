VAR HelpObjName = ""
VAR BeforePlay = 0
VAR AfterPlay = 0       // [Auto]에만 유용함

-> Help01

========== Help01 ==========
~HelpObjName = "Center"
~BeforePlay = 1

AVOID FORCE 세계에 오신걸 환영 합니다.
튜토리얼을 시작하겠습니다.
*   [NEXT]

중앙 아래에 있는 동그란 물체가 플레이이에요
좌우로 조정하여 전방에서 오는 불꽃을 피하면 되는 게임이에요.
**   [NEXT]

정확히 말하자면 아슬아슬하게 피하여 높은 점수를 추구하는 것입니다.
***   [NEXT]

전방에 불꽃이 랜덤하게 발사되어 플레이어 방향으로 내려와요
조이스틱으로 좌우로 피할 수 있습니다.
****   [NEXT]

플레이어 정면과 충돌하면 생명력이 줄어듭니다.
*****   [NEXT]

점수는 일정시간마다 1점씩 추가됩니다.
******   [NEXT]

플레이어의 좌우 표시부분에 불꽃을 충동하면 콤보 카운트가 올라가고
추가 점수가 지급 됩니다.
*******   [NEXT]

~AfterPlay = 1
5번 콤보 시 특수 능력이 발동 됩니다.
********   [EXIT] -> Help02

========== Help02 ==========
~HelpObjName = "Left"
~BeforePlay = 0
~AfterPlay = 1

푸른색 불꽃을 5회 콤보 시 실드가 발동됩니다.
충돌 1회를 무효화 합니다.
* [EXIT] -> Help03

========== Help03 ==========
녹색 불꽃을 5회 콤보 시 보이는 불꽃을 단일 색으로 바꿈니다
* [EXIT] -> Help04

========== Help04 ==========
붉은 불꽃은 5회 콤보 이후부터 콤보마다 추가 접수가 누적 지급 됩니다.
* [EXIT] -> Help05

========== Help05 ==========
~BeforePlay = 0
~AfterPlay = 0
핑크 불꽃은 5회 콤보 마다 불꽃의 속도를 조금씩 느리게 합니다.
* [NEXT] -> Help06

========== Help06 ==========
~HelpObjName = "Center"
게임의 최종 목적은 높은 점수를 얻어 상위 랭크에 등급하는 것입니다.
높은 점수를 얻어 탑 랭커가 되어 보세요.
* [NEXT]

튜토리얼은 끝났습니다.
바로 시작 합니다.
** [NEXT]

-> END    
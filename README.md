# BattleDungeon-TeamProject
배틀모드가 추가된 콘솔던전게임
## 🎬프로젝트 소개
코드작업에 처음 접하는 사람들끼리 모여 만든 미니 콘솔게임입니다.

## 🕛개발 기간
23.08.28(월) - 23.09.01(금)

### 🧑‍🤝‍🧑멤버 구성
ㆍ팀장 손병의 - 필수기능(게임 시작화면,상태창,전투) / 선택기능(로그인,직업선택,콘솔꾸미기)

ㆍ팀원 임현진 - 필수기능(전투기능 보완-랜덤등장요소 추가) / 선택기능(아이템,회복 아이템)

ㆍ팀원 김준범 - 필수기능(전투기능 보완) / 선택기능(레벨업)

ㆍ팀원 진우성 - 필수기능(전투기능 보완) / 선택기능(치명타)

ㆍ팀원 이성권 - 필수기능 참여

### 📗팀 노션 링크
<https://www.notion.so/02-A2-6a6dc3241b7c4f9d844bf9d2d9cc6fa0>

### ⚙️개발 환경
- 'C#'
- 'Visual studio 2022'
- 'github'

##  📲주요 기능
#### ID생성하기
      : 초기화면에서 1을 입력하면 ID 및 PW 기입하는 화면 출력

#### 로그인하기
      : ID생성하기 화면에서 생성한 ID와 PW를 그대로 입력(실패 시 초기화면 출력)

#### 직업 선택하기
      : 로그인에 성공하면 직업을 선택할 수 있는 화면 출력(1~4를 입력해서 선택 / 그 외 숫자 입력 시 "잘못된 입력입니다"라고 출력)
      
#### 상태창 확인
      : 로그인하기에서 만든 닉네임과 직업선택화면에서 고른 직업의 이름,체력,공격력,방어력 반영

#### 인벤토리
      : 인벤토리에 있는 아이템 앞의 숫자를 입력 시 [E]표시가 아이템 이름 왼쪽에 출력되며 상태창의 능력치에 반영됨 

#### 회복아이템
      : 사용 시 전투에서 깎인 플레이어의 HP회복 반영

#### 전투
      - 전투 화면에서 랜덤한 마릿수의 몬스터 등장
      - 공격하기(1)을 눌러 몬스터와 전투 시작
      - 등장한 몬스터의 번호를 입력해서 그 몬스터와 전투
         : 플레이어의 선제공격턴 / 몬스터의 공격턴으로 구성
      - 몬스터의 HP가 전부 소모된 상태라면 몬스터의 이름이 흑백으로 변하고 Dead 상태 출력
      - Dead상태의 몬스터 공격 시 ("이미 죽은 몬스터를 선택하셨습니다.") 출력

#### 전투결과
      - 몬스터의 HP를 먼저 깎으면 플레이어의 승리(보상페이즈로 이동)
      - 플레이어의 HP가 먼저 깎이면 패배
#### 보상 페이즈
      - 랜덤한 아이템과 골드를 획득(플레이어의 상태창에 반영)

##  😥기능 구현 아쉬웠던점
#### 로그인 하기
      - ID를 생성하고 게임에 진입하고 나서 초기화면으로 돌아갔을때 
        생성했던 ID로 로그인이 되지 않았음

#### 상태창 반영
      - 아이템을 장착하면 상태창에 추가 공격력()이 반영되었지만,
        플레이어의 총스탯에 합산되어 반영이 되지  않았음

#### 인벤토리
      - 전투에서 획득한 보상 아이템이 인벤토리에 반영이 되지 않았음

#### 회복아이템
      - 회복아이템을 사용했을 때 상태창에 반영이 되는 기능은 잘 작동했지만,
        회복아이템 사용 당시의 화면의 HP에는 반영이 되지 않았음

#### 전투
      - 몬스터와의 전투 시 내가 선택한 몬스터와 전투를 해야하는데
        등장한 모든 몬스터와 전투를 하는 상황이 발생함

#### UI
      - 실제 반영되는 숫자가 상태창 등에 반영되었을 때, 만들어둔 UI의 정렬이 제대로 되지않았음

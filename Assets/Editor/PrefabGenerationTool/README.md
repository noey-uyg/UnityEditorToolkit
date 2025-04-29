# Prefab Generation Tool
## 개요
이 도구는 Unity 프로젝트에서 프리팹을 자동으로 생성하고, 생성된 프리팹에 맞는 메뉴 항목을 추가하는 도구입니다. 

PrefabChangeWatcher를 통해 프리팹이 추가, 삭제, 이동될 때마다 자동으로 생성 코드를 갱신할 수 있으며, 

수동으로 메뉴에서 RefreshPrefabs를 실행하여 프리팹 목록을 업데이트할 수도 있습니다.

## 주요 기능
1. 자동 모드 : PrefabAutoMode가 활성화되면, 프리팹이 추가, 삭제, 이동될 때마다 자동으로 CreatePrefab.cs 파일을 갱신합니다.

2. 수동 모드 : 수동으로 Assets/RefreshPrefabs 메뉴를 통해 프리팹 목록을 갱신할 수 있습니다.

3. 프리팹 생성 : 생성된 CreatePrefab.cs 스크립트를 통해 각 프리팹에 대한 메뉴 항목을 자동으로 추가하고, 이를 통해 원하는 프리팹을 쉽게 생성할 수 있습니다.

4. 중복 처리 : 동일한 이름을 가진 프리팹이 있을 경우, 메뉴 항목 이름 뒤에 숫자를 추가하여 중복을 피합니다.

5. 특수 기호 처리 : 메서드 이름에 포함될 수 없는 특수 기호를 안전한 문자로 변환하여 메뉴 항목으로 사용할 수 있도록 처리합니다.
   
## 사용 방법
1. 자동 모드 활성화 : Tools > Prefab Settings에서 Auto Mode 토글을 활성화합니다.
활성화되면 프리팹의 추가, 삭제, 이동 시 자동으로 CreatePrefab.cs 파일이 갱신됩니다.

2. 수동 모드 : Assets > RefreshPrefabs 메뉴를 통해 언제든지 프리팹 목록을 수동으로 갱신할 수 있습니다.

3. 프리팹 생성 : Hierachy > Prefabs > [폴더 이름] 메뉴에서 원하는 프리팹을 선택하여 씬에 프리팹을 인스턴스화할 수 있습니다.

## 구현 세부 사항
### PrefabSettings.cs
- PrefabSettings 윈도우를 통해 Auto Mode를 설정할 수 있습니다.
- Auto Mode가 활성화되면 프리팹의 변경 사항이 자동으로 반영됩니다.

### AutoGenerateCreatePrefabCode.cs
- CreatePrefab.cs 파일을 자동으로 생성하고, 각 프리팹에 맞는 메뉴 항목을 추가합니다.
- 메뉴 항목 이름에 사용할 수 없는 특수 기호나 중복된 이름을 처리합니다.

### PrefabChangeWatcher.cs
- 프리팹 추가, 삭제, 이동 시 자동으로 CreatePrefab.cs 파일을 갱신하는 기능을 담당합니다.
- PrefabAutoMode 설정에 따라 자동 모드가 활성화되면 변경 사항을 감지하여 파일을 갱신합니다.

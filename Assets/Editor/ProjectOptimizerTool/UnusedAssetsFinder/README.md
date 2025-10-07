# Align Tool
## 개요
이 도구는 Unity 프로젝트 내에서 현재 빌드나 활성화된 씬에서 사용되지 않는 에셋을 탐지하고 관리할 수 있는 유틸리티입니다.

프로젝트 용량 최적화, 빌드 크기 감소, 불필요한 리소스 정리에 효과적이며, 사용자는 UI 상에서 탐지된 에셋을 바로 확인하거나 삭제할 수 있습니다.

## 주요 기능
- 미사용 에셋 탐지 : 현재 빌드에 포함된 씬을 기준으로 사용되지 않는 에셋을 자동 검색합니다.
- 확장자별 필터링 : 특정 파일 확장자를 제외하고 검색할 수 있습니다.
- 씬 종속성 확인 : 현재 활성화된 씬에서 사용 중인 모든 에셋 목록을 확인할 수 있습니다.
- 강조 & 복사 기능 : 탐지된 에셋의 위치를 Hierarchy 또는 Project 뷰에서 즉시 강조하거나 경로를 복사할 수 있습니다.
- 안전한 삭제 : 개별 또는 일괄 삭제 기능 제공 - 잘못 삭제를 방지하기 위해 확인 대화창 표시.
- 동적 확장자 관리 : 프로젝트 내 존재하는 모든 확장자를 자동으로 탐색하여 UI에서 체크박스로 제어 가능.

## 사용 방법
1. Unused Assets Finder 툴 윈도우를 엽니다.
2. 현재 씬 종속성 확인
    - "Current Scene Dependencies" 섹션에서 Refresh 버튼을 눌러 현재 씬에 사용중인 에셋 목록을 확인합니다.
    - 각 항목 우측의 Ping 또는 Copy 버튼으로 해당 에셋을 쉽게 찾을 수 있습니다.
3. 제외할 확장자 선택
    - "Exclude Extensions" 영역에서 검색에 제외할 확장자를 체크합니다.
    - Select All / Deslect All / Refresh Asset 버튼으로 빠르게 조정할 수 있습니다.
4. 미사용 에셋 검색
    - "Find Unused Assets" 버튼을 클릭하면 프로젝트 전체를 검색하여 현재 빌드에 포함되지 않은(의존성이 없는) 에셋을 탐지합니다.
5. 결과 확인 및 정리
    - 탐지된 미사용 에셋이 리스트로 표시됩니다.
    - 각 항목에서 Ping 또는 Delete로 강조 및 삭제를 할 수 있습니다.
    - Delete All 버튼으로 모든 미사용 에셋을 한 번에 삭제할 수 있습니다.

## 구현 세부 사항
### UnusedAssetsFinderLogic.cs
- FindUnusedAssets
    - AssetDatabase.GetAllAssetPaths()를 통해 프로젝트 내 모든 에셋 경로를 수집
    - EditorBuildSettings.scenes에 등록된 활성 씬을 기준으로 AssetDatabase.GetDependencies()를 사용해 종속 에셋 목록을 확보
    - 사용 중이지 않은(의존성에 포함되지 않은) 에셋만 필터링하여 반환
- GetCurrentSceneDependencies
    - 현재 활성화된 씬(SceneManager.GetActiveScene())의 에셋 의존성 목록을 반환
    - AssetDatabase.GetDependencies()로 씬에 포함된 모든 에셋 경로를 수집

### UnusedAssetsFinderUI.cs
- Current Scene Dependencies, Exclude Extensions, Unused Assets Result 섹션으로 구성
- 확장자 필터 초기화(InitExtensions()), 토글 상태 저장, 스크롤 뷰 UI 등 구현
- Ping, Copy, Delete, Delete All 버튼을 통해 직접적인 에셋 조작이 가능.

## 참고 사항
- 탐지 기준은 빌드 설정에 포함된 씬의 의존성만 고려합니다. (빌드에 포함되지 않은 씬에 존재하는 에셋은 사용 중이라도 미사용으로 판단될 수 있습니다.)
- 에셋을 삭제하기 전에 반드시 버전 관리 시스템을 사용해 백업을 권장합니다.
- 일부 동적으로 로드되는 에셋(Addressables, Resourecs.Load())은 코드 의존성이 직접 분석되지 않으므로 실제 사용중이여도 미사용으로 탐지될 수 있습니다.
- "Exclude Extensions" 기능을 활용해 주요 리소스를 제외하면 오탐을 줄일 수 있습니다.
- 검색 및 삭제 작업은 즉시 수행되며, Undo 기록에 포함되지 않습니다.

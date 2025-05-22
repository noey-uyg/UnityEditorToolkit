# Mirror Tool
## 개요
이 도구는 Unity에서 선택된 객체들을 지정된 축 기준으로 쉽게 미러링할 수 있도록 도와주는 툴입니다.

객체를 월드 원점 또는 선택 중심 기준으로 반전 복제할 수 있으며, 반복적이고 대칭적인 배치 작업을 빠르게 수행할 수 있도록 설계되었습니다.

## 주요 기능
1. 축 기준 미러링 : 선택한 객체들을 X/Y/Z 축을 기준으로 반전 복제합니다.
2. Pivot 기준 선택
```
- Individual Origins : 각 객체의 위치 기준으로 개별 미러링
- Global Origin : 월드 좌표계 원점 기준 미러링
- Selection Center : 선택한 전체 객체의 중심 기준으로 반전
```
3. Undo 지원 : Undo 시스템을 지원하여 작업 전 상태로 복원 가능

## 사용 방법
1. Mirror Tool 열기 : Unity 메뉴에서 Tools > NoeyToolkit > Placement Tools Window를 선택하여 윈도우를 열고, Tool > Mirror Tool 선택합니다.
2. 옵션 설정 : 씬에서 하나 이상의 오브젝트를 선택한 후 옵션을 설정합니다.
3. Mirror 버튼을 누르면 미러링된 객체가 생성됩니다.

## 구현 세부 사항
### MirrorObjectLogic.cs
- MirrorSelectedObjects: 미러링 전체 처리 로직
- GetMirroredPosition: 축 기준 위치 반전 처리 (선택된 피벗 기준 적용)
- GetMirroredRotation: 미러링 회전 처리 (Euler 각도 반전)
- GetMirroredScale: 축 방향 스케일 반전 처리

### MirrorObjectUI.cs
- MirrorObjectUI.Draw: 사용자 인터페이스 출력 및 옵션 설정
- 선택된 오브젝트 배열을 MirrorObjectLogic에 전달

## 참고 사항
- IndividualOrigins 모드는 객체 하나만 선택된 경우에도 정상적으로 작동합니다.
- 이름 중복 방지를 위해 미러링된 오브젝트는 _Mirrored 접미사가 자동 추가됩니다.
- 회전 반전은 단순 Euler 값 뒤집기를 사용하므로, 특정 각도에서는 정확한 시각적 결과를 확인해보는 것이 좋습니다.

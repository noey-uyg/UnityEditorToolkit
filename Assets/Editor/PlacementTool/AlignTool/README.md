# Align Tool
## 개요
이 도구는 Unity에서 선택된 객체들을 직관적으로 정렬할 수 있도록 도와주는 툴입니다.

객체들을 일렬, 중심, 그리드 형태로 정렬하며, 씬 편집 중 반복적인 객체 정렬 작업을 빠르고 정확하게 처리할 수 있도록 도와줍니다.

## 주요 기능
1. Linear 정렬 : 선택된 객체들을 지정한 축(X, Y, Z) 방향으로 일정한 간격(Spacing)을 두고 일렬로 정렬합니다.
```
- 기준점: 첫 번째 선택 객체 or 선택 전체의 중심
- 방향 반전 가능
```
2. Center 정렬 : 선택된 모든 객체를 선택 영역의 중심 위치로 이동시킵니다.
3. Grid 정렬 : 행(Row)과 열(Col)을 설정하고, 지정한 간격(Spacing)에 따라 격자 형태로 배치합니다.
4. Undo 지원 : Unity의 Undo 시스템을 사용하여 정렬 작업 이전 상태로 복원할 수 있습니다.

## 사용 방법
1. Align Tool 열기 : Unity 메뉴에서 Tools > NoeyToolkit > Placement Tools Window를 선택하여 윈도우를 열고, Tool > Align Tool 선택합니다.
2. 여러 객체를 선택하고, 원하는 정렬 방식을 선택하여 아래 옵션을 설정합니다:
### Linear
- Axis : X / Y / Z 축 방향 선택
- Origin : 정렬 기준 위치 선택
- Spacing : 객체 간 간격
- Reverse Direction : 정렬 방향 반전 여부
### Grid
- Rows / Columns : 행과 열 개수
- Spacing : 셀 간격 (Vector2)
3. Apply 버튼을 누르면 선택된 객체들이 설정에 따라 정렬됩니다.

## 구현 세부 사항
### AlignToolLogic.cs
- AlignObjects 메서드 : 정렬 방식 분기 처리
    - ApplyLinearAlignment : 축 방향 정렬
    - MoveToCenter : 중심 정렬
    - ArrangeInGrid : 그리드 배치
- 정렬된 객체는 Undo 기능을 사용하여 되돌릴 수 있습니다.

### AlignToolUI.cs
- AlignToolUI 클래스는 사용자 인터페이스를 정의하며, 정렬 툴을 시각적으로 제공합니다.
- Draw 메서드에서 입력된 옵션을 기반으로 AlignToolLogic 호출

## 참고 사항
- Grid 정렬 주의 : 선택된 객체 수보다 Rows * Columns가 작으면 일부 객체가 배치되지 않을 수 있습니다.
- 정렬 기준 좌표계는 World Position 기준입니다.

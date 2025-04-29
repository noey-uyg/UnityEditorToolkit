# Smart Duplicate Tool
## 개요
이 도구는 Unity에서 선택한 객체를 복제할 수 있는 에디터 툴입니다.

복제된 객체에 대해 사용자 지정 번호 형식과 위치, 회전, 크기 오프셋을 설정하여 객체를 효율적으로 복제할 수 있습니다.

## 주요 기능
1. 객체 복제 : 선택한 객체를 지정한 개수만큼 복제하고, 각 복제본에 번호를 매길 수 있습니다.
2. 번호 형식 : 복제된 객체 이름에 사용할 번호 형식을 선택할 수 있습니다.
```
- Underscore : GameObject_01
- Dash : GameObject-1
- Parentheses : GameObject(1)
- Dot : GameObject.01
- Hash : GameObject#1
- LeadingZeros3 : GameObject001
```
3. 위치, 회전, 크기 오프셋 : 복제된 객체에 대해 각각 위치, 회전, 크기 오프셋을 설정할 수 있습니다.
4. 미리보기 기능 : 번호 형식의 미리보기를 통해 결과를 확인할 수 있습니다.

## 사용 방법
1. Smart Duplicate 열기 : Unity 메뉴에서 Tools > NoeyToolkit > Placement Tools Window를 선택하여 윈도우를 열고, Tool > Smart Duplicate를 선택합니다.
2. 복제할 객체 설정 :
- Count 슬라이더를 사용하여 복제할 객체의 개수를 설정합니다.
- Numbering 드롭다운에서 번호 형식을 선택합니다.
- Position Offset, Rotation Offset, Scale Offset 필드를 사용하여 각 복제본의 위치, 회전, 크기 오프셋을 설정합니다.
3. 복제 시작 : "Duplicate" 버튼을 클릭하면 선택된 객체가 설정한 개수만큼 복제되며, 지정한 번호 형식과 오프셋이 적용됩니다.
4. 번호 형식 미리보기 : 선택한 번호 형식에 따른 복제된 객체 이름을 미리 확인할 수 있습니다.

## 구현 세부 사항
### SmartDuplicateLogic.cs
- Duplicate 메서드는 선택된 객체들을 복제하고, 각 복제본에 번호 형식과 오프셋을 적용하는 주요 로직을 처리합니다.
- 복제된 객체는 Undo 기능을 사용하여 되돌릴 수 있습니다.

### SmartDuplicateUI.cs
- SmartDuplicateUI 클래스는 사용자 인터페이스를 정의하며, 복제 툴을 시각적으로 제공합니다.
- Draw 메서드에서 복제할 객체 수, 번호 형식, 위치, 회전, 크기 오프셋을 설정할 수 있는 UI를 제공합니다.

## 참고 사항
- 복제 후 객체 간의 간격이나 정렬을 수동으로 조정할 필요가 있을 수 있습니다.

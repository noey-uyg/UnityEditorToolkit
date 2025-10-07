# Large Mesh Detector Tool
## 개요
이 도구는 Unity 씬 내의 모든 렌더러를 검사하여 지정한 삼각형 수 이상을 가진 메쉬를 탐지하는 에디터 유틸리티입니다.

이는 씬 최적화, 성능 개선, 빌드 검수 등에 유용하며, 삼각형이 과도하게 많은 모델을 빠르게 식별할 수 있습니다.

## 주요 기능
1. 대형 메쉬 탐지 : 씬 내의 모든 MeshRenderer 및 SkinnedMeshRenderer를 스캔하여 지정된 임계값 이상인 메쉬를 찾습니다.
2. 삼각형 기준 설정 가능 : 사용자가 직접 삼각형 수 임계값을 입력할 수 있습니다.
3. 결과 목록 제공 : 탐지된 모든 메쉬를 리스트 형태로 표시합니다.
4. 오브젝트 강조 : 결과 리스트에서 각 메쉬의 GameObject를 Unity Hierarchy/Scene 뷰에서 즉시 강조할 수 있습니다.
5. 스크롤 UI 지원 : 결과가 많을 경우 스크롤로 편리하게 탐색할 수 있습니다.

## 사용 방법
1. Tools > NoeyToolkit > Optimization Tools > Large Mesh Detector를 선택해 툴 윈도우를 엽니다.
2. Triangle Threshold 필드에 기준 삼각형 수를 입력합니다. 기본값은 5000입니다.
3. Scan Scene 버튼을 클릭하면 현재 씬의 모든 렌더러를 분석합니다.
4. 탐지된 대형 메쉬 리스트가 아래 표시됩니다. 각 항목의 오른쪽 "Ping" 버튼을 클릭하면 해당 오브젝트가 강조됩니다.

## 구현 세부 사항
### LargeMeshDetectorLogic.cs
-FindLargeMeshes
    - 씬 내 모든 Renderer 검색
    - MeshRenderer는 MeshFilter.sharedMesh에서 SkinnedMeshRerer는 SharedMesh에서 메쉬 정보를 가져옵니다.
    - 각 메쉬의 삼각형 수 (Mesh.triangles.Length / 3)를 계산한 뒤, 지정한 임계값 이상일 경우 결과 리스트에 추가합니다.
- 반환값은 MeshInfo 리스트로, GameObject와 TrangleCount를 포합합니다.

### LargeMeshDetectorUI.cs
- 에디터 UI를 구성하며, 사용자로부터 삼각형 임계값을 입력받고 스캔 실행 버튼을 제공합니다.
- 스캔 결과를 스크롤 가능한 리스트 형태로 표시합니다.
- 각 항목에는 objectField와 함께 Ping 버튼이 있어 에디터에서 즉시 해당 오브젝트를 찾아볼 수 있습니다.

## 참고 사항
- 씬에 존재하는 모든 Renderer를 탐색하므로, 씬 규모가 크면 검사 시간이 늘어날 수 있습니다.
- 삼각형 수는 공유 메쉬를 기준으로 계산됩니다. (인스턴스 변형은 고려되지 않음)
- LODGroup 등 다단계 메쉬는 현재 버전에서 개별 LOD 레벨별로 분석되지 않습니다.
- 탐지된 오브젝트는 Hierarchy에서 Ping만 수행되며, 자동 수정이나 최적화는 하지 않습니다.

# Align Tool
## 개요
이 도구는 Unity 프로젝트의 마지막 빌드 결과를 기반으로, 각 에셋이 빌드에서 차지하는 용량을 분석해주는 도구입니다.

빌드 리포트를 활용하여 에셋별 크기 기여도를 빠르게 파악할 수 있으며, 최적화 과정에서 용량이 큰 리소스를 식별하는 데 유용합니다.

## 주요 기능
1. 빌드 크기 분석 : 마지막 빌드 리포트를 기반으로 각 에셋의 용량을 계산합니다.
2. 상위 에셋 표시 : 빌드에서 용량을 가장 많이 차지하는 상위 10개의 에셋을 표시합니다.
3. 전체 요약 제공 : 전체 에셋 개수와 총 용량을 함께 확인할 수 있습니다.
4. 로그 출력 : 모든 에셋의 상세 크기 정보가 콘솔에 출력됩니다.
5. 에디터 연동 : Unity Editor 창에서 바로 분석 버튼을 눌러 결과를 확인할 수 있습니다.

## 사용 방법
1. Build 실행
    - 유니티 빌드를 수행 하면 빌드 경로가 EditorPrefs를 통해 저장됩니다.
2. Analyzer 실행
    - Unity 메뉴에서 Tools > NoeyToolkit > Build Size Analyzer를 열고, "Analyze Last Build" 버튼을 클릭합니다.
3. 결과 확인
    - 최대 상위 10개의 에셋과 그 용량이 표시됩니다.
    - 전체 로그는 Unity 콘솔에 출력됩니다.

## 구현 세부 사항
### BuildSizeAnalyzerLogic.cs
- AnalyzeLastBuild()
    - 마지막 빌드 경로를 EditorPrefs에서 불러와 BuildPipeline.BuildPlayer()를 이용해 빌드 리포트를 분석합니다.
    - packedAssets 정보를 기반으로 에셋 경로별 크기(PackedSize)를 합산합니다.
    - 결과는 AssetSizeInfo 리스트로 반환되며, 크기 기준으로 내림차순 정렬됩니다.
- StoreBuildPath()
    - 빌드 시 지정한 빌드 경로를 EditorPrefs에 저장하여 나중에 분석 시 참조할 수 있도록 합니다.

### BuildSizeAnalyzerUI.cs
- 간단한 에디터 UI를 구성하며, "Analyze Last Build" 버튼을 제공합니다.
- 버튼 클릭 시 BuildSizeAnalyzerLogic.AnalyzeLastBuild()를 호출하고, 상위 10개의 에셋의 크기 요약 정보를 EditorUtility.DisplayDialog로 표시합니다.
- 전체 에셋 결과는 Unity 콘솔로 출력됩니다.

## 참고 사항
- 분석은 반드시 빌드 리포트가 존재하는 상태에서만 수행됩니다.
- 임시 빌드나 스크립트 전용 빌드에서는 정확한 크기 정보를 얻을 수 없습니다.
- packedAssets 정보는 Unity 버전에 따라 일부 다를 수 있습니다.
- 결과 단위는 EditorUtility.FormatBytes()를 통해 자동 변환되어 표시됩니다.

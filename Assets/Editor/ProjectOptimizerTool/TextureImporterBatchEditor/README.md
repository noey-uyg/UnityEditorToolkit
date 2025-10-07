# Texture Importer Batch Editor Tool
## 개요
이 도구는 Unity 프로젝트 내 여러 텍스처 에셋의 임포트 설정을 일괄적으로 변경할 수 있는 에디터 유틸리티입니다.

모바일 빌드를 비롯한 플랫폼 별 임포트 세팅을 한 번에 적용할 수 있으며, 압축 설정, 최대 해상도, MipMap 생성 여부, 알파 채널 감지 등을 빠르고 일관되게 설정할 수 있습니다.

텍스처 최적화와 플랫폼 대응 세팅을 효율적으로 관리할 수 있도록 설계되었습니다.

## 주요 기능
1. 일괄 설정 적용 : 여러 텍스처를 선택한 후, 설정값을 한 번에 적용합니다.
2. 플랫폼별 세팅 지원 : Android, IOS용 별도 임포트 설정을 적용할 수 있습니다.
3. 알파 채널 자동 감지 : 텍스처의 알파 채널 존재 여부를 자동 감지하여 투명 여부 설정을 자동화합니다.
4. 압축 형식 선택 : TexturImporterCompression 옵션을 통해 압출 품질을 조정합니다.
5. MipMap 생성 여부 제어 : 필요에 따라 MipMap 생성 기능을 켜거나 끌 수 있습니다.
6. 미리보기 제공 : 현재 선택된 텍스처들의 썸네일을 미리 확인할 수 있습니다.

## 사용 방법
1. Tools > NoeyToolkit > Project Optimizer Toolkit > Texture Batch Editor를 선택합니다.
2. Project 뷰에서 임포트 설정을 변경할 텍스처를 선택합니다.
3. 설정값 입력
4. "Apply To Selected Textures" 버튼을 클릭하여 선택된 모든 텍스처에 설정을 적용합니다.
5. 미리보기 확인

## 구현 세부 사항
### TextureImporterBatchEditorLogic.cs
- ApplySettingsToTextures
    - 전달받은 Texture2D 배열에 대해 각종 임포트 설정을 일괄 적용합니다.
- TextureHasAlpha
    - 에셋 경로를 기준으로 알파 채널 존재 여부를 검사하여 반환합니다.

### TextureImporterBatchEditorUI.cs
- 사용자 인터페이스를 담당하는 클래스입니다.
- 각 옵션을 GUI로 설정하고, 버튼 클릭 시 로직을 호출합니다.
- 선택된 텍스처 미리보기를 표시합니다.

## 참고 사항
- project 뷰에서 텍스처 에셋만 선택 가능합니다.(폴더 선택 불가)
- Apply To Selected Textures 클릭 시 즉시 Reimport가 수행되므로, 대량의 텍스처를 선택할 경우 시간이 다소 소요될 수 있습니다.
- 플랫폼 별 설정은 Unity가 지원하는 TextureImporterFormat을 자동으로 선택합니다.
- 알파 채널이 존재하는 경우 자동으로 ASTC_4x4, 존재하지 않을 경우 ETC2_RGB4(Android) 또는 PVRTC_RGB4(iOS)로 지정됩니다.
- 변경된 설정은 unity의 Undo 시스템에 등록되지 않습니다.(필요 시 버전 관리 시스템 사용을 권장합니다.)

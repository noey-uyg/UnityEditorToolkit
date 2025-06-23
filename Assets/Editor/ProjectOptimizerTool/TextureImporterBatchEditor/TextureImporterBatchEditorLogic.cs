using UnityEditor;
using UnityEngine;

namespace noeyToolkit
{
    public class TextureImporterBatchEditorLogic : MonoBehaviour
    {
        public static int ApplySettingsToTextrues(Texture2D[] textures, int maxSize, TextureImporterCompression compression, bool enableMipMaps,
            bool applyToAndroid, bool applyToIOS, bool detectAlpha, TextureImporterType type)
        {
            int changedCount = 0;

            foreach(var tex in textures)
            {
                string path = AssetDatabase.GetAssetPath(tex);
                var importer = AssetImporter.GetAtPath(path) as TextureImporter;

                if (importer == null) continue;

                // 알파 채널 감지
                bool hasAlpha = detectAlpha ? TextureHasAlpha(tex) : importer.DoesSourceTextureHaveAlpha();

                // 기본 설정 적용
                importer.textureType = type;
                importer.maxTextureSize = maxSize;
                importer.textureCompression = compression;
                importer.mipmapEnabled = enableMipMaps;

                importer.alphaSource = hasAlpha ? TextureImporterAlphaSource.FromInput : TextureImporterAlphaSource.None;
                importer.alphaIsTransparency = hasAlpha;

                if (applyToAndroid)
                {
                    var settings = importer.GetPlatformTextureSettings("Android");
                    settings.overridden = true;
                    settings.maxTextureSize = maxSize;
                    settings.format = hasAlpha ? TextureImporterFormat.ASTC_4x4 : TextureImporterFormat.ETC2_RGB4;
                    importer.SetPlatformTextureSettings(settings);
                }

                if (applyToIOS)
                {
                    var settings = importer.GetPlatformTextureSettings("iPhone");
                    settings.overridden = true;
                    settings.maxTextureSize = maxSize;
                    settings.format = hasAlpha ? TextureImporterFormat.ASTC_4x4 : TextureImporterFormat.PVRTC_RGB4;
                    importer.SetPlatformTextureSettings(settings);
                }

                EditorUtility.SetDirty(importer);
                importer.SaveAndReimport();
                changedCount++;
            }

            return changedCount;
        }

        // 텍스쳐가 알파 채널을 포함하는지 확인
        private static bool TextureHasAlpha(Texture2D texture)
        {
            var path = AssetDatabase.GetAssetPath(texture);
            var t = AssetImporter.GetAtPath(path) as TextureImporter;
            return t != null && t.DoesSourceTextureHaveAlpha();
        }
    }
}

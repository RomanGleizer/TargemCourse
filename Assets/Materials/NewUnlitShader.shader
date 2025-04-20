Shader "Custom/ProceduralNoiseDomainWarp_NoDiag"
{
    Properties
    {
        _GradientTex("Gradient Texture (Color Lookup)", 2D) = "white" {}
        _NoiseScale("Noise Scale (Frequency)", Float) = 5.0
        _NoiseOctaves("Noise Octaves (fBM layers)", Range(1,8)) = 5
        _Persistence("Persistence (fBM amplitude decay)", Range(0,1)) = 0.5
        _Lacunarity("Lacunarity (fBM frequency growth)", Float) = 2.0
        _WarpStrength("Warp Strength", Float) = 0.2
        _WarpScale("Warp Noise Scale", Float) = 2.0
        _WarpOctaves("Warp Noise Octaves", Range(1,8)) = 2
        _Speed("Animation Speed", Float) = 0.2
        _Vectorization("Vectorization Steps", Range(1,10)) = 1
        _NormalStrength("Normal Strength", Float) = 1.0
        _LightDir("Light Direction (world space)", Vector) = (0,0,1,0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0
            #include "UnityCG.cginc"

            // Объявляем uniform-переменные, соответствующие свойствам
            sampler2D _GradientTex;
            float _NoiseScale;
            float _NoiseOctaves;
            float _Persistence;
            float _Lacunarity;
            float _WarpStrength;
            float _WarpScale;
            float _WarpOctaves;
            float _Speed;
            float _Vectorization;
            float _NormalStrength;
            float4 _LightDir;

            // Функции для генерации шума
            float Hash(float n) {
                return frac(sin(n) * 43758.5453123);
            }
            float Hash(float2 p) {
                return frac(sin(dot(p, float2(12.9898,78.233))) * 43758.5453123);
            }
            float PerlinNoise2D(float2 pos) {
                float2 i = floor(pos);
                float2 f = frac(pos);
                float a = Hash(i);
                float b = Hash(i + float2(1, 0));
                float c = Hash(i + float2(0, 1));
                float d = Hash(i + float2(1, 1));
                float2 u = f * f * (3.0 - 2.0 * f);
                return lerp(lerp(a, b, u.x), lerp(c, d, u.x), u.y);
            }
            float fBM(float2 pos, int octaves, float persistence, float lacunarity) {
                float value = 0.0;
                float amplitude = 1.0;
                float frequency = 1.0;
                [unroll(8)]
                for (int octave = 0; octave < 8; octave++) {
                    if (octave >= octaves) break;
                    value += PerlinNoise2D(pos * frequency) * amplitude;
                    frequency *= lacunarity;
                    amplitude *= persistence;
                }
                return value;
            }

            /* 
              Функция domainWarpedNoise:
              - Базовый uv используется без смещения.
              - В вычислении смещения (warpOffset) время добавляется симметрично:
                  для x-компоненты – прибавляем (time, –time)
                  для y-компоненты – прибавляем (–time, time)
              Это гарантирует, что суммарное смещение (среднее) равно (0,0) и общий паттерн не смещается.
            */
            float domainWarpedNoise(float2 uv, float time, int octavesMain, float persMain, float lacunMain,
                                     float warpStrength, float warpScale, int octavesWarp, float persWarp, float lacunWarp)
            {
                // Применяем масштаб для вычисления шума искажения (без time)
                float2 warpUV = uv * warpScale;
                
                // Вычисляем симметричное время: добавляем time по-разному для x и y
                float2 timeOffsetX = float2(time, -time);
                float2 timeOffsetY = float2(-time, time);
                
                float2 warpOffset;
                warpOffset.x = fBM(warpUV + float2(37.2, 15.7) + timeOffsetX, octavesWarp, persWarp, lacunWarp);
                warpOffset.y = fBM(warpUV + float2(92.3, 47.6) + timeOffsetY, octavesWarp, persWarp, lacunWarp);
                
                // Центрируем смещение (из диапазона [0,1] в [-0.5,0.5]) и масштабируем силой
                warpOffset = (warpOffset - 0.5) * warpStrength;
                
                // Применяем смещение к исходным uv – паттерн не смещается в целом
                float2 warpedUV = uv + warpOffset;
                
                // Вычисляем итоговое значение шума по искажённым координатам
                float noiseValue = fBM(warpedUV, octavesMain, persMain, lacunMain);
                return noiseValue;
            }

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float time = _Time.y * _Speed;
                // Масштабируем UV для контроля детализации
                float2 uv = i.uv * _NoiseScale;
                
                float noiseValue = domainWarpedNoise(uv, time, (int)_NoiseOctaves, _Persistence, _Lacunarity,
                                                     _WarpStrength, _WarpScale, (int)_WarpOctaves, _Persistence, _Lacunarity);
                
                if (_Vectorization > 1)
                {
                    float steps = floor(_Vectorization);
                    noiseValue = floor(noiseValue * steps) / steps;
                }
                
                fixed4 color = tex2D(_GradientTex, float2(noiseValue, 0.5));
                
                // Вычисляем нормали по центральным разностям
                float e = 0.001;
                float heightL = domainWarpedNoise(uv + float2(-e, 0), time, (int)_NoiseOctaves, _Persistence, _Lacunarity,
                                                  _WarpStrength, _WarpScale, (int)_WarpOctaves, _Persistence, _Lacunarity);
                float heightR = domainWarpedNoise(uv + float2(e, 0), time, (int)_NoiseOctaves, _Persistence, _Lacunarity,
                                                  _WarpStrength, _WarpScale, (int)_WarpOctaves, _Persistence, _Lacunarity);
                float heightD = domainWarpedNoise(uv + float2(0, -e), time, (int)_NoiseOctaves, _Persistence, _Lacunarity,
                                                  _WarpStrength, _WarpScale, (int)_WarpOctaves, _Persistence, _Lacunarity);
                float heightU = domainWarpedNoise(uv + float2(0, e), time, (int)_NoiseOctaves, _Persistence, _Lacunarity,
                                                  _WarpStrength, _WarpScale, (int)_WarpOctaves, _Persistence, _Lacunarity);
                float dHx = (heightR - heightL);
                float dHy = (heightU - heightD);
                float3 normalTangent = normalize(float3(-dHx * _NormalStrength, -dHy * _NormalStrength, 1.0));
                
                // Простое диффузное освещение (Ламберт)
                float3 lightDir = normalize(_LightDir.xyz);
                float NdotL = saturate(dot(normalTangent, lightDir));
                float ambient = 0.3;
                float lighting = ambient + NdotL;
                
                fixed4 finalColor = fixed4(color.rgb * lighting, 1.0);
                return finalColor;
            }
            ENDCG
        }
    }
}

/*
 * PointFaible.shader
 * Permet à un object (plus précisément son matériel) de rester visible même à l'intérieur d'un autre objet 3d.
 */

Shader "Unlit/PointFaible"
{
    // Éléments qui seront affichés et modifiables depuis l'éditeur (comme les attributs publics dans les scripts).
    Properties
    {
        _Color ("Couleur", Color) = (1, 0, 0, 1)
    }
    SubShader
    {
        Tags
        {
            // Définit l'affichage du shader tout à la fin
            "Queue"="Overlay"
        }

        Pass
        {
            // Permet d'afficher l'élément entièrement tout le temps
            ZTest Always
            // Évite de cacher d'autres objets
            ZWrite Off
            // Active la transparence
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            // On indique les fonctions à utiliser
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            float4 _Color;

            v2f vert(appdata v)
            {
                v2f result;
                result.vertex = UnityObjectToClipPos(v.vertex);
                return result;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return _Color;
            }
            ENDCG
        }
    }
}
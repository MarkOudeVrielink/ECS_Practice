﻿using Unity.Entities;

#if ENABLE_HYBRID_RENDERER_V2 && UNITY_2020_1_OR_NEWER && (HDRP_9_0_0_OR_NEWER || URP_9_0_0_OR_NEWER)
namespace Unity.Rendering
{
    [MaterialProperty("_DetailNormalScale"    , MaterialPropertyFormat.Float )]
    [GenerateAuthoringComponent]
    public struct HDRPMaterialPropertyDetailNormalScale      : IComponentData { public float  Value; }
}
#endif

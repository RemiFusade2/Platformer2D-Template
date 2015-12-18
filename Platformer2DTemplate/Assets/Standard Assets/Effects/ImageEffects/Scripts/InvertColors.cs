using System;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	[ExecuteInEditMode]
	[AddComponentMenu("Image Effects/Color Adjustments/Invert Colors")]
	public class InvertColors : ImageEffectBase
	{
		// Called by camera to apply image effect
		void OnRenderImage (RenderTexture source, RenderTexture destination)
		{
			Graphics.Blit (source, destination, material);
		}
	}
}

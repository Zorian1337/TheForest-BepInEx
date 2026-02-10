using System;
using UnityEngine;

namespace Ceto
{
	// Token: 0x020001B4 RID: 436
	[AddComponentMenu("Ceto/Camera/UnderWaterPostEffect")]
	[RequireComponent(typeof(Camera))]
	public class UnderWaterPostEffect : MonoBehaviour
	{
		// Token: 0x06000DAB RID: 3499 RVA: 0x00071E1B File Offset: 0x0007021B
		public UnderWaterPostEffect()
		{
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x00071E3C File Offset: 0x0007023C
		private void Start()
		{
			this.m_material = new Material(this.underWaterPostEffectSdr);
			this.m_imageBlur = new ImageBlur(this.blurShader);
			this.m_query = new WaveQuery();
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x00071E6C File Offset: 0x0007026C
		private void LateUpdate()
		{
			Camera component = base.GetComponent<Camera>();
			this.m_underWaterIsVisible = this.UnderWaterIsVisible(component);
			if (this.controlUnderwaterMode && Ocean.Instance != null && Ocean.Instance.UnderWater is UnderWater)
			{
				UnderWater underWater = Ocean.Instance.UnderWater as UnderWater;
				if (!this.m_underWaterIsVisible)
				{
					underWater.underwaterMode = UNDERWATER_MODE.ABOVE_ONLY;
				}
				else
				{
					underWater.underwaterMode = UNDERWATER_MODE.ABOVE_AND_BELOW;
				}
			}
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x00071EEC File Offset: 0x000702EC
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (this.underWaterPostEffectSdr == null || this.m_material == null || SystemInfo.graphicsShaderLevel < 30)
			{
				Graphics.Blit(source, destination);
				return;
			}
			if (Ocean.Instance == null || Ocean.Instance.UnderWater == null)
			{
				Graphics.Blit(source, destination);
				return;
			}
			if (!Ocean.Instance.gameObject.activeInHierarchy)
			{
				Graphics.Blit(source, destination);
				return;
			}
			if (Ocean.Instance.UnderWater.Mode != UNDERWATER_MODE.ABOVE_AND_BELOW)
			{
				Graphics.Blit(source, destination);
				return;
			}
			if (!this.m_underWaterIsVisible)
			{
				Graphics.Blit(source, destination);
				return;
			}
			Camera component = base.GetComponent<Camera>();
			float nearClipPlane = component.nearClipPlane;
			float farClipPlane = component.farClipPlane;
			float fieldOfView = component.fieldOfView;
			float aspect = component.aspect;
			Matrix4x4 identity = Matrix4x4.identity;
			float num = fieldOfView * 0.5f;
			Vector3 b = component.transform.right * nearClipPlane * Mathf.Tan(num * 0.0174532924f) * aspect;
			Vector3 b2 = component.transform.up * nearClipPlane * Mathf.Tan(num * 0.0174532924f);
			Vector3 vector = component.transform.forward * nearClipPlane - b + b2;
			float d = vector.magnitude * farClipPlane / nearClipPlane;
			vector.Normalize();
			vector *= d;
			Vector3 vector2 = component.transform.forward * nearClipPlane + b + b2;
			vector2.Normalize();
			vector2 *= d;
			Vector3 vector3 = component.transform.forward * nearClipPlane + b - b2;
			vector3.Normalize();
			vector3 *= d;
			Vector3 vector4 = component.transform.forward * nearClipPlane - b - b2;
			vector4.Normalize();
			vector4 *= d;
			identity.SetRow(0, vector);
			identity.SetRow(1, vector2);
			identity.SetRow(2, vector3);
			identity.SetRow(3, vector4);
			this.m_material.SetMatrix("_FrustumCorners", identity);
			Color value = Color.white;
			if (this.attenuateBySun)
			{
				value = Ocean.Instance.SunColor() * Mathf.Max(0f, Vector3.Dot(Vector3.up, Ocean.Instance.SunDir()));
			}
			this.m_material.SetColor("_MultiplyCol", value);
			RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height, 0, RenderTextureFormat.Default);
			this.CustomGraphicsBlit(source, temporary, this.m_material, 0);
			this.m_imageBlur.BlurIterations = this.blurIterations;
			
			// Disables our underwater blur with a simple toggle
			this.m_imageBlur.BlurMode = ImageBlur.BLUR_MODE.OFF; // Original was this.blurMode
			this.m_imageBlur.BlurSpread = this.blurSpread;
			this.m_imageBlur.Blur(temporary);
			this.m_material.SetTexture("_BelowTex", temporary);
			Graphics.Blit(source, destination, this.m_material, 1);
			RenderTexture.ReleaseTemporary(temporary);
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x00072234 File Offset: 0x00070634
		private void CustomGraphicsBlit(RenderTexture source, RenderTexture dest, Material mat, int pass)
		{
			RenderTexture.active = dest;
			mat.SetTexture("_MainTex", source);
			GL.PushMatrix();
			GL.LoadOrtho();
			mat.SetPass(pass);
			GL.Begin(7);
			GL.MultiTexCoord2(0, 0f, 0f);
			GL.Vertex3(0f, 0f, 3f);
			GL.MultiTexCoord2(0, 1f, 0f);
			GL.Vertex3(1f, 0f, 2f);
			GL.MultiTexCoord2(0, 1f, 1f);
			GL.Vertex3(1f, 1f, 1f);
			GL.MultiTexCoord2(0, 0f, 1f);
			GL.Vertex3(0f, 1f, 0f);
			GL.End();
			GL.PopMatrix();
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x00072308 File Offset: 0x00070708
		private bool UnderWaterIsVisible(Camera cam)
		{
			if (Ocean.Instance == null)
			{
				return false;
			}
			Vector3 position = cam.transform.position;
			if (this.disableOnClip)
			{
				this.m_query.posX = position.x;
				this.m_query.posZ = position.z;
				this.m_query.mode = QUERY_MODE.CLIP_TEST;
				Ocean.Instance.QueryWaves(this.m_query);
				if (this.m_query.result.isClipped)
				{
					return false;
				}
			}
			float num = Ocean.Instance.FindMaxDisplacement(true) + Ocean.Instance.level;
			if (position.y < num)
			{
				return true;
			}
			Matrix4x4 inverse = (cam.projectionMatrix * cam.worldToCameraMatrix).inverse;
			for (int i = 0; i < 4; i++)
			{
				Vector4 vector = inverse * UnderWaterPostEffect.m_corners[i];
				vector.y /= vector.w;
				if (vector.y < num)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x0007242C File Offset: 0x0007082C
		// Note: this type is marked as 'beforefieldinit'.
		static UnderWaterPostEffect()
		{
		}

		// Token: 0x04000D1D RID: 3357
		public bool disableOnClip = true;

		// Token: 0x04000D1E RID: 3358
		public bool controlUnderwaterMode;

		// Token: 0x04000D1F RID: 3359
		public bool attenuateBySun;

		// Token: 0x04000D20 RID: 3360
		public ImageBlur.BLUR_MODE blurMode;

		// Token: 0x04000D21 RID: 3361
		[Range(0f, 4f)]
		public int blurIterations = 3;

		// Token: 0x04000D22 RID: 3362
		[Range(0.5f, 1f)]
		private float blurSpread = 0.6f;

		// Token: 0x04000D23 RID: 3363
		public Shader underWaterPostEffectSdr;

		// Token: 0x04000D24 RID: 3364
		[HideInInspector]
		public Shader blurShader;

		// Token: 0x04000D25 RID: 3365
		private Material m_material;

		// Token: 0x04000D26 RID: 3366
		private ImageBlur m_imageBlur;

		// Token: 0x04000D27 RID: 3367
		private WaveQuery m_query;

		// Token: 0x04000D28 RID: 3368
		private bool m_underWaterIsVisible;

		// Token: 0x04000D29 RID: 3369
		private static readonly Vector4[] m_corners = new Vector4[]
		{
			new Vector4(-1f, -1f, -1f, 1f),
			new Vector4(1f, -1f, -1f, 1f),
			new Vector4(1f, 1f, -1f, 1f),
			new Vector4(-1f, 1f, -1f, 1f)
		};
	}
}

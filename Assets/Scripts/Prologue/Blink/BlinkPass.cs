// ScriptableRenderPass template created for URP 12 and Unity 2021.2
// Made by Alexander Ameye 
// https://alexanderameye.github.io/
// With eye blink effect by pamisu
// https://www.jianshu.com/u/2bbc278bfa6e
// Modified by Eli Fox

using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BlinkPass : ScriptableRenderPass
{
    // The profiler tag that will show up in the frame debugger.
    const string ProfilerTag = "Blink Pass";

    // We will store our pass settings in this variable.
    BlinkFeature.PassSettings passSettings;
    
    RenderTargetIdentifier colorBuffer, buffer0, buffer1;
    int buffer0ID = Shader.PropertyToID("_TemporaryBuffer0");
    int buffer1ID = Shader.PropertyToID("_TemporaryBuffer1");

    Material material;
    
    // It is good to cache the shader property IDs here.
    static readonly int BlurSizeProperty = Shader.PropertyToID("_BlurSize");
    static readonly int ProgressProperty = Shader.PropertyToID("_Progress");
    static readonly int ArchHeightProperty = Shader.PropertyToID("_ArchHeight");
    static readonly int DarknessStrengthProperty = Shader.PropertyToID("_DarknessStrength");

    // The constructor of the pass. Here you can set any material properties that do not need to be updated on a per-frame basis.
    public BlinkPass(BlinkFeature.PassSettings passSettings)
    {
        this.passSettings = passSettings;

        // Set the render pass event.
        renderPassEvent = passSettings.renderPassEvent; 
        
        // We create a material that will be used during our pass. You can do it like this using the 'CreateEngineMaterial' method, giving it
        // a shader path as an input or you can use a 'public Material material;' field in your pass settings and access it here through 'passSettings.material'.
        if(material == null) material = CoreUtils.CreateEngineMaterial("Custom/Awake Screen Effect");

        // Set any material properties based on our pass settings. 
        material.SetFloat(DarknessStrengthProperty, passSettings.darknessStrength);
    }

    // Gets called by the renderer before executing the pass.
    // Can be used to configure render targets and their clearing state.
    // Can be user to create temporary render target textures.
    // If this method is not overriden, the render pass will render to the active camera render target.
    public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
    {
        // Grab the camera target descriptor. We will use this when creating a temporary render texture.
        RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;
        
        // Set the number of depth bits we need for our temporary render texture.
        descriptor.depthBufferBits = 0;

        // Downsample the original camera target descriptor. 
        // You would do this for performance reasons or less commonly, for aesthetics.
        //descriptor.width /= passSettings.downsample;
        //descriptor.height /= passSettings.downsample;

        // Enable these if your pass requires access to the CameraDepthTexture or the CameraNormalsTexture.
        // ConfigureInput(ScriptableRenderPassInput.Depth);
        // ConfigureInput(ScriptableRenderPassInput.Normal);

        // Grab the color buffer from the renderer camera color target.
        colorBuffer = renderingData.cameraData.renderer.cameraColorTarget;
        
        // Create a temporary render texture using the descriptor from above.
        cmd.GetTemporaryRT(buffer0ID, descriptor, FilterMode.Bilinear);
        buffer0 = new RenderTargetIdentifier(buffer0ID);
        cmd.GetTemporaryRT(buffer1ID, descriptor, FilterMode.Bilinear);
        buffer1 = new RenderTargetIdentifier(buffer1ID);
    }

    // The actual execution of the pass. This is where custom rendering occurs.
    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        // Grab a command buffer. We put the actual execution of the pass inside of a profiling scope.
        CommandBuffer cmd = CommandBufferPool.Get(); 
        using (new ProfilingScope(cmd, new ProfilingSampler(ProfilerTag)))
        {
            material.SetFloat(ProgressProperty, passSettings.progress);

            if (passSettings.progress < 1)
            {
                // 由于降采样会影响模糊到清晰的连贯性，这里没有使用
                // int ds = Mathf.RoundToInt(downSample - (downSample - 1) * progress);
                // int rtW = src.width / ds;
                // int rtH = src.height / ds;

                Blit(cmd, colorBuffer, buffer0, material, 0);

                // int iterations = Mathf.RoundToInt(blurIterations - blurIterations * progress);
                float blurSize;
                for (int i = 0; i < passSettings.blurIterations; i++)
                {
                    // 将progress(0~1)映射到blurSize(blurSize~0)
                    blurSize = 1f + i * passSettings.blurSpread;
                    blurSize -= blurSize * passSettings.progress;
                    material.SetFloat(BlurSizeProperty, blurSize);

                    Blit(cmd, buffer0, buffer1, material, 1);
                }
                Blit(cmd, buffer1, buffer0, material, 2);
                Blit(cmd, buffer0, colorBuffer);
            }
            else
            {
                Blit(cmd, colorBuffer, colorBuffer);
            }
        }

        // Execute the command buffer and release it.
        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }
    
    // Called when the camera has finished rendering.
    // Here we release/cleanup any allocated resources that were created by this pass.
    // Gets called for all cameras i na camera stack.
    public override void OnCameraCleanup(CommandBuffer cmd)
    {
        if (cmd == null) throw new ArgumentNullException("cmd");
        
        // Since we created a temporary render texture in OnCameraSetup, we need to release the memory here to avoid a leak.
        cmd.ReleaseTemporaryRT(buffer0ID);
        cmd.ReleaseTemporaryRT(buffer1ID);
    }
}
